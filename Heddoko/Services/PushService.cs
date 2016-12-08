using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DAL;
using DAL.Models;
using DAL.Models.Enum;
using DAL.Models.Enums;
using DAL.Models.MongoDocuments.Notifications;
using Hangfire;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using PushSharp.Core;
using PushSharp.Google;
using Services.Authorization;

namespace Services
{
    public static class PushService
    {
        private static HubConnection _hubConnection;
        private static IHubProxy _notificationsHubProxy;

        private static readonly object LockHubConnection = new object();

        public static DateTime? RetryGcmAfterUtc { get; private set; }

        private static ApnsConfiguration ConfigApns()
        {
            return new ApnsConfiguration(
                DAL.Config.IOSSandbox ? ApnsConfiguration.ApnsServerEnvironment.Sandbox : ApnsConfiguration.ApnsServerEnvironment.Production,
                Path.Combine(DAL.Config.BaseDirectory, DAL.Config.IOSCertificate),
                null
            );
        }

        private static GcmConfiguration ConfigGcm()
        {
            return new GcmConfiguration(DAL.Config.GcmSenderId, DAL.Config.GcmSenderAuthToken, null);
        }

        private static JObject BuildPayloadForApns(string message, UserEventType type, string id)
        {
            string json = string.Format(@"
                {{
                    ""aps"": {{
                    ""badge"": 1,
                    ""alert"": ""{0}"",
                    ""custom"": {{
                            ""message"" : ""{0}"",
                            ""type"" : {1},
                            ""eventId"" : ""{2}""
                        }}
                    }}
                }}
            ", message,
            (int)type,
            id);

            return JObject.Parse(json);
        }

        private static JObject BuildNotificationForGcm(string message)
        {
            string json = string.Format(@"
                {{
                    ""body"" : ""{0}"",
                    ""title"" : ""{0}""
                }}",
                message);

            return JObject.Parse(json);
        }

        private static JObject BuildDataForGcm(string message, UserEventType type, string id)
        {
            string json = $@"
                {{
                    ""message"" : ""{message}"",
                    ""type"" : {(int)type},
                    ""eventId"" : ""{id}""
                }}";

            return JObject.Parse(json);
        }

        private static void ApnsNotificationSucceeded(ApnsNotification notification)
        {
            Trace.TraceInformation($"Apple Notification Succeeded, Token:{notification.DeviceToken}");

            string eventId = (string)notification.Payload["aps"]["custom"]["eventId"];

            SetEventStatusSent(eventId);
        }

        private static void ApnsNotificationFailed(ApnsNotification notification, AggregateException exception)
        {
            exception.Handle(ex =>
            {
                var notificationException = ex as ApnsNotificationException;
                if (notificationException != null)
                {
                    ApnsNotification apnsNotification = notificationException.Notification;
                    ApnsNotificationErrorStatusCode statusCode = notificationException.ErrorStatusCode;

                    Trace.TraceError($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}, Token:{apnsNotification.DeviceToken}");

                    switch (statusCode)
                    {
                        case ApnsNotificationErrorStatusCode.InvalidToken:
                        case ApnsNotificationErrorStatusCode.InvalidTokenSize:
                        case ApnsNotificationErrorStatusCode.MissingDeviceToken:
                            UnitOfWork unitOfWork = new UnitOfWork();
                            unitOfWork.DeviceRepository.DeleteByToken(apnsNotification.DeviceToken);
                            unitOfWork.Save();
                            break;
                    }
                }
                else
                {
                    Trace.TraceError($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                }

                string eventId = (string)notification.Payload["aps"]["custom"]["eventId"];

                SetEventStatusFailed(eventId);

                return true;
            });
        }

        private static void GcmNotificationSucceeded(GcmNotification notification)
        {
            Trace.TraceInformation($"GCM Notification Succeeded, Token:{notification.RegistrationIds.FirstOrDefault()}");

            string eventId = (string)notification.Data["eventId"];

            SetEventStatusSent(eventId);
        }

        private static void GcmNotificationFailed(GcmNotification notification, AggregateException exception)
        {
            exception.Handle(ex =>
            {
                var notificationException = ex as GcmNotificationException;
                if (notificationException != null)
                {
                    GcmNotification gcmNotification = notificationException.Notification;
                    string description = notificationException.Description;

                    Trace.TraceInformation($"GCM Notification Failed: ID={gcmNotification.MessageId}, Desc={description}");
                }
                else if (ex is GcmMulticastResultException)
                {
                    var multicastException = (GcmMulticastResultException)ex;

                    foreach (var succeededNotification in multicastException.Succeeded)
                    {
                        Trace.TraceInformation($"GCM Notification Failed: ID={succeededNotification.MessageId}");
                    }

                    foreach (var failedKvp in multicastException.Failed)
                    {
                        GcmNotification n = failedKvp.Key;
                        Exception e = failedKvp.Value;

                        var notifExeption = e as GcmNotificationException;
                        string description = notifExeption != null ? notifExeption.Description : e.Message;

                        Trace.TraceInformation($"GCM Notification Failed: ID={n.MessageId}, Desc={description}");
                    }
                }
                else if (ex is DeviceSubscriptionExpiredException)
                {
                    var expiredException = (DeviceSubscriptionExpiredException)ex;

                    string oldId = expiredException.OldSubscriptionId;
                    string newId = expiredException.NewSubscriptionId;

                    Trace.TraceInformation($"Device RegistrationId Expired: {oldId}");

                    UnitOfWork unitOfWork = new UnitOfWork();

                    if (!string.IsNullOrWhiteSpace(newId))
                    {

                        Device device = unitOfWork.DeviceRepository.GetByToken(oldId);

                        if (device != null)
                        {
                            device.Token = newId;
                            unitOfWork.Save();
                        }

                        Trace.TraceInformation($"Device RegistrationId Changed To: {newId}");
                    }
                    else
                    {
                        unitOfWork.DeviceRepository.DeleteByToken(oldId);
                    }
                }
                else if (ex is RetryAfterException)
                {
                    var retryException = (RetryAfterException)ex;

                    Trace.TraceInformation($"GCM Rate Limited, don't send more until after {retryException.RetryAfterUtc}");

                    // If you get rate limited, you should stop sending messages until after the RetryAfterUtc date
                    RetryGcmAfterUtc = retryException.RetryAfterUtc;
                    BackgroundJob.Schedule(() => SendGcmNotification(notification), retryException.RetryAfterUtc);
                }
                else
                {
                    Trace.TraceInformation($"GCM Notification Failed for some unknown reason: {ex.GetBaseException()}");
                }

                string eventId = (string)notification.Data["eventId"];

                SetEventStatusFailed(eventId);

                return true;
            });
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void SendApnsNotification(string message, string token, UserEventType type, string eventId)
        {
            ApnsServiceBroker broker = new ApnsServiceBroker(ConfigApns());

            broker.OnNotificationFailed += ApnsNotificationFailed;

            broker.OnNotificationSucceeded += ApnsNotificationSucceeded;

            JObject payload = BuildPayloadForApns(message, type, eventId);
            broker.Start();

            broker.QueueNotification(new ApnsNotification
            {
                DeviceToken = token,
                Payload = payload
            });

            broker.Stop();
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void SendGcmNotification(string message, string token, UserEventType type, string eventId)
        {
            SendGcmNotification(new GcmNotification
            {
                RegistrationIds = new List<string> { token },
                To = token,
                Data = BuildDataForGcm(message, type, eventId),
                Notification = BuildNotificationForGcm(message)
            });
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        private static void SendGcmNotification(GcmNotification notification)
        {
            var config = ConfigGcm();

            var gcmBroker = new GcmServiceBroker(config);

            gcmBroker.OnNotificationFailed += GcmNotificationFailed;

            gcmBroker.OnNotificationSucceeded += GcmNotificationSucceeded;

            gcmBroker.Start();

            gcmBroker.QueueNotification(notification);

            gcmBroker.Stop();
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void SendDesktopNotification(string message, string token, UserEventType type, string eventId)
        {
            try
            {
                if (_hubConnection == null || _hubConnection.State != ConnectionState.Connected)
                {
                    lock (LockHubConnection)
                    {
                        if (_hubConnection == null || _hubConnection.State != ConnectionState.Connected)
                        {
                            _hubConnection = new HubConnection(DAL.Config.DashboardSite);
                            _hubConnection.Headers.Add(Config.TokenHeaderName, AuthorizationManager.Instance.GetToken());

                            _notificationsHubProxy = _hubConnection.CreateHubProxy(DAL.Config.NotificationsHub);

                            _hubConnection.StateChanged += HubConnectionOnStateChanged;

                            _hubConnection.Start().Wait();
                        }
                    }
                }

                _notificationsHubProxy.Invoke("Send", message, token, type).ContinueWith(task =>
                 {
                     if (task.IsFaulted && task.Exception != null)
                     {
                         Trace.TraceError($"SignalR Notification Failed on Invoke: {task.Exception.GetBaseException()}");

                         SetEventStatusFailed(eventId);
                     }
                     else if (!task.IsFaulted)
                     {
                         SetEventStatusSent(eventId);
                     }
                 });

            }
            catch (Exception e)
            {
                Trace.TraceError($"SignalR Notification Failed : {e.GetBaseException()}");
                SetEventStatusFailed(eventId);
            }
        }

        private static void HubConnectionOnStateChanged(StateChange stateChange)
        {
            if (stateChange.NewState == ConnectionState.Connected)
            {
                Trace.TraceInformation($"Hub connection connected to {_hubConnection?.Url}");
            }
            else if (stateChange.NewState == ConnectionState.Disconnected)
            {
                Trace.TraceInformation($"Hub connection disconnected from {_hubConnection?.Url}");
            }
        }

        private static void SetEventStatusSent(string eventId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            UserEvent userEvent = unitOfWork.UserActivityRepository.GetEvent(eventId);

            if (userEvent != null)
            {
                userEvent.Status = UserEventStatus.Sent;
                unitOfWork.UserActivityRepository.Update(userEvent);
            }
        }

        private static void SetEventStatusFailed(string eventId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            UserEvent userEvent = unitOfWork.UserActivityRepository.GetEvent(eventId);

            if (userEvent != null && userEvent.Status != UserEventStatus.Sent)
            {
                userEvent.Status = UserEventStatus.Failed;
                unitOfWork.UserActivityRepository.Update(userEvent);
            }
        }
    }
}

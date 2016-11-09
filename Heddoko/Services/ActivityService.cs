using System;
using System.Diagnostics;
using System.Resources;
using AutoMapper;
using DAL;
using DAL.Models;
using DAL.Models.Enum;
using DAL.Models.Enums;
using DAL.Models.MongoDocuments.Notifications;
using Hangfire;
using i18n;

namespace Services
{
    public static class ActivityService
    {
        static ActivityService()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<License, License>();
            });
        }

        private static string BuildMessage(UserEvent userEvent)
        {
            switch (userEvent.Type)
            {
                case UserEventType.StreamChannelOpened:
                    return Resources.StreamChannelOpened;
                case UserEventType.StreamChannelClosed:
                    return Resources.StreamChannelClosed;
                case UserEventType.LicenseAddedToOrganization:
                case UserEventType.LicenseRemovedFromOrganization:
                case UserEventType.LicenseExpiring:
                case UserEventType.LicenseExpired:
                case UserEventType.LicenseAddedToUser:
                case UserEventType.LicenseRemovedFromUser:
                case UserEventType.LicenseChangedForUser:
                    var license = userEvent.Entity as License;

                    ResourceManager rm = new ResourceManager(typeof(Resources));
                    string message = rm.GetString(userEvent.Type.ToString());

                    if (message != null)
                    {
                        return string.Format(message, license?.Name, license?.ExpirationAt);
                    }

                    return userEvent.Type.ToString();
            }

            return string.Empty;
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void SendForTeam(int teamId, UserEventType eventType, int? entityId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Team team = unitOfWork.TeamRepository.GetFull(teamId);

            foreach (User user in team.Users)
            {
                SendNew(user, eventType, entityId, unitOfWork);
            }
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void SendNew(int userId, UserEventType eventType, int? entityId)
        {
            var unitOfWork = new UnitOfWork();
            User user = unitOfWork.UserRepository.GetFull(userId);

            SendNew(user, eventType, entityId, unitOfWork);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void Resend(string eventId)
        {
            var unitOfWork = new UnitOfWork();
            UserEvent userEvent = unitOfWork.UserActivityRepository.GetEvent(eventId);

            SendUserEvent(userEvent, unitOfWork);
        }

        private static void SendNew(User user, UserEventType eventType, int? entityId, UnitOfWork unitOfWork = null)
        {
            if (unitOfWork == null)
                unitOfWork = new UnitOfWork();

            UserEvent userEvent = new UserEvent
            {
                Created = DateTime.Now,
                ReadStatus = ReadStatus.Unread,
                UserId = user.Id,
                Status = UserEventStatus.New,
                Type = eventType,
                Entity = GetEntity(entityId, eventType, unitOfWork),
                EntityId = entityId
            };

            userEvent.Message = BuildMessage(userEvent);

            unitOfWork.UserActivityRepository.Add(userEvent);

            SendUserEvent(userEvent, unitOfWork);
        }

        private static void SendUserEvent(UserEvent userEvent, UnitOfWork unitOfWork = null)
        {
            if (unitOfWork == null)
                unitOfWork = new UnitOfWork();

            User user = unitOfWork.UserRepository.GetFull(userEvent.UserId);
            if (user.Devices.Count > 0)
            {
                userEvent.Status = UserEventStatus.Sending;
                userEvent.Updated = DateTime.Now;

                unitOfWork.UserActivityRepository.Update(userEvent);
            }
            string eventId = userEvent.IdString;
            string message = userEvent.Message;

            foreach (Device userDevice in user.Devices)
            {
                switch (userDevice.Type)
                {
                    case DeviceType.Android:
                        if (PushService.RetryGcmAfterUtc.HasValue && PushService.RetryGcmAfterUtc > DateTime.UtcNow)
                        {
                            BackgroundJob.Schedule(() => PushService.SendGcmNotification(message, userDevice.Token, userEvent.Type, eventId), PushService.RetryGcmAfterUtc.Value);
                        }
                        else
                        {
                            BackgroundJob.Enqueue(() => PushService.SendGcmNotification(message, userDevice.Token, userEvent.Type, eventId));
                        }
                        break;
                    case DeviceType.IOS:
                        BackgroundJob.Enqueue(() => PushService.SendApnsNotification(message, userDevice.Token, userEvent.Type, eventId));
                        break;
                    case DeviceType.Desktop:
                        BackgroundJob.Enqueue(() => PushService.SendDesktopNotification(message, userDevice.Token, userEvent.Type, eventId));
                        break;
                }
            }
        }

        private static object GetEntity(int? entityId, UserEventType eventType, UnitOfWork unitOfWork)
        {
            if (!entityId.HasValue)
                return null;

            switch (eventType)
            {
                case UserEventType.LicenseAddedToOrganization:
                case UserEventType.LicenseRemovedFromOrganization:
                case UserEventType.LicenseExpiring:
                case UserEventType.LicenseExpired:
                case UserEventType.LicenseAddedToUser:
                case UserEventType.LicenseRemovedFromUser:
                case UserEventType.LicenseChangedForUser:
                    License proxyLicense = unitOfWork.LicenseRepository.Get(entityId.Value);

                    return Mapper.Map<License>(proxyLicense);
                default:
                    return null;
            }
        }
    }
}

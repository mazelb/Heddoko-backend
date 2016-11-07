using System;
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
        private static string BuildMessage(UserEventType eventType)
        {
            switch (eventType)
            {
                case UserEventType.StreamChannelOpened:
                    return Resources.StreamChannelOpened;
                case UserEventType.StreamChannelClosed:
                    return Resources.StreamChannelClosed;
                default:
                    return string.Empty;
            }
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void SendForTeam(int teamId, UserEventType eventType)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Team team = unitOfWork.TeamRepository.GetFull(teamId);

            foreach (User user in team.Users)
            {
                SendNew(user, eventType, unitOfWork);
            }
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void SendNew(int userId, UserEventType eventType)
        {
            var unitOfWork = new UnitOfWork();
            User user = unitOfWork.UserRepository.GetFull(userId);

            SendNew(user, eventType, unitOfWork);
        }

        private static void SendNew(User user, UserEventType eventType, UnitOfWork unitOfWork = null)
        {
            if (unitOfWork == null)
                unitOfWork = new UnitOfWork();

            UserEvent userEvent = new UserEvent
            {
                Created = DateTime.Now,
                ReadStatus = ReadStatus.Unread,
                UserId = user.Id,
                Status = UserEventStatus.New,
                Type = eventType
            };

            unitOfWork.UserActivityRepository.Add(userEvent);

            if (user.Devices.Count > 0)
            {
                userEvent.Status = UserEventStatus.Sending;
                userEvent.Updated = DateTime.Now;

                unitOfWork.UserActivityRepository.Update(userEvent);
            }
            
            string message = BuildMessage(eventType);
            string eventId = userEvent.IdString;

            foreach (Device userDevice in user.Devices)
            {
                switch (userDevice.Type)
                {
                    case DeviceType.Android:
                        if (PushService.RetryGcmAfterUtc.HasValue && PushService.RetryGcmAfterUtc > DateTime.UtcNow)
                        {
                            BackgroundJob.Schedule(() => PushService.SendGcmNotification(message, userDevice.Token, eventType, eventId), PushService.RetryGcmAfterUtc.Value);
                        }
                        else
                        {
                            BackgroundJob.Enqueue(() => PushService.SendGcmNotification(message, userDevice.Token, eventType, eventId));
                        }
                        break;
                    case DeviceType.IOS:
                        BackgroundJob.Enqueue(() => PushService.SendApnsNotification(message, userDevice.Token, eventType, eventId));
                        break;
                    case DeviceType.Desktop:
                        BackgroundJob.Enqueue(() => PushService.SendDesktopNotification(message, userDevice.Token, eventType, eventId));
                        break;
                }
            }
        }
    }
}

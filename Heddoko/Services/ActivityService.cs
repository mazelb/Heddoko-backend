using System;
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

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyStreamChannelOpenedToTeam(int teamId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Team team = unitOfWork.TeamRepository.GetFull(teamId);

            foreach (User user in team.Users)
            {
                UserEvent userEvent = CreateUserEventWithoutEntity(user.Id, UserEventType.StreamChannelOpened, unitOfWork);

                SendUserEvent(userEvent, unitOfWork);
            }
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyStreamChannelClosedToTeam(int teamId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Team team = unitOfWork.TeamRepository.GetFull(teamId);

            foreach (User user in team.Users)
            {
                UserEvent userEvent = CreateUserEventWithoutEntity(user.Id, UserEventType.StreamChannelClosed, unitOfWork);

                SendUserEvent(userEvent, unitOfWork);
            }
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseAddedToOrganization(int organizationId, int licenseId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Organization organization = unitOfWork.OrganizationRepository.Get(organizationId);

            var userEvent = CreateLicenseEvent(licenseId, organization.UserID, UserEventType.LicenseAddedToOrganization, unitOfWork);

            SendUserEvent(userEvent, unitOfWork);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseRemovedFromOrganization(int organizationId, int licenseId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Organization organization = unitOfWork.OrganizationRepository.Get(organizationId);

            var userEvent = CreateLicenseEvent(licenseId, organization.UserID, UserEventType.LicenseRemovedFromOrganization, unitOfWork);

            SendUserEvent(userEvent, unitOfWork);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseExpiringToOrganization(int organizationId, int licenseId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Organization organization = unitOfWork.OrganizationRepository.Get(organizationId);

            var userEvent = CreateLicenseEvent(licenseId, organization.UserID, UserEventType.LicenseExpiring, unitOfWork);

            SendUserEvent(userEvent, unitOfWork);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseExpiredToOrganization(int organizationId, int licenseId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Organization organization = unitOfWork.OrganizationRepository.Get(organizationId);

            var userEvent = CreateLicenseEvent(licenseId, organization.UserID, UserEventType.LicenseExpired, unitOfWork);

            SendUserEvent(userEvent, unitOfWork);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseAddedToUser(int userId, int licenseId)
        {
            var userEvent = CreateLicenseEvent(licenseId, userId, UserEventType.LicenseAddedToUser);

            SendUserEvent(userEvent);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseRemovedFromUser(int userId, int licenseId)
        {
            var userEvent = CreateLicenseEvent(licenseId, userId, UserEventType.LicenseRemovedFromUser);

            SendUserEvent(userEvent);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseChangedForUser(int userId, int licenseId)
        {
            var userEvent = CreateLicenseEvent(licenseId, userId, UserEventType.LicenseChangedForUser);

            SendUserEvent(userEvent);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseExpiringToUser(int userId, int licenseId)
        {
            var userEvent = CreateLicenseEvent(licenseId, userId, UserEventType.LicenseExpiring);

            SendUserEvent(userEvent);
        }

        [Queue(Constants.HangFireQueue.Notifications)]
        public static void NotifyLicenseExpiredToUser(int userId, int licenseId)
        {
            var userEvent = CreateLicenseEvent(licenseId, userId, UserEventType.LicenseExpired);

            SendUserEvent(userEvent);
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

        private static UserEvent CreateUserEventWithoutEntity(int userId, UserEventType eventType, UnitOfWork unitOfWork = null)
        {
            if (unitOfWork == null)
                unitOfWork = new UnitOfWork();

            UserEvent userEvent = new UserEvent
            {
                Created = DateTime.Now,
                ReadStatus = ReadStatus.Unread,
                UserId = userId,
                Status = UserEventStatus.New,
                Type = eventType,
                Message = BuildMessage(eventType)
            };

            unitOfWork.UserActivityRepository.Add(userEvent);

            return userEvent;
        }

        private static UserEvent CreateLicenseEvent(int licenseId, int userId, UserEventType eventType, UnitOfWork unitOfWork = null)
        {
            if (unitOfWork == null)
                unitOfWork = new UnitOfWork();

            License proxyLicense = unitOfWork.LicenseRepository.Get(licenseId);

            License license = Mapper.Map<License>(proxyLicense);

            UserEvent userEvent = new UserEvent
            {
                Created = DateTime.Now,
                ReadStatus = ReadStatus.Unread,
                UserId = userId,
                Status = UserEventStatus.New,
                Type = eventType,
                Entity = license,
                EntityId = license.Id,
                Message = BuildMessageForLicence(license, eventType)
            };

            unitOfWork.UserActivityRepository.Add(userEvent);

            return userEvent;
        }

        private static string BuildMessageForLicence(License license, UserEventType eventType)
        {
            return BuildMessage(eventType, license?.Name, license?.ExpirationAt);
        }

        private static string BuildMessage(UserEventType eventType, params object[] args)
        {
            ResourceManager rm = new ResourceManager(typeof(Resources));
            string message = rm.GetString(eventType.ToString());

            if (message != null)
            {
                return string.Format(message, args);
            }

            return eventType.ToString();
        }
    }
}

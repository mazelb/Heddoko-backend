﻿/**
 * @file EmailManager.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL;
using DAL.Models;
using Hangfire;
using Services.MailSending;

namespace Services
{
    public static class EmailManager
    {
        [Queue(Constants.HangFireQueue.Email)]
        public static void SendActivationEmail(int userId, string code)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendActivationEmail(user, code);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendInviteAdminEmail(int organizationId, string inviteToken)
        {
            UnitOfWork uow = new UnitOfWork();
            Organization organization = uow.OrganizationRepository.GetFull(organizationId);

            Mailer.SendInviteAdminEmail(organization, inviteToken);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendInviteEmail(int userId, string inviteToken)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetFull(userId);

            Mailer.SendInviteEmail(user, inviteToken);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendForgotPasswordEmail(int userId, string resetPasswordToken)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendForgotPasswordEmail(user, resetPasswordToken);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendForgotUsernameEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendForgotUsernameEmail(user);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendSupportEmail(ISupportEmailViewModel model)
        {
            Mailer.SendSupportEmail(model);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendActivatedEmail(int userId)
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendActivatedEmail(user);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendLicenseExpiringToOrganization(int licenseId)
        {
            UnitOfWork uow = new UnitOfWork();
            License expiringLicense = uow.LicenseRepository.GetFull(licenseId);

            Mailer.SendLicenseExpiringOrganizationEmail(expiringLicense);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendLicenseExpiringToAdmin(int userId, int licenseId)
        {
            UnitOfWork uow = new UnitOfWork();
            License expiringLicense = uow.LicenseRepository.GetFull(licenseId);
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendLicenseExpiringAdminEmail(user, expiringLicense);
        }

        [Queue(Constants.HangFireQueue.Email)]
        public static void SendLicenseExpiringToUser(int userId, int licenseId)
        {
            UnitOfWork uow = new UnitOfWork();
            License expiringLicense = uow.LicenseRepository.GetFull(licenseId);
            User user = uow.UserRepository.GetIDCached(userId);

            Mailer.SendLicenseExpiringUserEmail(user, expiringLicense);
        }
    }
}
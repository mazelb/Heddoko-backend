/**
 * @file LicenseManager.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DAL;
using DAL.Models;
using DAL.Models.Enum;
using Hangfire;

namespace Services
{
    public static class LicenseManager
    {
        public static void Check()
        {
            // TODO - Benb - Revisit under new system
            try
            {
                HDContext context = new HDContext();
                UnitOfWork uow = new UnitOfWork(context);
                List<License> updatedLicenses = uow.LicenseRepository.Check().ToList();

                uow.Save();

                var manager = new ApplicationUserManager(new UserStore(context));

                DateTime today = DateTime.Now.StartOfDay();

                foreach (License license in updatedLicenses.Where(l => l.ExpirationAt < today))
                {
                    if (license.OrganizationID.HasValue)
                    {
                        BackgroundJob.Enqueue(() => ActivityService.NotifyLicenseExpiredToOrganization(license.OrganizationID.Value, license.Id));
                    }

                    foreach (User user in license.Users)
                    {
                        BackgroundJob.Enqueue(() => ActivityService.NotifyLicenseExpiredToUser(user.Id, license.Id));
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"LicenseManager.Check.Exception ex: {ex.GetOriginalException()}");
                throw ex;
            }
        }

        public static void CheckExpiring(int days)
        {
            try
            {
                UnitOfWork uow = new UnitOfWork();
                IEnumerable<License> expiringLicenses = uow.LicenseRepository.GetByDaysToExpire(days).ToList();

                foreach (License license in expiringLicenses)
                {
                    BackgroundJob.Enqueue(() => EmailManager.SendLicenseExpiringToOrganization(license.Id));

                    if (license.OrganizationID.HasValue)
                    {
                        BackgroundJob.Enqueue(() => ActivityService.NotifyLicenseExpiringToOrganization(license.OrganizationID.Value, license.Id));
                    }

                    foreach (User user in license.Users)
                    {
                        BackgroundJob.Enqueue(() => EmailManager.SendLicenseExpiringToUser(user.Id, license.Id));
                        BackgroundJob.Enqueue(() => ActivityService.NotifyLicenseExpiringToUser(user.Id, license.Id));
                    }
                }

            }
            catch (Exception ex)
            {
                Trace.TraceError($"LicenseManager.CheckExpiring.Exception ex: {ex.GetOriginalException()}");
                throw ex;
            }
        }

        public static void CheckExpiringForAdmins(int days)
        {
            try
            {
                UnitOfWork uow = new UnitOfWork();
                IEnumerable<License> expiringLicenses = uow.LicenseRepository.GetByDaysToExpire(days).ToList();
                IEnumerable<User> admins = uow.UserRepository.Admins();

                foreach (User admin in admins)
                {
                    foreach (License license in expiringLicenses)
                    {
                        BackgroundJob.Enqueue(() => EmailManager.SendLicenseExpiringToAdmin(admin.Id, license.Id));
                    }
                }

            }
            catch (Exception ex)
            {
                Trace.TraceError($"LicenseManager.CheckExpiringForAdmins.Exception ex: {ex.GetOriginalException()}");
                throw ex;
            }
        }
    }
}

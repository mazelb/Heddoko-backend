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
            try
            {
                HDContext context = new HDContext();
                UnitOfWork uow = new UnitOfWork(context);
                List<License> updatedLicenses = uow.LicenseRepository.Check().ToList();

                uow.Save();

                var manager = new ApplicationUserManager(new UserStore(context));
                foreach (User user in updatedLicenses.SelectMany(l => l.Users ?? new List<User>()))
                {
                    manager.ApplyUserRolesForLicense(user);
                }

                DateTime today = DateTime.Now.StartOfDay();

                foreach (License license in updatedLicenses.Where(l => l.ExpirationAt < today && l.OrganizationID.HasValue))
                {
                    Organization organization = uow.OrganizationRepository.Get(license.OrganizationID.Value);
                    BackgroundJob.Enqueue(() => ActivityService.SendNew(organization.UserID, UserEventType.LicenseExpired, license.Id));
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

                    BackgroundJob.Enqueue(() => ActivityService.SendNew(license.Organization.UserID, UserEventType.LicenseExpiring, license.Id));
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

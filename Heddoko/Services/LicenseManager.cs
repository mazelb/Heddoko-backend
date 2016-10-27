using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DAL;
using DAL.Models;

namespace Services
{
    public static class LicenseManager
    {
        public static void Check()
        {
            try
            {
                UnitOfWork uow = new UnitOfWork();
                IEnumerable<License> updatedLicenses = uow.LicenseRepository.Check();
                
                uow.Save();

                var manager = new ApplicationUserManager(new UserStore(new HDContext()));
                foreach (User user in updatedLicenses.SelectMany(l => l.Users ?? new List<User>()))
                {
                    manager.ApplyUserRolesForLicense(user);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError($"LicenseManager.Check.Exception ex: {ex.GetOriginalException()}");
                throw ex;
            }
        }
    }
}

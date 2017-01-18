/**
 * @file LicenseRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;

namespace DAL
{
    public class LicenseRepository : BaseRepository<License>, ILicenseRepository
    {
        public LicenseRepository(HDContext sb)
            : base(sb)
        {
        }

        public override IEnumerable<License> All()
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Users)
                        .OrderByDescending(c => c.ExpirationAt);
        }

        public override License GetFull(int id)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Organization.User)
                        .Include(c => c.Users)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<License> GetByOrganization(int organizationID)
        {
            return All().Where(c => c.OrganizationID == organizationID);
        }

        public IEnumerable<License> Search(string search, int? organizationID = null)
        {
            return All().Where(c => !organizationID.HasValue || c.OrganizationID.HasValue && c.OrganizationID.Value == organizationID)
                        .Where(c => (c.OrganizationID + "-" + c.Id).ToLower().Contains(search.ToLower()));
        }

        public IEnumerable<License> GetAvailableByOrganization(int organizationID, int? id = null)
        {
            DateTime today = DateTime.Now.StartOfDay();

            return DbSet.Include(c => c.Organization)
                        .Where(c => c.OrganizationID == organizationID
                                    && c.Status == LicenseStatusType.Active
                                    && (c.Users.Count() < c.Amount || c.Users.Any(p => p.Id == id))
                                    && c.ExpirationAt > today)
                        .OrderByDescending(c => c.ExpirationAt);
        }

        public IEnumerable<License> Check()
        {
            DateTime today = DateTime.Now.StartOfDay();

            IQueryable<License> expired = DbSet.Include(c => c.Users)
                                               .Where(c => c.ExpirationAt < today && c.Status != LicenseStatusType.Expired && c.Status != LicenseStatusType.Deleted);

            IQueryable<License> activated = DbSet.Include(c => c.Users)
                                                 .Where(c => c.ExpirationAt > today && (c.Status == LicenseStatusType.Expired));

            List<License> result = expired.Concat(activated).ToList();

            expired.Update(c => new License
            {
                Status = LicenseStatusType.Expired
            });

            activated.Update(c => new License
            {
                Status = LicenseStatusType.Active
            });

            return result;
        }

        public IEnumerable<License> GetByDaysToExpire(int days)
        {
            DateTime expirationDate = DateTime.Now.Date.AddDays(days);
            
            return DbSet.Include(c => c.Organization.User)
                        .Include(c => c.Users)
                        .Where(c => c.Status != LicenseStatusType.Expired && c.Status != LicenseStatusType.Deleted
                                    && DbFunctions.TruncateTime(c.ExpirationAt) == expirationDate);
        }
    }
}
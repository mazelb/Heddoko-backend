﻿using System;
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
                        .Where(c => c.OrganizationID.Value == organizationID
                                    && c.Status == LicenseStatusType.Active
                                    && (c.Users.Count() < c.Amount || c.Users.Any(p => p.Id == id))
                                    && c.ExpirationAt > today)
                        .OrderByDescending(c => c.ExpirationAt);
        }

        public IEnumerable<License> Check()
        {
            DateTime today = DateTime.Now.StartOfDay();

            IQueryable<License> expired = DbSet.Where(c => c.ExpirationAt < today && (c.Status != LicenseStatusType.Expired && c.Status != LicenseStatusType.Deleted))
                               .Include(c => c.Organization);
            
            IQueryable<License> activated = DbSet.Where(c => c.ExpirationAt > today && (c.Status == LicenseStatusType.Expired))
                               .Include(c => c.Users);

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
    }
}
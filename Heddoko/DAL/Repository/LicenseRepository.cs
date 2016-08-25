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
                        .Include(c => c.Users)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<License> GetByOrganization(int organizationID)
        {
            return All().Where(c => c.OrganizationID == organizationID);
        }

        public IEnumerable<License> Search(string search, int? organizationID = null)
        {
            return All().Where(c => !organizationID.HasValue || c.OrganizationID.HasValue && c.OrganizationID.Value == organizationID)
                        .Where(c => (c.OrganizationID + "-" + c.ID).ToLower().Contains(search.ToLower()));
        }

        public IEnumerable<License> GetAvailableByOrganization(int organizationID, int? id = null)
        {
            DateTime today = DateTime.Now.StartOfDay();

            return DbSet.Include(c => c.Organization)
                        .Where(c => c.OrganizationID.Value == organizationID
                                    && c.Status == LicenseStatusType.Active
                                    && (c.Users.Count() < c.Amount || c.Users.Any(p => p.ID == id))
                                    && c.ExpirationAt > today)
                        .OrderByDescending(c => c.ExpirationAt);
        }

        public void Check()
        {
            DateTime today = DateTime.Now.StartOfDay();

            DbSet.Where(c => c.ExpirationAt < today
                        && (c.Status != LicenseStatusType.Expired || c.Status != LicenseStatusType.Deleted)).Update(c => new License()
                        {
                            Status = LicenseStatusType.Expired
                        });

            DbSet.Where(c => c.ExpirationAt > today
                        && (c.Status == LicenseStatusType.Expired)).Update(c => new License()
                        {
                            Status = LicenseStatusType.Active
                        });
        }
    }
}
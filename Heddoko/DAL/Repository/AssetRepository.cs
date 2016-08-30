using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(HDContext sb) : base(sb)
        {
        }

        public Asset GetByImage(string name)
        {
            return DbSet.FirstOrDefault(c => c.Image.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Asset> GetRecordByOrganization(int organizationID, int take, int? skip = 0, int? userID = null)
        {
            IQueryable<Asset> query = DbSet.Include(c => c.User)
                                           .Where(c => c.Type == AssetType.Record)
                                           .Where(c => c.Status == UploadStatusType.Uploaded)
                                           .Where(c => c.User.OrganizationID == organizationID)
                                           .OrderByDescending(c => c.Created);

            if (userID.HasValue)
            {
                query = query.Where(c => c.UserID == userID);
            }

            query = query.Take(take);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            return query;
        }

        public int GetRecordByOrganizationCount(int organizationID, int? userID = null)
        {
            IQueryable<Asset> query = DbSet.Where(c => c.Type == AssetType.Record)
                                           .Where(c => c.Status == UploadStatusType.Uploaded)
                                           .Where(c => c.User.OrganizationID == organizationID);

            if (userID.HasValue)
            {
                query = query.Where(c => c.UserID == userID);
            }

            return query.Count();
        }
    }
}

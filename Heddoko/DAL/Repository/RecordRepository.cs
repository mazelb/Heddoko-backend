using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;
using DAL.Repository.Interface;

namespace DAL.Repository
{
    public class RecordRepository : BaseRepository<Record>, IRecordRepository
    {
        public RecordRepository(HDContext sb) : base(sb)
        {
        }

        public IEnumerable<Record> GetRecordByOrganization(int organizationID, int teamID, int take, int? skip, int? userID)
        {
            IQueryable<Record> query = DbSet.Include(c => c.User)
                                            .Include(c => c.Assets)
                                            .Where(c => c.User.OrganizationID == organizationID)
                                            .Where(c => c.User.TeamID == teamID)
                                            .OrderByDescending(c => c.Created);

            if (userID.HasValue)
            {
                query = query.Where(c => c.UserID == userID);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            query = query.Take(take);

            return query;
        }

        public int GetRecordByOrganizationCount(int organizationID, int teamID, int? userID = null)
        {
            IQueryable<Record> query = DbSet.Where(c => c.User.OrganizationID == organizationID)
                                            .Where(c => c.User.TeamID == teamID);

            if (userID.HasValue)
            {
                query = query.Where(c => c.UserID == userID);
            }

            return query.Count();
        }

        public IEnumerable<Record> GetRecordsByTeam(int teamId, int take, int? skip = 0)
        {
            IQueryable<Record> query = DbSet.Include(c => c.User)
                                            .Include(c => c.Assets)
                                            .Where(c => c.User.TeamID == teamId)
                                            .OrderByDescending(c => c.Created);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            query = query.Take(take);

            return query;
        }

        public int GetRecordsByTeamCount(int teamId)
        {
            return DbSet.Count(c => c.User.TeamID == teamId);
        }
    }
}

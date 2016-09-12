using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(HDContext sb)
            : base(sb)
        {
        }

        public override Team GetFull(int id)
        {
            return DbSet.Include(c => c.Organization)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Team> All(int? organizationID = null, bool isDeleted = false)
        {
            TeamStatusType status = isDeleted ? TeamStatusType.Deleted : TeamStatusType.Active;

            return DbSet.Include(c => c.Organization)
                        .Where(c => c.Status == status)
                        .Where(c => !organizationID.HasValue || c.OrganizationID == organizationID)
                        .OrderBy(c => c.Name);
        }

        public IEnumerable<Team> Search(string search, int? organizationID = null, bool isDeleted = false)
        {
            TeamStatusType status = isDeleted ? TeamStatusType.Deleted : TeamStatusType.Active;

            return DbSet.Include(c => c.Organization)
                        .Where(c => c.Status == status)
                        .Where(c => !organizationID.HasValue || c.OrganizationID == organizationID)
                        .Where(c => c.ID.ToString().ToLower().Contains(search.ToLower())
                                    || c.Name.ToLower().Contains(search.ToLower())
                                    || c.Address.ToLower().Contains(search.ToLower()));
        }

        public IEnumerable<Team> GetByOrganization(int organizationID, bool isDeleted = false)
        {
            TeamStatusType status = isDeleted ? TeamStatusType.Deleted : TeamStatusType.Active;

            return DbSet.Include(c => c.Organization)
                        .Where(c => c.Status == status)
                        .Where(c => c.OrganizationID == organizationID)
                        .OrderBy(c => c.Name);
        }
    }
}
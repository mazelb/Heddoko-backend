using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        IEnumerable<Team> GetByOrganization(int organizationID, bool isDeleted = false);

        IEnumerable<Team> All(int? organizationID = null, bool isDeleted = false);

        IEnumerable<Team> Search(string search, int? organizationID = null, bool isDeleted = false);

        IEnumerable<Team> GetAllAPI(int take, int? skip = 0);

        int GetAllAPICount();
    }
}
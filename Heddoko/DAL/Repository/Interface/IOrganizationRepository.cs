using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IOrganizationRepository : IBaseRepository<Organization>
    {
        Organization GetByName(string name);

        IEnumerable<Organization> All(bool isDeleted = false);

        IEnumerable<Organization> Search(string search, bool isDeleted = false);

        IEnumerable<Organization> GetAllAPI(int take, int? skip = 0);

        int GetAllAPICount();
    }
}
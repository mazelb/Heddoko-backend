using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IOrganizationRepository : IBaseRepository<Organization>
    {
        Organization GetByName(string name);

        Organization GetFull(int id);

        IEnumerable<Organization> All(bool isDeleted = false);

        IEnumerable<Organization> Search(string search, bool isDeleted = false);
    }
}

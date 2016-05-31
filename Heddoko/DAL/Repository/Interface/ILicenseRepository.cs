using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface ILicenseRepository : IBaseRepository<License>
    {
        IEnumerable<License> GetByOrganization(int organizationID);

        License GetFull(int value);

        IEnumerable<License> Search(string search, int? organizationID = null);

        IEnumerable<License> GetAvailableByOrganization(int organizationID);
    }
}

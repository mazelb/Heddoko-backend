using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface ILicenseRepository : IBaseRepository<License>
    {
        IEnumerable<License> GetByOrganization(int organizationID);

        IEnumerable<License> Search(string search, int? organizationID = null);

        IEnumerable<License> GetAvailableByOrganization(int organizationID);
    }
}

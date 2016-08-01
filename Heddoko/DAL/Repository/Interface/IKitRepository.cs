using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IKitRepository : IBaseRepository<Kit>
    {
        void RemoveBrainpack(int iD);
        IEnumerable<Kit> Search(string value, bool isDeleted, int? organizationID = null);
        IEnumerable<Kit> All(bool isDeleted, int? organizationID = null);
        IEnumerable<Kit> GetAvailable(int? usedID, int? organizationID = null);
    }
}
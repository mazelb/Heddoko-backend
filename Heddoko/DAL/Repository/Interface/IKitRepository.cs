using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IKitRepository : IBaseRepository<Kit>
    {
        void RemoveBrainpack(int iD);
        IEnumerable<Kit> GetAvailable(int? usedID);
        IEnumerable<Kit> Search(string value, bool isDeleted);
        IEnumerable<Kit> All(bool isDeleted);
    }
}
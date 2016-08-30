using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IShirtRepository : IBaseRepository<Shirt>
    {
        IEnumerable<Shirt> Search(string value, int? statusFilter = null, bool isDeleted = false);
        IEnumerable<Shirt> All(bool isDeleted);
        void RemoveShirtOctopi(int shirtOctopiID);
        IEnumerable<Shirt> GetAvailable(int? usedID);
        int GetNumReady();
    }
}
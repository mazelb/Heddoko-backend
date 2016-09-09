using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IPantsRepository : IBaseRepository<Pants>, IHistoryRepository<Pants>
    {
        IEnumerable<Pants> Search(string value, int? statusFilter = null, bool isDeleted = false);

        IEnumerable<Pants> All(bool isDeleted);

        void RemovePantsOctopi(int pantsOctopiID);

        IEnumerable<Pants> GetAvailable(int? usedID);
        int GetNumReady();
    }
}
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IBrainpackRepository : IBaseRepository<Brainpack>, IHistoryRepository<Brainpack>
    {
        Brainpack Get(string label);
        void RemoveDataboard(int databoardID);
        void RemovePowerboard(int id);
        IEnumerable<Brainpack> GetAvailable(int? usedID);
        IEnumerable<Brainpack> Search(string value, int? statusFilter = null, bool isDeleted = false);
        IEnumerable<Brainpack> All(bool isDeleted);
        int GetNumReady();
    }
}
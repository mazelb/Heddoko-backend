using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IBrainpackRepository : IBaseRepository<Brainpack>, IHistoryRepository<Brainpack>
    {
        void RemoveDataboard(int databoardID);
        void RemovePowerboard(int id);
        IEnumerable<Brainpack> GetAvailable(int? usedID);
        IEnumerable<Brainpack> Search(string value, bool isDeleted);
        IEnumerable<Brainpack> All(bool isDeleted);
        int GetNumReady();
    }
}
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IDataboardRepository : IBaseRepository<Databoard>, IHistoryRepository<Databoard>
    {
        Databoard Get(string label);
        IEnumerable<Databoard> All(bool isDeleted);
        IEnumerable<Databoard> Search(string value, bool isDeleted);
        IEnumerable<Databoard> GetAvailable(int? usedID);
        int GetNumReady();
    }
}
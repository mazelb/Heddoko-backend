using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IPowerboardRepository : IBaseRepository<Powerboard>, IHistoryRepository<Powerboard>
    {
        IEnumerable<Powerboard> Search(string value, bool isDeleted);
        IEnumerable<Powerboard> GetAvailable(int? usedID);
        IEnumerable<Powerboard> All(bool isDeleted);
        int GetNumReady();
    }
}
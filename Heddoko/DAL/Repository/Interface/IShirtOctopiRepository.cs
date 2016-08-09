using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IShirtOctopiRepository : IBaseRepository<ShirtOctopi>
    {
        IEnumerable<ShirtOctopi> All(bool isDeleted);
        IEnumerable<ShirtOctopi> Search(string value, bool isDeleted);
        IEnumerable<ShirtOctopi> GetAvailable(int? id = null);
        int GetNumReady();
    }
}
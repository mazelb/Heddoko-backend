using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IShirtOctopiRepository : IBaseRepository<ShirtOctopi>
    {
        IEnumerable<ShirtOctopi> All(bool isDeleted);
        IEnumerable<ShirtOctopi> Search(string value, bool isDeleted);
        IEnumerable<ShirtOctopi> GetAvailable(int? id = null);
    }
}

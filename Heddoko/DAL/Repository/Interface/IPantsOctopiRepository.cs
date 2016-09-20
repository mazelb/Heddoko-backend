using System.Collections.Generic;
using DAL.Models;
using Z.EntityFramework.Plus;

namespace DAL
{
    public interface IPantsOctopiRepository : IBaseRepository<PantsOctopi>, IHistoryRepository<PantsOctopi>
    {
        IEnumerable<PantsOctopi> All(bool isDeleted);
        IEnumerable<PantsOctopi> Search(string value, bool isDeleted);
        IEnumerable<PantsOctopi> GetAvailable(int? id = null);
        int GetNumReady();
    }
}
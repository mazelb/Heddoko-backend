using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IPantsOctopiRepository : IBaseRepository<PantsOctopi>
    {
        IEnumerable<PantsOctopi> All(bool isDeleted);
        IEnumerable<PantsOctopi> Search(string value, bool isDeleted);
        IEnumerable<PantsOctopi> GetAvailable(int? id = null);
    }
}
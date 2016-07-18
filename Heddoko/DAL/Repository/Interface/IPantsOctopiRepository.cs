using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IPantsOctopiRepository : IBaseRepository<PantsOctopi>
    {
        IEnumerable<PantsOctopi> All(bool isDeleted);
        IEnumerable<PantsOctopi> Search(string value, bool isDeleted);
    }
}

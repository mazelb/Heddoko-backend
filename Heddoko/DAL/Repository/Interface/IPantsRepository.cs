using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IPantsRepository : IBaseRepository<Pants>
    {
        IEnumerable<Pants> Search(string value, bool isDeleted);

        IEnumerable<Pants> All(bool isDeleted);

        void RemovePantsOctopi(int pantsOctopiID);

        IEnumerable<Pants> GetAvailable(int? usedID);
    }
}
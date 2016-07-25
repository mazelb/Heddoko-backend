using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IShirtRepository : IBaseRepository<Shirt>
    {
        IEnumerable<Shirt> Search(string value, bool isDeleted);
        IEnumerable<Shirt> All(bool isDeleted);

        void RemoveShirtOctopi(int shirtOctopiID);
    }
}
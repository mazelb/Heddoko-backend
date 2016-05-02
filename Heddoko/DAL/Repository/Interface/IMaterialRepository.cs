using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IMaterialRepository : IBaseRepository<Material>
    {
        Material GetByName(string name);
        IEnumerable<Material> Search(string value);
    }
}

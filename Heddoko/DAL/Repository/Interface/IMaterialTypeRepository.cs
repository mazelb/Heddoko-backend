using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IMaterialTypeRepository : IBaseRepository<MaterialType>
    {
        MaterialType GetByName(string name);
        IEnumerable<MaterialType> Search(string value);
    }
}

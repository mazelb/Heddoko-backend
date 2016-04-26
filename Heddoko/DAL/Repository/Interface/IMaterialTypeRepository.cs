using DAL.Models;

namespace DAL
{
    public interface IMaterialTypeRepository : IBaseRepository<MaterialType>
    {
        MaterialType GetByName(string name);
    }
}

using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class MaterialTypeRepository : BaseRepository<MaterialType>, IMaterialTypeRepository
    {
        public MaterialTypeRepository(HDContext sb) : base(sb)
        {
        }
    }
}

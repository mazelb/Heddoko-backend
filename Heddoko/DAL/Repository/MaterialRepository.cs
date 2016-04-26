using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(HDContext sb) : base(sb)
        {
        }
    }
}

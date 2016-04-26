using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class EquipmentRepository : BaseRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(HDContext sb) : base(sb)
        {
        }
    }
}

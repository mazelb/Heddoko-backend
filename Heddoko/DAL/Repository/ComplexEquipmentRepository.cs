using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class ComplexEquipmentRepository : BaseRepository<ComplexEquipment>, IComplexEquipmentRepository
    {
        public ComplexEquipmentRepository(HDContext sb) : base(sb)
        {
        }
    }
}

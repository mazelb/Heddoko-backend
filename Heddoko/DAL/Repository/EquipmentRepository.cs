using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class EquipmentRepository : BaseRepository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(HDContext sb) : base(sb)
        {
        }

        public override IEnumerable<Equipment> All()
        {
            return DbSet.Include(c => c.Material)
                        .Include(c => c.VerifiedBy)
                        .OrderBy(c => c.SerialNo);
        }

        public IEnumerable<Equipment> Search(string value)
        {
            return All().Where(c => c.SerialNo.Contains(value)
                                 || c.PhysicalLocation.Contains(value)
                                 || c.MacAddress.Contains(value));
        }
    }
}

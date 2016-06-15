using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class ComplexEquipmentRepository : BaseRepository<ComplexEquipment>, IComplexEquipmentRepository
    {
        public ComplexEquipmentRepository(HDContext sb) : base(sb)
        {
        }

        public override IEnumerable<ComplexEquipment> All()
        {
            return DbSet.Include(c => c.Equipments)
                        .OrderBy(c => c.MacAddress)
                        .OrderBy(c => c.SerialNo);
        }

        public ComplexEquipment GetFull(int value)
        {
            return DbSet.Include(c => c.Equipments).FirstOrDefault(c => c.ID == value);
        }

        public IEnumerable<ComplexEquipment> Search(string value)
        {
            return All().Where(c => c.MacAddress.ToLower().Contains(value.ToLower())
                                 || c.SerialNo.ToLower().Contains(value.ToLower())
                                 || c.PhysicalLocation.ToLower().Contains(value.ToLower()));
        }
    }
}

using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using EntityFramework.Extensions;

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

        public void RemoveComplexEquipment(int complexEquipmentID)
        {
            DbSet.Where(c => c.ComplexEquipmentID == complexEquipmentID)
                 .Update(u => new Equipment
                 {
                     ComplexEquipmentID = null
                 });
        }

        public IEnumerable<Equipment> GetByComplexEquipment(int complexEquipmentID)
        {
            return All().Where(c => c.ComplexEquipmentID == complexEquipmentID);
        }
    }
}

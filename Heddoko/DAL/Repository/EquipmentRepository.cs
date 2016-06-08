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
            return All().Where(c => c.SerialNo.ToLower().Contains(value.ToLower())
                                 || c.PhysicalLocation.ToLower().Contains(value.ToLower())
                                 || c.MacAddress.ToLower().Contains(value.ToLower()));
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

        public Equipment GetFull(int id)
        {
           return  DbSet.Include(c => c.Material)
                        .Include(c => c.VerifiedBy)
                        .Include(c => c.ComplexEquipment)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Equipment> GetEmpty()
        {
            return DbSet.Where(c => c.ComplexEquipmentID == null)
                        .OrderBy(c => c.SerialNo);
        }

        public IEnumerable<Equipment> GetBySerialSearch(string value)
        {
            return DbSet.Where(c => c.ComplexEquipmentID == null
                                 && (c.SerialNo.ToLower().Contains(value.ToLower())
                                 || c.PhysicalLocation.ToLower().Contains(value.ToLower())
                                 || c.MacAddress.ToLower().Contains(value.ToLower())));
        }

        public Equipment GetBySerialNo(string serialNo)
        {
            return DbSet.Where(c => c.SerialNo.Equals(serialNo, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();
        }
    }
}

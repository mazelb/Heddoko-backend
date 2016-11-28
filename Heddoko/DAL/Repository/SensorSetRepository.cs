using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class SensorSetRepository : BaseRepository<SensorSet>, ISensorSetRepository
    {
        public SensorSetRepository(HDContext sb)
            : base(sb)
        {
        }

        public IEnumerable<SensorSet> All(bool isDeleted)
        {
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<SensorSet> GetAvailable(int? id = null)
        {
            return DbSet
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<SensorSet> Search(string search, int? statusFilter, bool isDeleted = false)
        {
            IQueryable<SensorSet> query = DbSet.Include(c => c.Id);

            if (!string.IsNullOrEmpty(search))
            {
                int? id = search.ParseID();
                query = query.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                                .Where(c => (c.Id == id)
                                    || c.Label.ToLower().Contains(search.ToLower())
                                    || c.Notes.ToLower().Contains(search.ToLower()));
            }
            if (statusFilter.HasValue)
            {
                query = query.Where(c => c.Status == (EquipmentStatusType)statusFilter);
            }
            query = query.OrderBy(c => c.Id);

            return query;
        }

        public override SensorSet GetFull(int id)
        {
            return DbSet.Include(c => c.Kits)
                        .Include(c => c.Sensors)
                        .FirstOrDefault(c => c.Id == id);
        }
    }
}
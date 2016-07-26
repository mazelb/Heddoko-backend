using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class SensorRepository : BaseRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(HDContext sb)
            : base(sb)
        {
        }

        public IEnumerable<Sensor> All(bool isDeleted)
        {
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Sensor> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
            //TODO: BENB - Need to check against sensorsets
        }

        public IEnumerable<Sensor> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet
                .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                .Where(c => (c.ID == id)
                            || c.Location.ToLower().Contains(search.ToLower())
                            || c.Type.ToString().ToLower().Contains(search.ToLower()))
                .OrderBy(c => c.ID);
        }
    }
}
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using DAL.Models;
using EntityFramework.Extensions;
using System.Text.RegularExpressions;

namespace DAL
{
    public class SensorRepository : BaseRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(HDContext sb)
            : base(sb)
        {
        }

        public override Sensor GetFull(int id)
        {
            return DbSet.Include(c => c.Firmware)
                        .Include(c => c.SensorSet)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Sensor> All(bool isDeleted)
        {
            return DbSet.Include(c => c.Firmware)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Sensor> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.SensorSetID == null)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Sensor> Search(string search, int? statusFilter = null, bool isDeleted = false)
        {
            IQueryable<Sensor> query = DbSet
                        .Include(c => c.Firmware);

            if (!string.IsNullOrEmpty(search))
            {
                int? id = search.ParseID();
                query = query.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                               .Where(c => (c.ID == id)
                                   || c.Location.ToLower().Contains(search.ToLower())
                                   || c.Type.ToString().ToLower().Contains(search.ToLower())
                                   || c.Label.ToLower().Contains(search.ToLower())
                                   || c.Notes.ToLower().Contains(search.ToLower()));
            }
            if (statusFilter.HasValue)
            {
                query = query.Where(c => c.Status == (EquipmentStatusType)statusFilter);
            }          
            query = query.OrderBy(c => c.ID);

            return query;
        }

        public void RemoveSensorSet(int sensorSetID)
        {
            DbSet.Where(c => c.SensorSetID == sensorSetID).Update(u => new Sensor { SensorSetID = null });

        }

        public IEnumerable<Sensor> GetBySensorSet(int sensorSetID)
        {
            return DbSet.Where(c => c.SensorSetID == sensorSetID)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Sensor> SearchAvailable(string search)
        {
            int? id = search.ParseID();
            return DbSet
                .Where(c => c.Status != EquipmentStatusType.Trash)
                .Where(c => c.SensorSet == null)
                .Where(c => (c.ID == id)
                            || c.ID.ToString().ToLower().Contains(search.ToLower())
                            || c.Location.ToLower().Contains(search.ToLower())
                            || c.Type.ToString().ToLower().Contains(search.ToLower()))
                .OrderBy(c => c.ID);
        }

        public int GetNumReady()
        {
            return DbSet.Where(c => c.Status == EquipmentStatusType.Ready).Count();
        }
    }
}
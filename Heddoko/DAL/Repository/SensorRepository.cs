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

        public void RemoveSensorSet(int sensorSetID)
        {
            DbSet.Where(c => c.SensorSetID == sensorSetID).Update(u => new Sensor { SensorSetID = null });

        }

        public IEnumerable<Sensor> GetBySensorSet(int sensorSetID)
        {
            return All().Where(c => c.SensorSetID == sensorSetID);
        }

        public Sensor GetByIDView(string idView)
        {
            //TODO - BENB - replace with this when IDView is editable or used in the database
            //return DbSet.Where(c => c.IDView.Equals(idView, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            idView = Regex.Replace(idView, "[^0-9]", "");
            int id;
            if (Int32.TryParse(idView, out id))
            {
                return DbSet.Where(c => c.ID == id).FirstOrDefault();
            }
            return null;
        }
    }
}
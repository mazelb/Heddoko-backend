using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;

namespace DAL
{
    public class BrainpackRepository : BaseRepository<Brainpack>, IBrainpackRepository
    {
        public BrainpackRepository(HDContext sb)
            : base(sb)
        {
        }


        public override Brainpack GetFull(int id)
        {
            return DbSet.Include(c => c.Firmware)
                        .Include(c => c.Databoard)
                        .Include(c => c.Kits)
                        .Include(c => c.Powerboard)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Brainpack> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Kits.Count == 0 || c.Kits.Any(p => p.ID == id))
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Brainpack> All(bool isDeleted)
        {
            return DbSet.Include(c => c.Firmware)
                        .Include(c => c.Databoard)
                        .Include(c => c.Powerboard)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Brainpack> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Include(c => c.Firmware)
                        .Include(c => c.Databoard)
                        .Include(c => c.Powerboard)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                    || c.Version.ToString().ToLower().Contains(search.ToLower())
                                    || c.Location.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.ID);
        }


        public void RemoveDataboard(int databoardID)
        {
            DbSet.Where(c => c.DataboardID.Value == databoardID).Update(c => new Brainpack()
            {
                DataboardID = null
            });
        }

        public void RemovePowerboard(int powerboardID)
        {
            DbSet.Where(c => c.PowerboardID.Value == powerboardID).Update(c => new Brainpack()
            {
                PowerboardID = null
            });
        }
    }
}
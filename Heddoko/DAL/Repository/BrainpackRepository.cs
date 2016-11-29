using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;
using System.Collections;

namespace DAL
{
    public class BrainpackRepository : BaseRepository<Brainpack>, IBrainpackRepository
    {
        public BrainpackRepository(HDContext sb)
            : base(sb)
        {
        }

        public Brainpack Get(string label)
        {
            return DbSet.FirstOrDefault(c => c.Label.Equals(label, StringComparison.OrdinalIgnoreCase));
        }

        public override Brainpack GetFull(int id)
        {
            return DbSet.Include(c => c.Firmware)
                        .Include(c => c.Databoard)
                        .Include(c => c.Kits)
                        .Include(c => c.Powerboard)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Brainpack> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Kits.Count == 0 || c.Kits.Any(p => p.Id == id))
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<Brainpack> All(bool isDeleted)
        {
            return DbSet.Include(c => c.Firmware)
                        .Include(c => c.Databoard)
                        .Include(c => c.Powerboard)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<Brainpack> Search(string search, int? statusFilter = null, bool isDeleted = false)
        {
            IQueryable<Brainpack> query = DbSet.Include(c => c.Firmware)
                                               .Include(c => c.Databoard)
                                               .Include(c => c.Powerboard);

            if (!string.IsNullOrEmpty(search))
            {
                int? id = search.ParseID();
                query = query.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                             .Where(c => (c.Id == id)
                                    || c.Version.ToString().ToLower().Contains(search.ToLower())
                                    || c.Location.ToLower().Contains(search.ToLower())
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

        public int GetNumReady()
        {
            return DbSet.Where(c => c.Status == EquipmentStatusType.Ready).Count();
        }

    }
}
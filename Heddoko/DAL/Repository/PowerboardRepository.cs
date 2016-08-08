﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class PowerboardRepository : BaseRepository<Powerboard>, IPowerboardRepository
    {
        public PowerboardRepository(HDContext sb)
            : base(sb)
        {
        }


        public override Powerboard GetFull(int id)
        {
            return DbSet.Include(c => c.Firmware)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Powerboard> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Brainpack.Count == 0 || c.Brainpack.Any(p => p.ID == id))
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Powerboard> All(bool isDeleted)
        {
            return DbSet.Include(c => c.Firmware)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Powerboard> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Include(c => c.Firmware)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                    || c.Version.ToString().ToLower().Contains(search.ToLower())
                                    || c.Location.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.ID);
        }
    }
}
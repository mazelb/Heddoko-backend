﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;

namespace DAL
{
    public class PantsRepository : BaseRepository<Pants>, IPantsRepository
    {
        public PantsRepository(HDContext sb)
            : base(sb)
        {
        }

        public override Pants GetFull(int id)
        {
            return DbSet.Include(c => c.PantsOctopi)
                        .Include(c => c.Kit)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Pants> All(bool isDeleted)
        {
            return DbSet.Include(c => c.PantsOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Pants> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Kit.Count == 0 || c.Kit.Any(p => p.ID == id))
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Pants> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Include(c => c.PantsOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                    || c.Size.ToString().ToLower().Contains(search.ToLower())
                                    || c.Location.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.ID);
        }

        public void RemovePantsOctopi(int pantsOctopiID)
        {
            DbSet.Where(c => c.PantsOctopiID.Value == pantsOctopiID).Update(c => new Pants
                                                                                 {
                                                                                     PantsOctopiID = null
                                                                                 });
        }
    }
}
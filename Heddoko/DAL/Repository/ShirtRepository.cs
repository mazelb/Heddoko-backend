﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;

namespace DAL
{
    public class ShirtRepository : BaseRepository<Shirt>, IShirtRepository
    {
        public ShirtRepository(HDContext sb)
            : base(sb)
        {
        }

        public override Shirt GetFull(int id)
        {
            return DbSet.Include(c => c.ShirtOctopi)
                        .Include(c => c.Kit)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Shirt> All(bool isDeleted)
        {
            return DbSet.Include(c => c.ShirtOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Shirt> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Kit.Count == 0 || c.Kit.Any(p => p.ID == id))
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Shirt> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Include(c => c.ShirtOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                    || c.Size.ToString().ToLower().Contains(search.ToLower())
                                    || c.Location.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.ID);
        }

        public void RemoveShirtOctopi(int shirtOctopiID)
        {
            DbSet.Where(c => c.ShirtOctopiID.Value == shirtOctopiID).Update(c => new Shirt
            {
                ShirtOctopiID = null
            });
        }

        public int GetNumReady()
        {
            IEnumerable<Shirt> shirts = DbSet.Where(c => c.Status == EquipmentStatusType.Ready);

            if (shirts != null)
            {
                return shirts.Count();
            }

            return 0;
        }
    }
}
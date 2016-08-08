﻿using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;
using System.Data.Entity;

namespace DAL
{
    public class KitRepository : BaseRepository<Kit>, IKitRepository
    {
        public KitRepository(HDContext sb)
            : base(sb)
        {
        }


        public void RemoveBrainpack(int brainpackID)
        {
            DbSet.Where(c => c.BrainpackID.Value == brainpackID).Update(c => new Kit()
            {
                BrainpackID = null
            });
        }


        public override Kit GetFull(int id)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Brainpack)
                        .Include(c => c.SensorSet)
                        .Include(c => c.Pants)
                        .Include(c => c.Shirt)
                        .Include(c => c.User)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Kit> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.User == null || c.UserID  == id)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Kit> All(bool isDeleted)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Brainpack)
                        .Include(c => c.SensorSet)
                        .Include(c => c.Pants)
                        .Include(c => c.Shirt)
                        .Include(c => c.User)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Kit> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Brainpack)
                        .Include(c => c.SensorSet)
                        .Include(c => c.Pants)
                        .Include(c => c.Shirt)
                        .Include(c => c.User)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                    || c.Location.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.ID);
        }
    }
}
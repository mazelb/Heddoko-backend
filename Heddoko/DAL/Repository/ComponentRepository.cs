﻿using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using EntityFramework.Extensions;
using System.Text.RegularExpressions;

namespace DAL
{
    public class ComponentRepository : BaseRepository<Component>, IComponentRepository
    {
        public ComponentRepository(HDContext sb)
            : base(sb)
        {
        }

        public IEnumerable<Component> All(bool isDeleted)
        {
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Component> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Component> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                 || (c.Type.ToString().ToLower().Contains(search.ToLower())))
                        .OrderBy(c => c.ID);
        }
    }
}
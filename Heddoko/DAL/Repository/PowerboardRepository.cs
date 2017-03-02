/**
 * @file PowerboardRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;
using System;

namespace DAL
{
    public class PowerboardRepository : BaseRepository<Powerboard>, IPowerboardRepository
    {
        public PowerboardRepository(HDContext sb)
            : base(sb)
        {
        }

        public Powerboard Get(string label)
        {
            return DbSet.FirstOrDefault(c => c.Label.Equals(label, StringComparison.OrdinalIgnoreCase));
        }

        public override Powerboard GetFull(int id)
        {
            return DbSet.Include(c => c.Firmware)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Powerboard> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Brainpacks.Count == 0 || c.Brainpacks.Any(p => p.Id == id))
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<Powerboard> All(bool isDeleted)
        {
            return DbSet.Include(c => c.Firmware)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<Powerboard> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Include(c => c.Firmware)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.Id == id)
                                    || c.Version.ToString().ToLower().Contains(search.ToLower())
                                    || c.Location.ToLower().Contains(search.ToLower())
                                    || c.Label.ToLower().Contains(search.ToLower())
                                    || c.Notes.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.Id);
        }

        public int GetNumReady()
        {
            return DbSet.Where(c => c.Status == EquipmentStatusType.Ready).Count();
        }
    }
}
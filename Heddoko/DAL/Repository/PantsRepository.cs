/**
 * @file PantsRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
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
                        .Include(c => c.Kits)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Pants> All(bool isDeleted)
        {
            return DbSet.Include(c => c.PantsOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<Pants> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Kits.Count == 0 || c.Kits.Any(p => p.Id == id))
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<Pants> Search(string search, int? statusFilter = null, bool isDeleted = false)
        {
            IQueryable<Pants> query = DbSet.Include(c => c.PantsOctopi);
                        
            if (!string.IsNullOrEmpty(search))
            {
                int? id = search.ParseID();
                query = query   .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)                               
                                .Where(c => (c.Id == id)
                                            || c.Size.ToString().ToLower().Contains(search.ToLower())
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

        public void RemovePantsOctopi(int pantsOctopiID)
        {
            DbSet.Where(c => c.PantsOctopiID.Value == pantsOctopiID).Update(c => new Pants
                                                                                 {
                                                                                     PantsOctopiID = null
                                                                                 });
        }

        public int GetNumReady()
        {
            return DbSet.Where(c => c.Status == EquipmentStatusType.Ready).Count();
        }
    }
}
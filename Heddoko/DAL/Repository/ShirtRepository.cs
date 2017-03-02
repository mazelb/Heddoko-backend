/**
 * @file ShirtRepository.cs
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
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Shirt> All(bool isDeleted)
        {
            return DbSet.Include(c => c.ShirtOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<Shirt> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Kit.Count == 0 || c.Kit.Any(p => p.Id == id))
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<Shirt> Search(string search, int? statusFilter = null, bool isDeleted = false)
        {           
            IQueryable<Shirt> query = DbSet.Include(c => c.ShirtOctopi);

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

        public void RemoveShirtOctopi(int shirtOctopiID)
        {
            DbSet.Where(c => c.ShirtOctopiID.Value == shirtOctopiID).Update(c => new Shirt
            {
                ShirtOctopiID = null
            });
        }

        public int GetNumReady()
        {
            return DbSet.Where(c => c.Status == EquipmentStatusType.Ready).Count();
        }
    }
}
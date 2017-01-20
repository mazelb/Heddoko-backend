/**
 * @file ShirtOctopiRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class ShirtOctopiRepository : BaseRepository<ShirtOctopi>, IShirtOctopiRepository
    {
        public ShirtOctopiRepository(HDContext sb)
            : base(sb)
        {
        }

        public IEnumerable<ShirtOctopi> All(bool isDeleted)
        {
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<ShirtOctopi> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Shirt.Count == 0 || c.Shirt.Any(p => p.Id == id))
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<ShirtOctopi> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.Id == id)
                                    || c.Size.ToString().ToLower().Contains(search.ToLower())
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
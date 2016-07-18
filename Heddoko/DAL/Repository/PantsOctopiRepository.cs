using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace DAL
{
    public class PantsOctopiRepository : BaseRepository<PantsOctopi>, IPantsOctopiRepository
    {
        public PantsOctopiRepository(HDContext sb) : base(sb)
        {
        }

        public IEnumerable<PantsOctopi> All(bool isDeleted)
        {
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<PantsOctopi> Search(string search, bool isDeleted = false)
        {
            return DbSet
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID.ToString().ToLower().Contains(search.ToLower()))
                                 || (c.Size.ToString().ToLower().Contains(search.ToLower())))
                        .OrderBy(c => c.ID);
        }
    }
}

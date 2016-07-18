using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace DAL
{
    public class PantsOctopiRepository : BaseRepository<PantsOctopi>, IPantsOctopiRepository
    {
        public PantsOctopiRepository(HDContext sb) : base(sb)
        {
        }

        public override PantsOctopi GetFull(int id)
        {
            return DbSet.Include(c => c.Pants)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<PantsOctopi> All(bool isDeleted)
        {
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<PantsOctopi> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Pants.Count == 0 || c.Pants.Any(p => p.ID == id))
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<PantsOctopi> Search(string search, bool isDeleted = false)
        {
            string tmp = Regex.Replace(search, "[^0-9]+", string.Empty);
            int id = 0;
            int.TryParse(tmp, out id);
            return DbSet
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                 || (c.Size.ToString().ToLower().Contains(search.ToLower()))
                                 || (c.Location.ToLower().Contains(search.ToLower())))
                        .OrderBy(c => c.ID);
        }
    }
}

using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using EntityFramework.Extensions;
using System.Text.RegularExpressions;

namespace DAL
{
    public class PantsRepository : BaseRepository<Pants>, IPantsRepository
    {
        public PantsRepository(HDContext sb) : base(sb)
        {
        }

        public override Pants GetFull(int id)
        {
            return DbSet.Include(c => c.PantsOctopi)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Pants> All(bool isDeleted)
        {
            return DbSet.Include(c => c.PantsOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Pants> Search(string search, bool isDeleted = false)
        {
            string tmp = Regex.Replace(search, "[^0-9]+", string.Empty);
            int id = 0;
            int.TryParse(tmp, out id);
            return DbSet.Include(c => c.PantsOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                 || (c.Size.ToString().ToLower().Contains(search.ToLower()))
                                 || (c.Location.ToLower().Contains(search.ToLower())))
                        .OrderBy(c => c.ID);
        }

        public void RemovePantsOctopi(int pantsOctopiID)
        {
            DbSet.Where(c => c.PantsOctopiID.Value == pantsOctopiID).Update(c => new Pants()
            {
                PantsOctopiID = null
            });
        }
    }
}

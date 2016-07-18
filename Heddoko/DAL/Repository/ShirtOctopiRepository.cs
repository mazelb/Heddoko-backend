using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DAL
{
    public class ShirtOctopiRepository : BaseRepository<ShirtOctopi>, IShirtOctopiRepository
    {
        public ShirtOctopiRepository(HDContext sb) : base(sb)
        {
        }

        public IEnumerable<ShirtOctopi> All(bool isDeleted)
        {
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                .OrderBy(c => c.ID);
        }

        /*public IEnumerable<PantsOctopi> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Shirt.Count == 0 || c.Shirt.Any(p => p.ID == id))
                        .OrderBy(c => c.ID);
        }*/

        public IEnumerable<ShirtOctopi> Search(string search, bool isDeleted = false)
        {
            return DbSet
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID.ToString().ToLower().Contains(search.ToLower()))
                                 || (c.Size.ToString().ToLower().Contains(search.ToLower())))
                        .OrderBy(c => c.ID);

        }
    }
}

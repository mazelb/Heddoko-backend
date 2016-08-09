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
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<ShirtOctopi> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.Shirt.Count == 0 || c.Shirt.Any(p => p.ID == id))
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<ShirtOctopi> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                    || c.Size.ToString().ToLower().Contains(search.ToLower())
                                    || c.Location.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.ID);
        }
    }
}
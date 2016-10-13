using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class PantsOctopiRepository : BaseRepository<PantsOctopi>, IPantsOctopiRepository
    {
        public PantsOctopiRepository(HDContext sb)
            : base(sb)
        {
        }

        public override PantsOctopi GetFull(int id)
        {
            return DbSet.Include(c => c.PantsCollection)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<PantsOctopi> All(bool isDeleted)
        {
            return DbSet.Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<PantsOctopi> GetAvailable(int? id = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.PantsCollection.Count == 0 || c.PantsCollection.Any(p => p.Id == id))
                        .OrderBy(c => c.Id);
        }

        public IEnumerable<PantsOctopi> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet
                .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
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
using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using EntityFramework.Extensions;
using System.Text.RegularExpressions;

namespace DAL
{
    public class ShirtRepository : BaseRepository<Shirt>, IShirtRepository
    {
        public ShirtRepository(HDContext sb) : base(sb)
        {
        }

        public override Shirt GetFull(int id)
        {
            return DbSet.Include(c => c.ShirtOctopi)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Shirt> All(bool isDeleted)
        {
            return DbSet.Include(c => c.ShirtOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Shirt> Search(string search, bool isDeleted = false)
        {
            string tmp = Regex.Replace(search, "[^0-9]+", string.Empty);
            int id = 0;
            int.TryParse(tmp, out id);
            return DbSet.Include(c => c.ShirtOctopi)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => (c.ID == id)
                                 || (c.Size.ToString().ToLower().Contains(search.ToLower()))
                                 || (c.Location.ToLower().Contains(search.ToLower())))
                        .OrderBy(c => c.ID);
        }

        public void RemoveShirtOctopi(int shirtOctopiID)
        {
            DbSet.Where(c => c.ShirtOctopiID.Value == shirtOctopiID).Update(c => new Shirt()
            {
                ShirtOctopiID = null
            });
        }

    }
}

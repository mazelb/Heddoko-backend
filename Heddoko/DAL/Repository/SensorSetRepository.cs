using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class SensorSetRepository : BaseRepository<SensorSet>, ISensorSetRepository
    {
        public SensorSetRepository(HDContext sb)
            : base(sb)
        {
        }

        public IEnumerable<SensorSet> All(bool isDeleted)
        {
            return DbSet
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<SensorSet> GetAvailable(int? id = null)
        {
            return DbSet
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<SensorSet> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet
                .Where(c => (c.ID == id))
                          //  || c.KitID.ToLower().Contains(search.ToLower()))
                .OrderBy(c => c.ID);
        }

        public override SensorSet GetFull(int id)
        {
            return DbSet.Include(c => c.Kits)
                        .Include(c => c.Sensors)
                        .FirstOrDefault(c => c.ID == id);
        }
    }
}
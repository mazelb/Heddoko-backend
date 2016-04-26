using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;

namespace DAL
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(HDContext sb) : base(sb)
        {
        }

        public Material GetByName(string name)
        {
            return DbSet.Where(c => c.Name == name).FirstOrDefault();
        }
    }
}

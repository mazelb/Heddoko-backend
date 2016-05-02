using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
    {
        public MaterialRepository(HDContext sb) : base(sb)
        {
        }


        public override IEnumerable<Material> All()
        {
            return DbSet.Include(c => c.MaterialType)
                        .OrderBy(c => c.Name);
        }

        public Material GetByName(string name)
        {
            return DbSet.Where(c => c.Name == name).FirstOrDefault();
        }

        public IEnumerable<Material> Search(string value)
        {
            return All().Where(c => c.Name.Contains(value)
                                 || c.PartNo.Contains(value));
        }
    }
}

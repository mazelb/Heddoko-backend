using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class MaterialTypeRepository : BaseRepository<MaterialType>, IMaterialTypeRepository
    {
        public MaterialTypeRepository(HDContext sb) : base(sb)
        {
        }

        public override IEnumerable<MaterialType> All()
        {
            return base.All().OrderBy(c => c.Identifier);
        }

        public MaterialType GetByName(string name)
        {
            return DbSet.Where(c => c.Identifier.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public IEnumerable<MaterialType> Search(string value)
        {
            return DbSet.Where(c => c.Identifier.ToLower().Contains(value.ToLower())).OrderBy(c => c.Identifier);
        }
    }
}

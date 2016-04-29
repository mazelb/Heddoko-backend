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
            return DbSet.Where(c => c.Identifier == name).FirstOrDefault();
        }

        public IEnumerable<MaterialType> Search(string value)
        {
            return DbSet.Where(c => c.Identifier.Contains(value)).OrderBy(c => c.Identifier);
        }
    }
}

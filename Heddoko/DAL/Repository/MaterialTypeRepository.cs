using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;

namespace DAL
{
    public class MaterialTypeRepository : BaseRepository<MaterialType>, IMaterialTypeRepository
    {
        public MaterialTypeRepository(HDContext sb) : base(sb)
        {
        }

        public MaterialType GetByName(string name)
        {
            return DbSet.Where(c => c.Identifier == name).FirstOrDefault();
        }
    }
}

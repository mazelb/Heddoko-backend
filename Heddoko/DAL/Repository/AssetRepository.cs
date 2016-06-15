using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;

namespace DAL
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(HDContext sb) : base(sb)
        {
        }

        public Asset GetByImage(string name)
        {
            return DbSet.Where(c => c.Image.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}

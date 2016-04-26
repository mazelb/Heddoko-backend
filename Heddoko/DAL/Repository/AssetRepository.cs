using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(HDContext sb) : base(sb)
        {
        }
    }
}

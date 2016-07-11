using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class ShirtOctopiRepository : BaseRepository<ShirtOctopi>, IShirtOctopiRepository
    {
        public ShirtOctopiRepository(HDContext sb) : base(sb)
        {
        }
    }
}

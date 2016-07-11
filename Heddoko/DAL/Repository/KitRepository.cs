using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class KitRepository : BaseRepository<Kit>, IKitRepository
    {
        public KitRepository(HDContext sb) : base(sb)
        {
        }
    }
}

using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class ShirtRepository : BaseRepository<Shirt>, IShirtRepository
    {
        public ShirtRepository(HDContext sb) : base(sb)
        {
        }
    }
}

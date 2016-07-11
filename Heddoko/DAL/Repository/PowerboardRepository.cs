using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class PowerboardRepository : BaseRepository<Powerboard>, IPowerboardRepository
    {
        public PowerboardRepository(HDContext sb) : base(sb)
        {
        }
    }
}

using DAL.Models;

namespace DAL
{
    public class PowerboardRepository : BaseRepository<Powerboard>, IPowerboardRepository
    {
        public PowerboardRepository(HDContext sb)
            : base(sb)
        {
        }
    }
}
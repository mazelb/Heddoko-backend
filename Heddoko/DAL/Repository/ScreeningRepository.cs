using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class ScreeningRepository : BaseRepository<Screening>, IScreeningRepository
    {
        public ScreeningRepository(HDContext sb) : base(sb)
        {
        }
    }
}

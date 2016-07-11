using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class PantsRepository : BaseRepository<Pants>, IPantsRepository
    {
        public PantsRepository(HDContext sb) : base(sb)
        {
        }
    }
}

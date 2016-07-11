using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class PantsOctopiRepository : BaseRepository<PantsOctopi>, IPantsOctopiRepository
    {
        public PantsOctopiRepository(HDContext sb) : base(sb)
        {
        }
    }
}

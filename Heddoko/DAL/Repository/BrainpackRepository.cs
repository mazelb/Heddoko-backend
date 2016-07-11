using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class BrainpackRepository : BaseRepository<Brainpack>, IBrainpackRepository
    {
        public BrainpackRepository(HDContext sb) : base(sb)
        {
        }
    }
}

using DAL.Models;

namespace DAL
{
    public class BrainpackRepository : BaseRepository<Brainpack>, IBrainpackRepository
    {
        public BrainpackRepository(HDContext sb)
            : base(sb)
        {
        }
    }
}
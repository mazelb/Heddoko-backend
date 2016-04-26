using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class MovementFrameRepository : BaseRepository<MovementFrame>, IMovementFrameRepository
    {
        public MovementFrameRepository(HDContext sb) : base(sb)
        {
        }
    }
}

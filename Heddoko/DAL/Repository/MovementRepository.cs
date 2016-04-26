using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class MovementRepository : BaseRepository<Movement>, IMovementRepository
    {
        public MovementRepository(HDContext sb) : base(sb)
        {
        }
    }
}

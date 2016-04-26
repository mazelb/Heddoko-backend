using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class MovementMarkerRepository : BaseRepository<MovementMarker>, IMovementMarkerRepository
    {
        public MovementMarkerRepository(HDContext sb) : base(sb)
        {
        }
    }
}

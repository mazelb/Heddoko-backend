using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class MovementEventRepository : BaseRepository<MovementEvent>, IMovementEventRepository
    {
        public MovementEventRepository(HDContext sb) : base(sb)
        {
        }
    }
}

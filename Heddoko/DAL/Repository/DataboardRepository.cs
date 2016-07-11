using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class DataboardRepository : BaseRepository<Databoard>, IDataboardRepository
    {
        public DataboardRepository(HDContext sb) : base(sb)
        {
        }
    }
}

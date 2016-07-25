using DAL.Models;

namespace DAL
{
    public class DataboardRepository : BaseRepository<Databoard>, IDataboardRepository
    {
        public DataboardRepository(HDContext sb)
            : base(sb)
        {
        }
    }
}
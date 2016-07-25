using DAL.Models;

namespace DAL
{
    public class SensorSetRepository : BaseRepository<SensorSet>, ISensorSetRepository
    {
        public SensorSetRepository(HDContext sb)
            : base(sb)
        {
        }
    }
}
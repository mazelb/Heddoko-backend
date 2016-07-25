using DAL.Models;

namespace DAL
{
    public class SensorRepository : BaseRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(HDContext sb)
            : base(sb)
        {
        }
    }
}
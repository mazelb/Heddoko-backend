using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class SensorRepository : BaseRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(HDContext sb) : base(sb)
        {
        }
    }
}

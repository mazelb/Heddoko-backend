using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class SensorSetRepository : BaseRepository<SensorSet>, ISensorSetRepository
    {
        public SensorSetRepository(HDContext sb) : base(sb)
        {
        }
    }
}

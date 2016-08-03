using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface ISensorRepository : IBaseRepository<Sensor>
    {
        IEnumerable<Sensor> All(bool isDeleted);
        IEnumerable<Sensor> Search(string value, bool isDeleted = false);
        IEnumerable<Sensor> GetAvailable(int? id = null);
        IEnumerable<Sensor> GetBySensorSet(int sensorSetID);
    }
}
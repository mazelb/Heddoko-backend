using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface ISensorRepository : IBaseRepository<Sensor>, IHistoryRepository<Sensor>
    {
        Sensor Get(string label);
        IEnumerable<Sensor> All(bool isDeleted);
        IEnumerable<Sensor> Search(string value, int? statusFilter = null, bool isDeleted = false);
        IEnumerable<Sensor> GetAvailable(int? id = null);
        IEnumerable<Sensor> GetBySensorSet(int sensorSetID);
        void RemoveSensorSet(int sensorSetID);
        IEnumerable<Sensor> SearchAvailable(string value);
        int GetNumReady();
    }
}
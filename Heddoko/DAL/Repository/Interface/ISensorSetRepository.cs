using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface ISensorSetRepository : IBaseRepository<SensorSet>
    {
        IEnumerable<SensorSet> All(bool isDeleted);
        IEnumerable<SensorSet> Search(string value, bool isDeleted);
        IEnumerable<SensorSet> GetAvailable(int? id = null);
    }
}
﻿using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface ISensorSetRepository : IBaseRepository<SensorSet>, IHistoryRepository<SensorSet>
    {
        IEnumerable<SensorSet> All(bool isDeleted);
        IEnumerable<SensorSet> Search(string value, int? statusFilter = null, bool isDeleted = false);
        IEnumerable<SensorSet> GetAvailable(int? id = null);
    }
}
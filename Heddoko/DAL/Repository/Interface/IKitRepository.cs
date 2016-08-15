﻿using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IKitRepository : IBaseRepository<Kit>
    {
        IEnumerable<Kit> Search(string value, int? statusFilter = null, bool isDeleted = false, int? organizationID = null);
        IEnumerable<Kit> All(bool isDeleted, int? organizationID = null);
        IEnumerable<Kit> GetAvailable(int? usedID, int? organizationID = null);
        void RemoveUser(int userID);
        void RemoveShirt(int shirtID);
        void RemoveBrainpack(int id);
        void RemovePants(int pantsID);
        void RemoveSensorSet(int sensorSetID);
        int GetNumReady();
    }
}
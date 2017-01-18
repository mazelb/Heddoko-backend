/**
 * @file IKitRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;
using Z.EntityFramework.Plus;

namespace DAL
{
    public interface IKitRepository : IBaseRepository<Kit>, IHistoryRepository<Kit>
    {
        Models.Kit Get(string label);
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
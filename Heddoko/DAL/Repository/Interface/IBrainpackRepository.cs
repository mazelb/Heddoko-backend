/**
 * @file IBrainpackRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IBrainpackRepository : IBaseRepository<Brainpack>, IHistoryRepository<Brainpack>
    {
        Brainpack Get(string label);
        void RemoveDataboard(int databoardID);
        void RemovePowerboard(int id);
        IEnumerable<Brainpack> GetAvailable(int? usedID);
        IEnumerable<Brainpack> Search(string value, int? statusFilter = null, bool isDeleted = false);
        IEnumerable<Brainpack> All(bool isDeleted);
        int GetNumReady();
    }
}
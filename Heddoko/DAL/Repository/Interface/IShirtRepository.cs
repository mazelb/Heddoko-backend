/**
 * @file IShirtRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IShirtRepository : IBaseRepository<Shirt>, IHistoryRepository<Shirt>
    {
        IEnumerable<Shirt> Search(string value, int? statusFilter = null, bool isDeleted = false);
        IEnumerable<Shirt> All(bool isDeleted);
        void RemoveShirtOctopi(int shirtOctopiID);
        IEnumerable<Shirt> GetAvailable(int? usedID);
        int GetNumReady();
    }
}
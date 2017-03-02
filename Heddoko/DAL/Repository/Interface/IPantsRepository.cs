/**
 * @file IPantsRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IPantsRepository : IBaseRepository<Pants>, IHistoryRepository<Pants>
    {
        IEnumerable<Pants> Search(string value, int? statusFilter = null, bool isDeleted = false);

        IEnumerable<Pants> All(bool isDeleted);

        void RemovePantsOctopi(int pantsOctopiID);

        IEnumerable<Pants> GetAvailable(int? usedID);
        int GetNumReady();
    }
}
/**
 * @file IShirtOctopiRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IShirtOctopiRepository : IBaseRepository<ShirtOctopi>, IHistoryRepository<ShirtOctopi>
    {
        IEnumerable<ShirtOctopi> All(bool isDeleted);
        IEnumerable<ShirtOctopi> Search(string value, bool isDeleted);
        IEnumerable<ShirtOctopi> GetAvailable(int? id = null);
        int GetNumReady();
    }
}
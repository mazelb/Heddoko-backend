/**
 * @file IPowerboardRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IPowerboardRepository : IBaseRepository<Powerboard>, IHistoryRepository<Powerboard>
    {
        Powerboard Get(string label);
        IEnumerable<Powerboard> Search(string value, bool isDeleted);
        IEnumerable<Powerboard> GetAvailable(int? usedID);
        IEnumerable<Powerboard> All(bool isDeleted);
        int GetNumReady();
    }
}
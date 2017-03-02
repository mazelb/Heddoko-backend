/**
 * @file IPantsOctopiRepository.cs
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
    public interface IPantsOctopiRepository : IBaseRepository<PantsOctopi>, IHistoryRepository<PantsOctopi>
    {
        IEnumerable<PantsOctopi> All(bool isDeleted);
        IEnumerable<PantsOctopi> Search(string value, bool isDeleted);
        IEnumerable<PantsOctopi> GetAvailable(int? id = null);
        int GetNumReady();
    }
}
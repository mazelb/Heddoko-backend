/**
 * @file IHistoryRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Models;
using Z.EntityFramework.Plus;

namespace DAL
{
    public interface IHistoryRepository<T> : IDisposable where T : Models.BaseModel
    {

        #region History
        IEnumerable<AuditEntry> History(int id);

        List<HistoryNotes> HistoryNotes(int id);
        #endregion
    }
}

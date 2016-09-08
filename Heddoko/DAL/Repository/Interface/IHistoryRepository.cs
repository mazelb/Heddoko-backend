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

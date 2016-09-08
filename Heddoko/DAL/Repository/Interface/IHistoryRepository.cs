using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace DAL
{
    public interface IHistoryRepository<T> : IDisposable where T : Models.BaseModel
    {

        #region History
        IEnumerable<AuditEntry> History(int id);

        List<string> HistoryNotes(int id);
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IBaseRepository<T> : IDisposable where T : DAL.Models.BaseModel
    {
        #region Cache
        string GetCacheKey(string id);

        T GetCached(string id);

        void SetCache(string id, T item);

        T GetIDCached(int id);
        #endregion

        #region Select
        T Get(int id);

        IEnumerable<T> All();

        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        bool Any(Expression<Func<T, bool>> predicate);

        T First(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        void RemoveRange(List<T> items);

        void Reload(T entity);
        #endregion

        #region Save
        void Attach(T item);

        bool Exists(T entity);

        void AttachModified(T item);

        void Add(T item);

        void Remove(T entity);

        void Save();

        void Update();

        void AttachAndUpdate(T item);

        void Create(T item);

        void Delete(T item);
        #endregion
    }
}

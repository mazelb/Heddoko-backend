using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class BaseRepository<T> : ICacheRepository<T>, IBaseRepository<T> where T : class, IBaseModel
    {
        protected readonly IDbSet<T> DbSet;

        protected BaseRepository(HDContext context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }

        private HDContext Db { get; }
        protected string Key { private get; set; }

        #region Cache

        public string GetCacheKey(string id)
        {
            return $"{Constants.Cache.Prefix}:{Config.Environment}:{Key}:{id}";
        }

        public T GetCached(string id)
        {
            T item = RedisManager.Get<T>(GetCacheKey(id?.ToLower()));
            if (item != null
                &&
                item.ID > 0)
            {
                Attach(item);
            }

            return item;
        }

        public virtual void SetCache(string id, T item, int? hours = null)
        {
            RedisManager.Set(GetCacheKey(id?.ToLower()), item, hours);
        }

        public virtual void ClearCache(T item)
        {
            ClearCache(item.ID.ToString());
        }

        public virtual void ClearCache(string id)
        {
            RedisManager.Clear(GetCacheKey(id?.ToLower()));
        }

        public virtual T GetIDCached(int id)
        {
            T item = GetCached(id.ToString());
            if (item == null)
            {
                item = Get(id);
                if (item != null)
                {
                    SetCache(id.ToString(), item);
                }
            }

            return item;
        }

        #endregion

        #region Select

        public virtual T Get(int id)
        {
            return DbSet.FirstOrDefault(c => c.ID == id);
        }

        public virtual T GetFull(int id)
        {
            return Get(id);
        }

        public virtual IEnumerable<T> All()
        {
            return DbSet;
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return DbSet.First(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public void RemoveRange(List<T> items)
        {
            foreach (T item in items)
            {
                DbSet.Remove(item);
            }
        }

        public void Reload(T entity)
        {
            Db.Entry(entity).Reload();
        }

        #endregion

        #region Save

        public void Attach(T item)
        {
            if (Db.Entry(item).State != EntityState.Detached)
            {
                return;
            }

            if (!Exists(item))
            {
                DbSet.Attach(item);
            }
        }

        public bool Exists(T entity)
        {
            return DbSet.Local.Any(e => e.ID == entity.ID);
        }


        public void AttachModified(T item)
        {
            if (Db.Entry(item).State == EntityState.Detached)
            {
                if (!Exists(item))
                {
                    DbSet.Attach(item);
                }
            }
            Db.Entry(item).State = EntityState.Modified;
        }

        public void Add(T item)
        {
            DbSet.Add(item);
        }

        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        private void Save()
        {
            Db.SaveChanges();
        }

        private void Update()
        {
            Save();
        }

        public void AttachAndUpdate(T item)
        {
            AttachModified(item);
            Update();
        }

        public void Create(T item)
        {
            Add(item);
            Save();
        }

        public void Delete(T item)
        {
            Remove(item);
            Save();
        }

        #endregion

        #region IDisposable

        private bool _disposed;

        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Db.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
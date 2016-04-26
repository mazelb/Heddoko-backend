using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BaseRepository<T> : CacheRepository<T>, IBaseRepository<T> where T : BaseModel
    {
        protected HDContext db { get; private set; }
        protected IDbSet<T> DbSet;
        protected string Key { get; set; }

        public BaseRepository(HDContext context)
        {
            db = context;
            DbSet = db.Set<T>();
        }

        #region Cache

        public string GetCacheKey(string id)
        {
            return $"{Constants.Cache.Prefix}:{Config.Environment}:{Key}:{id}";
        }

        public T GetCached(string id)
        {
            T item = RedisManager.Get<T>(GetCacheKey(id));
            if (item != null
             && item.ID > 0)
            {
                Attach(item);
            }

            return item;
        }

        public virtual void SetCache(string id, T item)
        {
            RedisManager.Set(GetCacheKey(id), item);
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

        public virtual IEnumerable<T> All()
        {
            return this.DbSet;
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Any(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.First(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.FirstOrDefault(predicate);
        }

        public void RemoveRange(List<T> items)
        {
            this.RemoveRange(items);
        }

        public void Reload(T entity)
        {
            this.db.Entry<T>(entity).Reload();
        }
        #endregion

        #region Save
        public void Attach(T item)
        {
            if (db.Entry(item).State == EntityState.Detached)
            {
                if (!Exists(item))
                {
                    DbSet.Attach(item);
                }
            }
        }
        public bool Exists(T entity)
        {
            return DbSet.Local.Any(e => e.ID == entity.ID);
        }


        public void AttachModified(T item)
        {
            if (db.Entry(item).State == EntityState.Detached)
            {
                if (!Exists(item))
                {
                    DbSet.Attach(item);
                }
            }
            db.Entry(item).State = EntityState.Modified;
        }

        public void Add(T item)
        {
            DbSet.Add(item);
        }

        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Save()
        {
            db.SaveChanges();
        }

        public void Update()
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

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

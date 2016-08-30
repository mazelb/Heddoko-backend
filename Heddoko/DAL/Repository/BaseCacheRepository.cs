using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Models;

namespace DAL
{
    public class BaseCacheRepository<T> : ICacheRepository<T>
    {
        protected string Key { private get; set; }

        #region Cache

        public string GetCacheKey(string id)
        {
            return $"{Constants.Cache.Prefix}:{Config.Environment}:{Key}:{id}";
        }

        public T GetCached(string id)
        {
            return RedisManager.Get<T>(GetCacheKey(id?.ToLower()));
        }

        public virtual void SetCache(string id, T item, int? hours = null)
        {
            RedisManager.Set(GetCacheKey(id?.ToLower()), item, hours);
        }

        public virtual void ClearCache(string id)
        {
            RedisManager.Clear(GetCacheKey(id?.ToLower()));
        }
        #endregion
    }
}
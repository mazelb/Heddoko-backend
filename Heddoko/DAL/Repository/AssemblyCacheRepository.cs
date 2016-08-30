using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class AssemblyCacheRepository : BaseCacheRepository<List<Assembly>>, IAssemblyCacheRepository
    {
        public AssemblyCacheRepository()
        {
            Key = Constants.Cache.Assembly;
        }

        public List<Assembly> GetCached()
        {
            return GetCached(Constants.Cache.KeyAll);
        }

        public void ClearCache()
        {
            ClearCache(Constants.Cache.KeyAll);
        }

        public void SetCache(List<Assembly> item)
        {
            SetCache(Constants.Cache.KeyAll, item, Constants.CacheExpiration.Assembly);
        }
    }
}
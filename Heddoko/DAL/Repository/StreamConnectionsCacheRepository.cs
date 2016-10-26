using System.Collections.Generic;
using DAL.Models;
using DAL.Repository.Interface;

namespace DAL.Repository
{
    public class StreamConnectionsCacheRepository : BaseCacheRepository<List<Channel>>, IStreamConnectionsCacheRepository
    {
        private readonly string _cacheKey;

        public StreamConnectionsCacheRepository()
        {
            Key = Constants.Cache.StreamConnections;
            _cacheKey = GetCacheKey(string.Empty);
        }

        public List<Channel> GetCached()
        {
            return GetCached(_cacheKey);
        }

        public void ClearCache()
        {
            ClearCache(_cacheKey);
        }

        public void SetCache(List<Channel> connections)
        {
            SetCache(_cacheKey, connections);
        }
    }
}

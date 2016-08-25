using System.Collections.Generic;

namespace DAL
{
    public interface ICacheRepository<T>
    {
        string GetCacheKey(string id);

        T GetCached(string id);

        void SetCache(string id, T item, int? hours = null);

        void ClearCache(string id);
    }
}

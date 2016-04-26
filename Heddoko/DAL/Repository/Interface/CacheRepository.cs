using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface CacheRepository<T> where T : DAL.Models.BaseModel
    {
        string GetCacheKey(string id);

        T GetCached(string id);

        void SetCache(string id, T item);
    }
}

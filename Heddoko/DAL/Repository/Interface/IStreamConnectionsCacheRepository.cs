using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repository.Interface
{
    public interface IStreamConnectionsCacheRepository : ICacheRepository<List<Channel>>
    {
        List<Channel> GetCached();

        void ClearCache();

        void SetCache(List<Channel> connections);
    }
}

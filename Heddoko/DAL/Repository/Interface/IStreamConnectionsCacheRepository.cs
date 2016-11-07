using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repository.Interface
{
    public interface IStreamConnectionsCacheRepository : ICacheRepository<List<Channel>>
    {
        List<Channel> GetCached(int teamId);

        void SetCache(int teamId, List<Channel> connections);
    }
}

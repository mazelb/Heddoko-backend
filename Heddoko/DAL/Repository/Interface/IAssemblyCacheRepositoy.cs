using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Models;

namespace DAL
{
    public interface IAssemblyCacheRepository : ICacheRepository<List<Assembly>>
    {
        List<Assembly> GetCached();
        void ClearCache();

        void SetCache(List<Assembly> item);
    }
}

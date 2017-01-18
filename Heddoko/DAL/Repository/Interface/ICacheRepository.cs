/**
 * @file ICacheRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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

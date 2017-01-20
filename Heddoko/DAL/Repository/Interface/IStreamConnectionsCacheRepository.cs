/**
 * @file IStreamConnectionsCacheRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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

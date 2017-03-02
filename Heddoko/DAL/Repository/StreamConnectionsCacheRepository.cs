/**
 * @file StreamConnectionsCacheRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository.Interface;

namespace DAL.Repository
{
    public class StreamConnectionsCacheRepository : BaseCacheRepository<List<Channel>>, IStreamConnectionsCacheRepository
    {
        public StreamConnectionsCacheRepository()
        {
            Key = Constants.Cache.StreamConnections;
        }

        public List<Channel> GetCached(int teamId)
        {
            return GetCached(teamId.ToString()) ?? new List<Channel>();
        }

        public void SetCache(int teamId, List<Channel> connections)
        {
            SetCache(teamId.ToString(), connections);
        }
    }
}

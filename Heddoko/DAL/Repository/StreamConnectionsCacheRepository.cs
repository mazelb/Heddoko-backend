using System.Collections.Generic;
using System.Linq;
using DAL.Helpers;
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

        public Channel CreateChannel(string channelName, User user)
        {
            Channel channel = null;
            if (user.TeamID != null)
            {
                List<Channel> connections = GetCached(user.TeamID.Value);

                channel = connections.FirstOrDefault(c => c.User.Id == user.Id);

                if (channel == null)
                {
                    channel = new Channel { Name = channelName, User = user };
                    connections.Add(channel);

                    SetCache(user.TeamID.Value, connections);
                }
            }

            return channel;
        }
    }
}

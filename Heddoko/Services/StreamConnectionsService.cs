using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Models;
using DAL.Models.Enum;
using Hangfire;

namespace Services
{
    public class StreamConnectionsService
    {
        private static readonly object LockObj = new object();
        private readonly UnitOfWork _unitOfWork;
        
        public StreamConnectionsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Channel CreateChannel(string channelName, User user)
        {
            Channel channel = null;
            if (user.TeamID != null)
            {
                lock (LockObj)
                {
                    List<Channel> connections = _unitOfWork.StreamConnectionsCacheRepository.GetCached(user.TeamID.Value);

                    channel = connections.FirstOrDefault(c => c.User.Id == user.Id);

                    if (channel == null)
                    {
                        channel = new Channel { Name = channelName, User = user };
                        connections.Add(channel);

                        _unitOfWork.StreamConnectionsCacheRepository.SetCache(user.TeamID.Value, connections);

                        int teamId = user.TeamID.Value;
                        BackgroundJob.Enqueue(() => ActivityService.SendForTeam(teamId, UserEventType.StreamChannelOpened));
                    }
                }
            }

            return channel;
        }

        public void RemoveChannel(User user)
        {
            if (user.TeamID != null)
            {
                lock (LockObj)
                {
                    List<Channel> connections = _unitOfWork.StreamConnectionsCacheRepository.GetCached(user.TeamID.Value);

                    if (connections.RemoveAll(c => c.User.Id == user.Id) > 0)
                    {
                        _unitOfWork.StreamConnectionsCacheRepository.SetCache(user.TeamID.Value, connections);

                        int teamId = user.TeamID.Value;
                        BackgroundJob.Enqueue(() => ActivityService.SendForTeam(teamId, UserEventType.StreamChannelClosed));
                    }
                }
            }
        }
    }
}

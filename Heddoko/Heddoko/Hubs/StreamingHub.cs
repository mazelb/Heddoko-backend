using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using i18n;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace Heddoko.Hubs
{
    [Authorize(Roles = DAL.Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
    public class StreamingHub : Hub
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public void Send(byte[] stream)
        {
            var currentUser = _unitOfWork.UserRepository.GetFull(Context.User.Identity.GetUserId<int>());

            if (!currentUser.TeamID.HasValue)
            {
                throw new HubException(Resources.UserIsNotInTeam, new { user = currentUser.UserName });
            }

            if (currentUser.Kit == null)
            {
                throw new HubException(Resources.UserDoesntHaveKit, new { user = currentUser.UserName });
            }

            List<Channel> connections = _unitOfWork.StreamConnectionsCacheRepository.GetCached() ??
                                            new List<Channel>();

            Channel channel = connections.FirstOrDefault(c => c.User.Id == currentUser.Id);

            if (channel == null)
            {
                channel = new Channel { Name = GetGroupNameForTeam(currentUser), User = currentUser };
                connections.Add(channel);

                _unitOfWork.StreamConnectionsCacheRepository.SetCache(connections);
            }

            Clients.Group(channel.Name).GetData(stream);
        }

        public async Task JoinGroupByTeam(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
        }

        private string GetGroupNameForTeam(User user)
        {
            return $"team-{user.TeamID}-{user.Kit?.Id}";
        }
    }
}
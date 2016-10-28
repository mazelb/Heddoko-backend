﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DAL;
using DAL.Helpers;
using DAL.Models;
using Heddoko.Models.Streaming;
using i18n;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;

namespace Heddoko.Hubs
{
    [Authorize(Roles = DAL.Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
    public class StreamingHub : Hub
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public void Send(StreamMessage message)
        {
            User currentUser = _unitOfWork.UserRepository.GetIDCached(Context.User.Identity.GetUserId<int>());

            CheckUserLicense(currentUser);

            if (!currentUser.TeamID.HasValue)
            {
                throw new HubException(Resources.UserIsNotInTeam, new { user = currentUser.UserName });
            }

            if (currentUser.Kit == null)
            {
                throw new HubException(Resources.UserDoesntHaveKit, new { user = currentUser.UserName });
            }

            Channel channel = _unitOfWork.StreamConnectionsCacheRepository.CreateChannel(ChanelHelper.GetChannelName(currentUser), currentUser);

            Clients.Group(channel.Name).GetData(message);
        }

        public async Task JoinGroupByTeam(string groupName)
        {
            User currentUser = _unitOfWork.UserRepository.GetIDCached(Context.User.Identity.GetUserId<int>());

            CheckUserLicense(currentUser);

            if (!currentUser.TeamID.HasValue)
            {
                throw new HubException(Resources.UserIsNotInTeam, new { user = currentUser.UserName });
            }

            List<Channel> connections = _unitOfWork.StreamConnectionsCacheRepository.GetCached(currentUser.TeamID.Value);

            Channel channel = connections.FirstOrDefault(c => c.Name == groupName);
            if (channel == null || channel.User.TeamID != currentUser.TeamID)
            {
                throw new HubException(Resources.WrongTeam);
            }

            await Groups.Add(Context.ConnectionId, groupName);
        }

        private void CheckUserLicense(User user)
        {
            ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            try
            {
                manager.CheckUserLicense(user);
            }
            catch (Exception e)
            {
                throw new HubException(e.Message, new { user = user.UserName });
            }
        }
    }
}
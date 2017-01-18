/**
 * @file NotificationsHub.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Threading.Tasks;
using DAL.Models.Enum;
using Heddoko.Models.Notifications;
using Microsoft.AspNet.SignalR;

namespace Heddoko.Hubs
{
    public class NotificationsHub : Hub
    {
        [Authorize(Roles = DAL.Constants.Roles.ServiceAdmin)]
        public void Send(string message, string token, UserEventType type)
        {
            Clients.Group(token).ShowNotification(new NotificationMessage { Text = message, Type = type });
        }

        [Authorize(Roles = DAL.Constants.Roles.All)]
        public async Task Subscribe(string token)
        {
            await Groups.Add(Context.ConnectionId, token);
        }
    }
}
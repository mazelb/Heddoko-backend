/**
 * @file StreamingAPIController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Web.Http;
using DAL;
using DAL.Helpers;
using DAL.Models;
using Heddoko.Helpers.DomainRouting.Http;
using i18n;
using Services;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/stream")]
    [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
    public class StreamingAPIController : BaseAPIController
    {
        private readonly StreamConnectionsService _streamConnectionsService;

        public StreamingAPIController()
        {
            //TODO: get rid of this after adding IoC container
            _streamConnectionsService = new StreamConnectionsService(UoW);
        }

        public StreamingAPIController(ApplicationUserManager userManager, UnitOfWork uow, StreamConnectionsService streamConnectionsService)
            : base(userManager, uow)
        {
            _streamConnectionsService = streamConnectionsService;
        }

        [DomainRoute("connections", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public List<Channel> Connections()
        {
            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.UserIsNotInTeam, Resources.UserIsNotInTeam);
            }

            return UoW.StreamConnectionsCacheRepository.GetCached(CurrentUser.TeamID.Value);
        }

        [DomainRoute("connections/add", Constants.ConfigKeyName.DashboardSite)]
        [HttpPost]
        public bool AddConnection()
        {
            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.UserIsNotInTeam, Resources.UserIsNotInTeam);
            }

            _streamConnectionsService.CreateChannel(ChanelHelper.GetChannelName(CurrentUser), CurrentUser);

            return true;
        }

        [DomainRoute("connections/delete", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public bool RemoveConnection()
        {
            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.UserIsNotInTeam, Resources.UserIsNotInTeam);
            }

            _streamConnectionsService.RemoveChannel(CurrentUser);

            return true;
        }
    }
}
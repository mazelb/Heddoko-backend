using System.Collections.Generic;
using System.Web.Http;
using DAL;
using DAL.Helpers;
using DAL.Models;
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

        [Route("connections")]
        [HttpGet]
        public List<Channel> Connections()
        {
            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.UserIsNotInTeam, Resources.UserIsNotInTeam);
            }

            return UoW.StreamConnectionsCacheRepository.GetCached(CurrentUser.TeamID.Value);
        }

        [Route("connections/add")]
        [HttpPost]
        public bool AddConnection()
        {
            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.UserIsNotInTeam, Resources.UserIsNotInTeam);
            }

            if (CurrentUser.Kit == null)
            {
                throw new APIException(ErrorAPIType.KitID, Resources.UserDoesntHaveKit);
            }

            _streamConnectionsService.CreateChannel(ChanelHelper.GetChannelName(CurrentUser), CurrentUser);

            return true;
        }

        [Route("connections/delete")]
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
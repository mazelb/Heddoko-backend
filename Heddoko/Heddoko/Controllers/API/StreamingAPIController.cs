using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DAL;
using DAL.Helpers;
using DAL.Models;
using i18n;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/stream")]
    public class StreamingAPIController : BaseAPIController
    {
        [Route("connections")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
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
        [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
        public bool AddConnection()
        {
            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.UserIsNotInTeam, Resources.UserIsNotInTeam);
            }

            UoW.UserRepository.ClearCache(CurrentUser);

            if (CurrentUser.Kit == null)
            {
                throw new APIException(ErrorAPIType.KitID, Resources.UserDoesntHaveKit);
            }

            UoW.StreamConnectionsCacheRepository.CreateChannel(ChanelHelper.GetChannelName(CurrentUser), CurrentUser);

            return true;
        }

        [Route("connections/delete")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
        public bool RemoveConnection()
        {
            if (!CurrentUser.TeamID.HasValue)
            {
                throw new APIException(ErrorAPIType.UserIsNotInTeam, Resources.UserIsNotInTeam);
            }

            List<Channel> connections = UoW.StreamConnectionsCacheRepository.GetCached(CurrentUser.TeamID.Value);

            if (CurrentUser != null &&
                connections.RemoveAll(c => c.User.Id == CurrentUser.Id) > 0)
            {
                UoW.StreamConnectionsCacheRepository.SetCache(CurrentUser.TeamID.Value, connections);
            }

            return true;
        }
    }
}
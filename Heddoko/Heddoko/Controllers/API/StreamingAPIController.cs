using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DAL;
using DAL.Helpers;
using DAL.Models;
using Heddoko.Helpers.DomainRouting.Http;
using i18n;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/stream")]
    [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
    public class StreamingAPIController : BaseAPIController
    {
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

            if (CurrentUser.Kit == null)
            {
                throw new APIException(ErrorAPIType.KitID, Resources.UserDoesntHaveKit);
            }

            UoW.StreamConnectionsCacheRepository.CreateChannel(ChanelHelper.GetChannelName(CurrentUser), CurrentUser);

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

            List<Channel> connections = UoW.StreamConnectionsCacheRepository.GetCached(CurrentUser.TeamID.Value);

            if (connections.RemoveAll(c => c.User.Id == CurrentUser.Id) > 0)
            {
                UoW.StreamConnectionsCacheRepository.SetCache(CurrentUser.TeamID.Value, connections);
            }

            return true;
        }
    }
}
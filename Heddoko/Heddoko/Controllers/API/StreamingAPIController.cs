using System.Collections.Generic;
using System.Web.Http;
using DAL;
using DAL.Models;

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
            return UoW.StreamConnectionsCacheRepository.GetCached() ?? new List<Channel>();
        }

        [Route("connections/delete")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseAdminAndWorkerAndAnalyst)]
        public bool RemoveConnection()
        {
            List<Channel> connections = UoW.StreamConnectionsCacheRepository.GetCached();

            if (connections != null &&
                CurrentUser != null &&
                connections.RemoveAll(c => c.User.Id == CurrentUser.Id) > 0)
            {
                UoW.StreamConnectionsCacheRepository.ClearCache();
                UoW.StreamConnectionsCacheRepository.SetCache(connections);
            }

            return true;
        }
    }
}
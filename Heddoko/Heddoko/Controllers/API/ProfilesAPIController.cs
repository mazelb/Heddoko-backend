using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Heddoko.Controllers.API
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("api/v1/profiles")]
    public class ProfilesAPIController : BaseAPIController
    {
        [Route("{embed?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public IEnumerable<Profile> Get(string embed = null)
        {
            return UoW.ProfileRepository.GetByUser(CurrentUser.ID, embed?.Split(',').Select(c => c.Trim()).ToList());
        }
    }
}

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
    [RoutePrefix("api/v1/groups")]
    public class GroupsAPIController : BaseAPIController
    {
        [Route("{embed?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public IEnumerable<Group> Get(string embed = null)
        {
            return UoW.GroupRepository.GetByUser(CurrentUser.ID, embed?.Split(',').Select(c => c.Trim()).ToList());
        }
    }
}

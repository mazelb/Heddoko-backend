using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/users")]
    public class UsersAPIController : BaseAPIController
    {
        [Route("{id:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public User Get(int? id = null)
        {
            if (id.HasValue)
            {
                User user = UoW.UserRepository.GetIDCached(id.Value);
                if (user != null)
                {
                    if (CurrentUser.ID == id.Value)
                    {
                    }
                    return user;
                }
                else
                {
                    throw new APIException(ErrorAPIType.UserNotFound, ErrorAPIType.UserNotFound.GetDisplayName());
                }
            }
            return CurrentUser;
        }
    }
}

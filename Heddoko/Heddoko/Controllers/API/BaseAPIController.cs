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
    public class BaseAPIController : ApiController
    {
        protected UnitOfWork UoW { get; set; }

        public BaseAPIController()
        {
            UoW = new UnitOfWork();
        }

        public bool IsAuth
        {
            get
            {
                return CurrentUser != null;
            }
        }

        private User currentUser { get; set; }

        public User CurrentUser
        {
            get
            {
                if (ControllerContext.RequestContext.Principal.Identity.IsAuthenticated)
                {
                    if (currentUser == null)
                    {
                        currentUser = UoW.UserRepository.GetIDCached(int.Parse(ControllerContext.RequestContext.Principal.Identity.Name));
                    }

                    return currentUser;
                }
                return null;
            }
        }
    }
}

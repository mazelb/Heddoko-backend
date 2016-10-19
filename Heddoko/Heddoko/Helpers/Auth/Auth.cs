using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using DAL;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using DAL.Models;

namespace Heddoko
{
    public static class Auth
    {
        public static User CurrentUser
        {
            get
            {
                ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return manager.FindByIdCached(HttpContext.Current.User.Identity.GetUserId<int>());
            }
        }

        public static IList<string> CurrentUserRoles
        {
            get
            {
                ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                int currentUserId = HttpContext.Current.User.Identity.GetUserId<int>();

                return manager.GetRoles(currentUserId);
            }
        }
    }
}
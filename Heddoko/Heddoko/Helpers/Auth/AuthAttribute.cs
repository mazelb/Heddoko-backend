using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Heddoko
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (skipAuthorization)
            {
                return;
            }

            if (filterContext.HttpContext.User == null)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            if (!string.IsNullOrEmpty(Roles))
            {
                string role = Roles.Split(',').FirstOrDefault(c => filterContext.HttpContext.User.IsInRole(c));
                if (string.IsNullOrEmpty(role))
                {
                    HandleUnauthorizedRequest(filterContext);
                    return;
                }
            }

            base.OnAuthorization(filterContext);
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User == null
            || filterContext.HttpContext.User.Identity == null
            || !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string returnUrl = string.Empty;
                string requestUrl = filterContext.HttpContext.Request.RawUrl;
                if (!requestUrl.Equals("/"))
                {
                    returnUrl = string.Format("?returnUrl={0}", HttpUtility.UrlEncode(requestUrl));
                }
                filterContext.Result = new RedirectResult(string.Format("~/login{0}", returnUrl));
            }
            else
            {
                throw new HttpException(403, i18n.Resources.YouAreNotAuthorized);
            }
        }
    }
}
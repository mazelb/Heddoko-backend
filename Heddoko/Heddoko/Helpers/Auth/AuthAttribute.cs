/**
 * @file AuthAttribute.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using i18n;
using ClaimsPrincipal = System.Security.Claims.ClaimsPrincipal;
using static DAL.Constants;

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

            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            if (user.HasClaim(OpenAPIClaims.ClaimType, OpenAPIClaims.ClaimValue))
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
                ||
                filterContext.HttpContext.User.Identity == null
                ||
                !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string returnUrl = string.Empty;
                string requestUrl = filterContext.HttpContext.Request.RawUrl;
                if (!requestUrl.Equals("/"))
                {
                    returnUrl = $"?returnUrl={HttpUtility.UrlEncode(requestUrl)}";
                }
                filterContext.Result = new RedirectResult($"~/login{returnUrl}");
            }
            else
            {
                throw new HttpException(403, Resources.YouAreNotAuthorized);
            }
        }
    }
}
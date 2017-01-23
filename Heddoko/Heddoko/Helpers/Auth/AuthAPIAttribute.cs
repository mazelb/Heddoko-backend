/**
 * @file AuthAPIAttribute.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using i18n;
using System.Security.Claims;
using static DAL.Constants;

namespace Heddoko
{
    public class AuthAPIAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext context)
        {
            if (context == null)
            {
                throw new Exception("actionContext");
            }
            
            if (SkipAuthorization(context))
            {
                return;
            }

            if (context.RequestContext.Principal?.Identity == null || !context.RequestContext.Principal.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(context);
                return;
            }

            var user = context.RequestContext.Principal as ClaimsPrincipal;
            if (user.HasClaim(OpenAPIClaims.ClaimType, OpenAPIClaims.ClaimValue))
            {
                HandleUnauthorizedRequest(context);
                return;
            }

            if (!string.IsNullOrEmpty(Roles))
            {
                string role = Roles.Split(',').FirstOrDefault(c => context.RequestContext.Principal.IsInRole(c));
                if (string.IsNullOrEmpty(role))
                {
                    HandleUnauthorizedRequest(context);
                    return;
                }
            }

            base.OnAuthorization(context);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext context)
        {
            if (context.RequestContext.Principal?.Identity == null || !context.RequestContext.Principal.Identity.IsAuthenticated)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(Resources.YouAreNotAuthorized)
                };
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(Resources.YouAreNotAllowed)
                };
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthroized = base.IsAuthorized(actionContext);

            return isAuthroized;
        }
        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            if (!actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                return actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            return true;
        }
    }
}
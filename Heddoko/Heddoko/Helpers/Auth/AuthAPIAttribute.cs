using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

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

            if (context.RequestContext.Principal == null
               || context.RequestContext.Principal.Identity == null
               || !context.RequestContext.Principal.Identity.IsAuthenticated)
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
            if (context.RequestContext.Principal == null
               || context.RequestContext.Principal.Identity == null
               || !context.RequestContext.Principal.Identity.IsAuthenticated)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(i18n.Resources.YouAreNotAuthorized),
                };
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(i18n.Resources.YouAreNotAllowed),
                };
            }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthroized = base.IsAuthorized(actionContext);

            return isAuthroized;
        }
    }
}
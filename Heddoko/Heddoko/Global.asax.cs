/**
 * @file Global.asax.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Newtonsoft.Json;

namespace Heddoko
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_BeginRequest(object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            Config.Initialise(context);
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string token = Context.Request.Headers[Constants.HeaderToken];

            if (!string.IsNullOrEmpty(token))
            {
                HDContext db = HDContext.Create();
                UnitOfWork uow = new UnitOfWork();
                User user = uow.AccessTokenRepository.GetByToken(token)?.User;
                if (user == null)
                {
                    return;
                }

                if (user.IsActive)
                {
                    List<IdentityRole> roles = db.Roles.ToList();

                    GenericIdentity identity = new GenericIdentity(user.UserName);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                    foreach (UserRole role in user.Roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                    }

                    Context.User = new GenericPrincipal(identity, user.Roles.Select(c => roles.FirstOrDefault(q => q.Id == c.RoleId)?.Name).ToArray());
                }
            }
        }

        protected void Application_EndRequest()
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeData = urlHelper.RouteCollection.GetRouteData(currentContext);
            if (routeData == null)
            {
                return;
            }

            string controller = routeData.Values["controller"] as string;

            if (!string.IsNullOrEmpty(controller) &&
                controller.Equals("Error"))
            {
                return;
            }

            Exception ex = Server.GetLastError();
            Trace.WriteLine($"Application_Error: Message: {ex.Message} \nTrace:{ex.StackTrace}");
            HttpException httpException = ex as HttpException;

            if (ex is CryptographicException)
            {
                Server.ClearError();
            }
            ContextSession.LastError = ex;
        }
    }
}
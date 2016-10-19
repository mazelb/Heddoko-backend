using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            HttpApplication app = (HttpApplication) source;
            HttpContext context = app.Context;

            Config.Initialise(context);
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string token = Context.Request.Headers[Constants.HeaderToken];

            if (!string.IsNullOrEmpty(token))
            {
                UnitOfWork uow = new UnitOfWork();
                User user = uow.AccessTokenRepository.GetByToken(token)?.User;
                if (user == null)
                {
                    return;
                }

                if (user.IsActive)
                {
                    List<UserRole> roles = uow


                    Context.User = new GenericPrincipal(new GenericIdentity(user.Id.ToString()), user.Roles.Select(c => c.RoleId.ToString()).ToArray());
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
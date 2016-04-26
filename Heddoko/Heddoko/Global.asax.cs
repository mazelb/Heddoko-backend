using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using DAL.Repository;
using DAL;
using DAL.Models;
using System.Security.Principal;
using Heddoko.Helpers.Auth;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Heddoko
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            Config.Initialise(context);
        }


        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            string token = Context.Request.Headers[DAL.Constants.HeaderToken];

            if (!string.IsNullOrEmpty(token))
            {
                HDContext db = new HDContext();
                UserRepository userRepo = new UserRepository(db);
                User user = userRepo.GetByEmail(token);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        Context.User = new GenericPrincipal(new System.Security.Principal.GenericIdentity(user.ID.ToString()), user.Roles.ToArray());
                    }
                }
            }
            else
            {
                if (FormsAuthentication.CookiesSupported == true)
                {
                    HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (authCookie == null || authCookie.Value == string.Empty)
                    {
                    }
                    else
                    {

                        FormsAuthenticationTicket authTicket = null;
                        authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                        if (authTicket.UserData == null)
                        {
                            return;
                        }
                        AuthCookie data = JsonConvert.DeserializeObject<AuthCookie>(authTicket.UserData);

                        Context.User = new GenericPrincipal(new System.Security.Principal.GenericIdentity(authTicket.Name), data.Roles.ToArray());
                    }
                }
            }
        }

        protected void Application_EndRequest()
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            RouteData routeData = urlHelper.RouteCollection.GetRouteData(currentContext);
            string controller = routeData.Values["controller"] as string;
            if (string.IsNullOrEmpty(controller)
            || !controller.Equals("Error"))
            {
                Exception ex = Server.GetLastError();
                Trace.WriteLine(string.Format("Application_Error: Message: {0} \nTrace:{1}", ex.Message, ex.StackTrace));
                var httpException = ex as HttpException;
                if (ex != null)
                {
                    if (ex is CryptographicException)
                    {
                        Server.ClearError();
                        Forms.SignOut();
                    }
                    ContextSession.LastError = ex;
                }
            }
        }
    }
}
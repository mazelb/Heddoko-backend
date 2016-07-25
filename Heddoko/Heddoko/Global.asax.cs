using System;
using System.Diagnostics;
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
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        Context.User = new GenericPrincipal(new GenericIdentity(user.ID.ToString()), user.Roles.ToArray());
                    }
                }
            }
            else
            {
                if (FormsAuthentication.CookiesSupported)
                {
                    HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (authCookie == null ||
                        authCookie.Value == string.Empty)
                    {
                    }
                    else
                    {
                        FormsAuthenticationTicket authTicket = null;
                        authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                        if (authTicket != null && authTicket.UserData == null)
                        {
                            return;
                        }
                        AuthCookie data = JsonConvert.DeserializeObject<AuthCookie>(authTicket.UserData);

                        Context.User = new GenericPrincipal(new GenericIdentity(authTicket.Name), data.Roles.ToArray());
                    }
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
            string controller = routeData.Values["controller"] as string;
            if (string.IsNullOrEmpty(controller)
                ||
                !controller.Equals("Error"))
            {
                Exception ex = Server.GetLastError();
                Trace.WriteLine(string.Format("Application_Error: Message: {0} \nTrace:{1}", ex.Message, ex.StackTrace));
                HttpException httpException = ex as HttpException;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Heddoko
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
             name: "Confirm",
             url: "confirm/{token}",
             defaults: new { controller = "Account", action = "Confirm", token = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Forgot",
               url: "forgot/{token}",
               defaults: new { controller = "Account", action = "Forgot", token = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

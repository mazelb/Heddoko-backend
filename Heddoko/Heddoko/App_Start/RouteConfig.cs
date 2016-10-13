using System.Web.Mvc;
using System.Web.Routing;

namespace Heddoko
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Admin",
                "admin",
                new
                {
                    controller = "Admin",
                    action = "Index",
                    token = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "Inventory",
                "inventory",
                new
                {
                    controller = "Inventory",
                    action = "Index",
                    token = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "Login",
                "login",
                new
                {
                    controller = "Account",
                    action = "SignIn",
                    token = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "Logout",
                "logout",
                new
                {
                    controller = "Account",
                    action = "SignOut",
                    token = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "SignUp",
                "register",
                new
                {
                    controller = "Account",
                    action = "SignUp",
                    token = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "SignUpOrganization",
                "register/{organizationID}",
                new
                {
                    controller = "Account",
                    action = "SignUpOrganization",
                    token = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "Invite",
                "invite/{token}",
                new
                {
                    controller = "Account",
                    action = "SignUpOrganization",
                    token = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "Confirm",
                "confirm/{userId}/{code}",
                new
                {
                    controller = "Account",
                    action = "Confirm",
                    userId = UrlParameter.Optional,
                    code = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "Forgot",
                "forgot/{token}",
                new
                {
                    controller = "Account",
                    action = "Forgot",
                    token = UrlParameter.Optional
                }
                );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Default",
                    action = "Index",
                    id = UrlParameter.Optional
                }
                );
        }
    }
}
using System.Web.Mvc;
using System.Web.Routing;
using Heddoko.Helpers.DomainRouting;

namespace Heddoko
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            var mainSiteDomain = Helpers.UrlHelper.GetHost(DAL.Config.DashboardSite);
            var openApiDomain = Helpers.UrlHelper.GetHost(DAL.Config.PublicApiSite);

            routes.MapRoute(
                "Authorize",
                "authorize",
                new
                {
                    controller = "OAuth",
                    action = "Authorize",
                    token = UrlParameter.Optional
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(openApiDomain)
                }
                );

            routes.MapRoute(
                "Admin",
                "admin",
                new
                {
                    controller = "Admin",
                    action = "Index",
                    token = UrlParameter.Optional
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
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
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
                }
                );

            routes.MapRoute(
                "Approve",
                "approve",
                new
                {
                    controller = "Development",
                    action = "Approve",
                    token = UrlParameter.Optional
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
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
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain, openApiDomain)
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
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain, openApiDomain)
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
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
                }
                );

            routes.MapRoute(
                "SignUpOrganization",
                "register/{organizationID}",
                new
                {
                    controller = "Account",
                    action = "SignUpOrganization",
                    organizationID = UrlParameter.Optional
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
                }
                );

            routes.MapRoute(
                "Invite",
                "invite/{userId}/{*code}",
                new
                {
                    controller = "Account",
                    action = "SignUpOrganization",
                    userId = UrlParameter.Optional,
                    code = UrlParameter.Optional
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
                }
                );

            routes.MapRoute(
                "Confirm",
                "confirm/{userId}/{*code}",
                new
                {
                    controller = "Account",
                    action = "Confirm",
                    userId = UrlParameter.Optional,
                    code = UrlParameter.Optional
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
                }
                );

            routes.MapRoute(
                "Forgot",
                "forgot/{userId}/{*code}",
                new
                {
                    controller = "Account",
                    action = "Forgot",
                    userId = UrlParameter.Optional,
                    code = UrlParameter.Optional
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
                }
                );

            routes.MapRoute(
                "Error",
                "Error/{action}/{url}",
                new
                {
                    controller = "Error",
                    url = UrlParameter.Optional
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain, openApiDomain)
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
                },
                constraints: new
                {
                    domain = new DomainRouteConstraint(mainSiteDomain)
                }
                );
        }
    }
}
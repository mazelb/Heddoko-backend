using System.Collections.Generic;
using System.Linq;
using Hangfire.Dashboard;
using Microsoft.Owin;

namespace Heddoko.Helpers.Hangfire
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public HangfireAuthorizationFilter(params string[] roles)
        {
            Roles = roles;
        }

        private IEnumerable<string> Roles { get; set; }

        public bool Authorize(DashboardContext context)
        {
            var environment = context.GetOwinEnvironment();
            OwinContext owinContext = new OwinContext(environment);

            return Roles.Aggregate(false, (current, role) => current || owinContext.Request.User.IsInRole(role));
        }
    }
}
using System.Collections.Generic;
using Hangfire.Dashboard;

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
            //TODO wait for 1.6.1 version
            //   AspNetCoreDashboardContext
            //   var result = Roles.Aggregate(false, (current, role) => current || httpContext.User.IsInRole(role));

            return false;
        }
    }
}
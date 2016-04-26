using DAL;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Heddoko.Startup))]
namespace Heddoko
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(Config.ConnectionString);

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                AuthorizationFilters = new[] { new AuthorizationFilter
                    {
                        Roles = Constants.Roles.Admin
                    }
                }
            });
        }
    }
}
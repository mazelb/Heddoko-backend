using DAL;
using Hangfire;
using Hangfire.Dashboard;
using Heddoko;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Heddoko
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(DAL.Config.ConnectionString);

            app.UseHangfireDashboard("/hangfire",
                new DashboardOptions
                {
                    //TODO wait for 1.6.1 version
                    //Authorization = new[]
                    //{
                    //    new HangfireAuthorizationFilter(Constants.Roles.Admin)
                    //}
                    AuthorizationFilters = new[]
                    {
                        new AuthorizationFilter
                        {
                            Roles = Constants.Roles.Admin
                        }
                    }
                });

            ConfigureAuth(app);
        }
    }
}
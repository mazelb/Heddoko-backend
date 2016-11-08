using DAL;
using Hangfire;
using Heddoko;
using Heddoko.Helpers.Hangfire;
using Heddoko.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Services;

[assembly: OwinStartup(typeof(Startup))]
namespace Heddoko
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(DAL.Config.ConnectionString);
            app.CreatePerOwinContext(UnitOfWork.Create);
            ConfigureAuth(app);

            app.UseHangfireDashboard("/hangfire",
                new DashboardOptions
                {
                    Authorization = new[]
                    {
                        new HangfireAuthorizationFilter(Constants.Roles.Admin)
                    }
                });


            GlobalHost.DependencyResolver.Register(
                typeof(StreamingHub),
                () => new StreamingHub(new StreamConnectionsService(new UnitOfWork())));

            app.MapSignalR();
        }
    }
}
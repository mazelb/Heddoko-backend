/**
 * @file Startup.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL;
using DAL.Models;
using Hangfire;
using Heddoko;
using Heddoko.Helpers.Hangfire;
using Heddoko.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using MongoDB.Bson.Serialization;
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

            BsonClassMap.RegisterClassMap<License>();
        }
    }
}
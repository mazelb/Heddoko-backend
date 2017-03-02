/**
 * @file HeddokoService.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using Hangfire;

namespace HeddokoService
{
    partial class HeddokoService : ServiceBase
    {
        private readonly BackgroundJobServer Server;
        private ManualResetEvent ShutdownEvent;
        private Thread Thread;

        private const string LicenseManagerCheck = "LicenseManager.Check";
        private const string AssembliesManagerCheck = "AssembliesManager.GetAssemblies";
        private const string LicenseManagerCheckExpiring = "LicenseManager.CheckExpiring";
        private const string LicenseManagerCheckExpiringForAdmins = "LicenseManager.CheckExpiringForAdmins";

        public HeddokoService()
        {
            InitializeComponent();

            HangFireOption option = HangFireOptions.Get();
            Server = new BackgroundJobServer(option.Options, option.Storage);
            JobStorage.Current = option.Storage;
            ShutdownEvent = new ManualResetEvent(false);
        }


        protected void Run()
        {
            while (!ShutdownEvent.WaitOne(0))
            {
                System.Threading.Thread.Sleep(Config.WaitMiliSeconds);
            }
        }

        protected override void OnStart(string[] args)
        {
            if (args != null)
            {
                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        case "-flush":
                            Trace.TraceInformation("FLUSHALL is started");

                            RedisManager.Flush();

                            Trace.TraceInformation("FLUSHALL is stoped");
                            break;
                    }
                }
            }

            RecurringJob.AddOrUpdate(LicenseManagerCheck, () => Services.LicenseManager.Check(), Cron.Hourly());
            RecurringJob.AddOrUpdate(AssembliesManagerCheck, () => Services.AssembliesManager.GetAssemblies(true), Cron.Daily());

            RecurringJob.AddOrUpdate(LicenseManagerCheckExpiring, () => Services.LicenseManager.CheckExpiring(Config.DaysOnExpiringOrganizationsEmail), Cron.Daily());
            RecurringJob.AddOrUpdate(LicenseManagerCheckExpiringForAdmins, () => Services.LicenseManager.CheckExpiringForAdmins(Config.DaysOnExpiringAdminsEmail), Cron.Daily());

            Thread = new Thread(Run)
            {
                Name = "LongChecker",
                IsBackground = true
            };

            Thread.Start();

            Trace.TraceInformation("Start service");
        }

        protected override void OnStop()
        {
            RecurringJob.RemoveIfExists(LicenseManagerCheck);
            RecurringJob.RemoveIfExists(AssembliesManagerCheck);
            RecurringJob.RemoveIfExists(LicenseManagerCheckExpiring);
            RecurringJob.RemoveIfExists(LicenseManagerCheckExpiringForAdmins);

            ShutdownEvent.Set();
            if (!Thread.Join(3000))
            {
                Thread.Abort();
            }

            Server.Dispose();
            Trace.TraceInformation("Stop service");
        }
    }
}

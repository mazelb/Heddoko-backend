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
            RecurringJob.AddOrUpdate(LicenseManagerCheck, () => Services.LicenseManager.Check(), Cron.Hourly());
            RecurringJob.AddOrUpdate(AssembliesManagerCheck, () => Services.AssembliesManager.GetAssemblies(), Cron.Daily);


            Thread = new Thread(Run);
            Thread.Name = "LongChecker";
            Thread.IsBackground = true;
            Thread.Start();

            Trace.TraceInformation("Start service");
        }

        protected override void OnStop()
        {
            RecurringJob.RemoveIfExists(LicenseManagerCheck);
            RecurringJob.RemoveIfExists(AssembliesManagerCheck);

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

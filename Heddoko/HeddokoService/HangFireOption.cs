﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;

namespace HeddokoService
{
    class HangFireOption
    {
        public SqlServerStorage Storage { get; set; }
        public BackgroundJobServerOptions Options { get; set; }
    }

    class HangFireOptions
    {
        public static HangFireOption Get()
        {
            SqlServerStorageOptions optionsSql = new SqlServerStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(1)
            };
            HangFireOption option = new HangFireOption();
            option.Storage = new SqlServerStorage(DAL.Config.ConnectionString, optionsSql);
            option.Options = new BackgroundJobServerOptions()
            {
                WorkerCount = Config.WorkerCount,
                Queues = new[] {
                    DAL.Constants.HangFireQueue.Default
                }
            };

            return option;
        }
    }
}
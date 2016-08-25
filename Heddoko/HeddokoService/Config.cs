using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeddokoService
{
    internal class Config : DAL.Config
    {
        public static int WorkerCount => int.Parse(ConfigurationManager.AppSettings["WorkerCount"]);

        public static string ServiceName => ConfigurationManager.AppSettings["ServiceName"];

        public static int WaitMiliSeconds => int.Parse(ConfigurationManager.AppSettings["WaitMiliSeconds"]);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportConsole
{
    internal class Config : DAL.Config
    {
        public static string File => ConfigurationManager.AppSettings["File"];
    }
}

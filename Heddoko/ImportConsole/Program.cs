﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Excel.ImportSensors(Config.File);
        }
    }
}

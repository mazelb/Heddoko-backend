/**
 * @file Program.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
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

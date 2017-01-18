/**
 * @file Config.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
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

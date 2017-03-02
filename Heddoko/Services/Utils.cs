/**
 * @file Utils.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.IO;
using System.Collections.Generic;

namespace Services
{
    public class Utils
    {
        public static string DownloadFolder
        {
            get
            {
                return Path.Combine(Config.BaseDirectory, "Downloads");
            }
        }
        public static string DownloadPath()
        {
            return Path.Combine(DownloadFolder, Guid.NewGuid().ToString());
        }
    }
}

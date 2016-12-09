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

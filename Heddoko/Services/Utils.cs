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

        public static DateTime ConvertFromUnixTimestamp(uint timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static uint ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return (uint)Math.Floor(diff.TotalSeconds);
        }
    }
}

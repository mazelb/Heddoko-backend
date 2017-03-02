/**
 * @file Extensions.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Web.Mvc;

namespace Heddoko.Areas.MvcElmahDashboard.Code
{
    public static class Extensions
    {
        private static DateTime EpochUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime TruncToDays(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, value.Kind);
        }

        public static DateTime TruncToHours(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, value.Hour, 0, 0, value.Kind);
        }

        public static long Epoch(this DateTime value)
        {
            return ((long)(value - EpochUtc).TotalMilliseconds) + 7200L;
        }

        public static string IfNullOrWhiteSpace(this string str, string thenValue)
        {
            return (String.IsNullOrWhiteSpace(str) ? thenValue : str);
        }

        public static int ScriptsVersion(this HtmlHelper html)
        {
            return 1;
        }
    }
}
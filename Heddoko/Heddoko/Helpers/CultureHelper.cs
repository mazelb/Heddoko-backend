using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace Heddoko
{
    public class CultureHelper
    {
        public static void Set(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public static CultureInfo Get()
        {
            return Thread.CurrentThread.CurrentCulture;
        }

        public static string GetLang()
        {
            CultureInfo info = Get();
            if (info.IsNeutralCulture)
            {
                return info.Name;
            }
            else
            {
                return info.Parent.Name;
            }
        }
    }
}
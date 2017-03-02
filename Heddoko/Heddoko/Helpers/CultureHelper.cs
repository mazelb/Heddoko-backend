/**
 * @file CultureHelper.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Globalization;
using System.Threading;

namespace Heddoko
{
    public class CultureHelper
    {
        public static void Set(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private static CultureInfo Get()
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
            return info.Parent.Name;
        }
    }
}
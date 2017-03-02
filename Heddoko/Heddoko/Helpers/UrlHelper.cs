/**
 * @file UrlHelper.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;

namespace Heddoko.Helpers
{
    public static class UrlHelper
    {
        public static string GetHost(string url)
        {
            var uri = new Uri(url);

            return uri.Host;
        }
    }
}
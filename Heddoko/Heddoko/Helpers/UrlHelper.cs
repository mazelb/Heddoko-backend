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
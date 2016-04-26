using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Heddoko
{
    public class Config : DAL.Config
    {
        public static int EmailForgotTokenExpiration
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["EmailForgotTokenExpiration"]);
            }
        }

        #region Host
        public static string Host
        {
            get
            {
                return _host;
            }
        }

        public static string ApplicationPath
        {
            get
            {
                return _applicationPath;
            }
        }

        private static string _host = null;
        private static string _applicationPath = null;

        private static Object _lock = new Object();

        public static void Initialise(HttpContext context)
        {
            if (string.IsNullOrEmpty(_host))
            {
                lock (_lock)
                {
                    if (string.IsNullOrEmpty(_host))
                    {
                        Uri uri = HttpContext.Current.Request.Url;
                        string appPath = HttpContext.Current.Request.ApplicationPath;
                        if (uri.Port == 80
                        || uri.Port == 443)
                        {
                            _host = string.Format("{0}{1}{2}{3}", uri.Scheme, Uri.SchemeDelimiter, uri.Host, appPath);
                        }
                        else
                        {
                            _host = string.Format("{0}{1}{2}{3}:{4}", uri.Scheme, Uri.SchemeDelimiter, uri.Host, appPath, uri.Port);
                        }
                        _host = _host.TrimEnd('/');

                        _applicationPath = HttpContext.Current.Server.MapPath("~/").TrimEnd('\\');

                        DAL.Models.BaseModel.AssetsServer = string.Empty;

                        Trace.TraceInformation(string.Format("Host: {0}", Host));
                        Trace.TraceInformation(string.Format("ApplicationPath : {0}", ApplicationPath));
                    }
                }
            }
        }
        #endregion
    }
}
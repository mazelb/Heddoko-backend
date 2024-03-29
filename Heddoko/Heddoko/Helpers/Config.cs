﻿/**
 * @file Config.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using DAL.Models;

namespace Heddoko
{
    public class Config : DAL.Config
    {
        public static int EmailForgotTokenExpiration => int.Parse(ConfigurationManager.AppSettings["EmailForgotTokenExpiration"]);
        
        public static string CIUploadToken => ConfigurationManager.AppSettings["CIUploadToken"];
        
        #region Host

        private static string Host { get; set; }

        public static string ApplicationPath { get; private set; }

        private static readonly object Lock = new object();

        public static void Initialise(HttpContext context)
        {
            if (!string.IsNullOrEmpty(Host))
            {
                return;
            }

            lock (Lock)
            {
                if (!string.IsNullOrEmpty(Host))
                {
                    return;
                }

                Uri uri = HttpContext.Current.Request.Url;
                string appPath = HttpContext.Current.Request.ApplicationPath;
                if (uri.Port == 80
                    ||
                    uri.Port == 443)
                {
                    Host = $"{uri.Scheme}{Uri.SchemeDelimiter}{uri.Host}{appPath}";
                }
                else
                {
                    Host = $"{uri.Scheme}{Uri.SchemeDelimiter}{uri.Host}{appPath}:{uri.Port}";
                }
                Host = Host.TrimEnd('/');

                ApplicationPath = HttpContext.Current.Server.MapPath("~/").TrimEnd('\\');

                BaseModel.AssetsServer = AssetsServer;

                Trace.TraceInformation($"Host: {Host}");
                Trace.TraceInformation($"ApplicationPath : {ApplicationPath}");
            }
        }

        #endregion
    }
}
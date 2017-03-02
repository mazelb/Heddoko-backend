/**
 * @file Config.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Configuration;

namespace DAL
{
    public class Config
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["HDContext"]?.ToString();

        public static bool AllowInitData => bool.Parse(ConfigurationManager.AppSettings["AllowInitData"]);

        public static string Environment => ConfigurationManager.AppSettings["Environment"];

        public static string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;

        public static string HomeSite => ConfigurationManager.AppSettings["HomeSite"];

        public static string DashboardSite => ConfigurationManager.AppSettings[Constants.ConfigKeyName.DashboardSite];

        public static string PublicApiSite => ConfigurationManager.AppSettings[Constants.ConfigKeyName.PublicApiSite];

        public static string SendgridKey => ConfigurationManager.AppSettings["SendgridKey"];

        public static string MailFrom => ConfigurationManager.AppSettings["MailFrom"];

        public static string WrikeEmail => ConfigurationManager.AppSettings["WrikeEmail"];

        #region Push
        public static bool IOSSandbox => bool.Parse(ConfigurationManager.AppSettings["IOSSandbox"]);

        public static string IOSCertificate => ConfigurationManager.AppSettings["IOSCertificate"];

        public static string NotificationsHub => ConfigurationManager.AppSettings["NotificationsHub"];

        public static string GcmSenderId => ConfigurationManager.AppSettings["GcmSenderId"];

        public static string GcmSenderAuthToken => ConfigurationManager.AppSettings["GcmSenderAuthToken"];
        #endregion

        #region Mongo
        public static string MongoDbConnectionString => ConfigurationManager.ConnectionStrings["MongoDb"]?.ToString();
        public static string MongoDbName => ConfigurationManager.AppSettings["MongoDbName"];
        #endregion

        #region JWT

        public static string JwtSecret => ConfigurationManager.AppSettings["JWTSecret"];

        #endregion

        #region Redis 

        public static string RedisConnectionString => ConfigurationManager.AppSettings["RedisConnectionString"];

        public static int RedisCacheExpiration => int.Parse(ConfigurationManager.AppSettings["RedisCacheExpiration"]);

        #endregion

        #region Azure

        public static string StorageConnectionString => ConfigurationManager.AppSettings["StorageConnectionString"];

        public static string AssetsContainer => ConfigurationManager.AppSettings["AssetsContainer"];

        public static string AssetsServer => ConfigurationManager.AppSettings["AssetsServer"];

        #endregion
    }
}
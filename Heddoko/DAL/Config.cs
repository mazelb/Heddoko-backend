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

        public static string DashboardSite => ConfigurationManager.AppSettings["DashboardSite"];

        public static string SendgridKey => ConfigurationManager.AppSettings["SendgridKey"];

        public static string MailFrom => ConfigurationManager.AppSettings["MailFrom"];

        public static string WrikeEmail => ConfigurationManager.AppSettings["WrikeEmail"];

        public static string MongoDbConnectionString => ConfigurationManager.ConnectionStrings["MongoDb"]?.ToString();

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
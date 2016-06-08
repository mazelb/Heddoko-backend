using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Config
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["HDContext"].ToString();

            }
        }

        public static bool AllowInitData
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["AllowInitData"]);
            }
        }

        public static string Environment
        {
            get
            {
                return ConfigurationManager.AppSettings["Environment"];
            }
        }

        public static string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string HomeSite
        {
            get
            {
                return ConfigurationManager.AppSettings["HomeSite"];
            }
        }

        public static string DashboardSite
        {
            get
            {
                return ConfigurationManager.AppSettings["DashboardSite"];
            }
        }

        #region Redis 

        public static string RedisConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["RedisConnectionString"];
            }
        }

        public static int RedisCacheExpiration
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["RedisCacheExpiration"]);
            }
        }
        #endregion

        #region Azure


        public static string StorageConnectionString
        {
            get
            {
                return ConfigurationManager.AppSettings["StorageConnectionString"];

            }
        }

        public static string AssetsContainer
        {
            get
            {
                return ConfigurationManager.AppSettings["AssetsContainer"];
            }
        }

        public static int AssetsEndpoint
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["AssetsEndpoint"]);
            }
        }

        public static string AssetsServer
        {
            get
            {
                return ConfigurationManager.AppSettings["AssetsServer"];
            }
        }
        #endregion

        #region JWT
        public static string JWTSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["JWTSecret"];
            }
        }
        #endregion
    }
}

/**
 * @file Config.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Configuration;

namespace Services
{
    public class Config : DAL.Config
    {
        public static string Username => ConfigurationManager.AppSettings["Username"];

        public static string Password => ConfigurationManager.AppSettings["Password"];

        public static string OauthUrl => $"{DAL.Config.DashboardSite}/oauth2/token";

        public static string TokenHeaderName => ConfigurationManager.AppSettings["TokenHeaderName"];
    }
}

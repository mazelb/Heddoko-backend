using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Constants
    {
        public const string ConnectionStringName = "HDContext";
        public const string HeaderToken = "token";

        public class Roles
        {
            public const string User = "User";
            public const string Analyst = "Analyst";
            public const string Admin = "Admin";
            public const string AdminAndAnalystAndUser = "User,Analyst,Admin";
            public const string UserAndAdmin = "User,Admin";
            public const string AnalystAndAdmin = "Analyst,Admin";
        }

        public class Languages
        {
            public const string En = "en";
            public const string EnUS = "en-US";
        }

        public class HangFireQueue
        {
            public const string Default = "default";
        }

        public class Cache
        {
            public const string Prefix = "heddoko_dev";
            public const string KeyALL = "all";
            public const string Users = "users";
        }
    }
}

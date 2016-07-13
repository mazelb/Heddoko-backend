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

        public const int EmailLimit = 20871520;

        public class Embed
        {
            public const string Groups = "groups";
            public const string Profiles = "profiles";
            public const string Tags = "tags";
            public const string AvatarSrc = "avatarSrc";
        }

        public class Environments
        {
            public const string Dev = "dev";
            public const string Stage = "stage";
            public const string Prod = "prod";
        }

        public class Assets
        {
            public const string Group = "groups";
            public const string User = "users";
            public const string Profile = "profile";
            public const string Seed = "seed";
        }

        public class Roles
        {
            public const string User = "User";
            public const string Analyst = "Analyst";
            public const string LicenseAdmin = "LicenseAdmin";
            public const string Worker = "Worker";
            public const string Admin = "Admin";
            public const string All = "User,Analyst,Admin,LicenseAdmin,Worker";
            public const string UserAndAdmin = "User,Admin";
            public const string AnalystAndAdmin = "Analyst,Admin";
            public const string LicenseAdminAndAdmin = "LicenseAdmin,Admin";
            public const string LicenseAdminAndWorkerAndAnalyst = "LicenseAdmin,Analyst,Worker";
            public const string LicenseAdminAndWorkerAndAnalystAndAdmin = "LicenseAdmin,Analyst,Worker,Admin";
            public const string WorkerAndAdmin = "Worker,Admin";
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

namespace DAL
{
    public static class Constants
    {
        public const string ConnectionStringName = "HDContext";
        public const string HeaderToken = "token";

        public const int EmailLimit = 20871520;
        public const string PadZero = "D6";
        public const string SystemUser = "System";

        public static class Embed
        {
            public const string Groups = "groups";
            public const string Profiles = "profiles";
            public const string Tags = "tags";
            public const string AvatarSrc = "avatarSrc";
        }

        public static class Environments
        {
            public const string Dev = "dev";
            public const string Stage = "stage";
            public const string Prod = "prod";
        }

        public static class Assets
        {
            public const string Group = "groups";
            public const string User = "users";
            public const string Profile = "profile";
            public const string Seed = "seed";
            public const string Firmware = "firmware";
            public const string Log = "logs";
            public const string SystemLog = "systemLogs";
            public const string Setting = "settings";
            public const string Record = "records";
        }

        public static class Roles
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

        public static class Languages
        {
            public const string En = "en";
            public const string EnUS = "en-US";
        }

        public static class HangFireQueue
        {
            public const string Default = "default";
            public const string Email = "email";
        }

        public static class Cache
        {
            public const string Prefix = "heddoko";
            public const string KeyAll = "all";
            public const string Users = "users";
            public const string Assembly = "assembly";
        }

        public static class CacheExpiration
        {
            public const int Assembly = 48;
        }

        public static class AuditFieldName
        {
            public const string Notes = "Notes";
        }
    }
}
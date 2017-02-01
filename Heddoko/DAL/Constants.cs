/**
 * @file Constants.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
            public const string DefaultRecords = "defaultrecords";
            public const string Guide = "guide";
            public const string ProcessedFrameData = "processedFrameData";
            public const string AnalysisFrameData = "analysisFrameData";
            public const string RawFrameData = "rawFrameData";
        }

        public static class Roles
        {
            public const string User = "User";
            public const string Analyst = "Analyst";
            public const string LicenseAdmin = "LicenseAdmin";
            public const string Worker = "Worker";
            public const string Admin = "Admin";
            public const string ServiceAdmin = "ServiceAdmin";
            public const string LicenseUniversal = "LicenseUniversal";
            public const string All = "User,Analyst,Admin,LicenseAdmin,Worker,ServiceAdmin,LicenseUniversal";
            public const string UserAndAdmin = "User,Admin";
            public const string AnalystAndAdmin = "Analyst,Admin";
            public const string LicenseAdminAndAdmin = "LicenseAdmin,Admin";
            public const string LicenseAdminAndAnalystAndAdmin = "LicenseAdmin,Analyst,Admin";
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
            public const string Notifications = "notifications";
        }

        public static class Cache
        {
            public const string Prefix = "heddoko";
            public const string KeyAll = "all";
            public const string Users = "users";
            public const string Assembly = "assembly";
            public const string StreamConnections = "connections";
        }

        public static class CacheExpiration
        {
            public const int Assembly = 48;
        }

        public static class AuditFieldName
        {
            public const string Notes = "Notes";
        }

        public static class Records
        {
            public const int MinFilesCount = 1;
            public const int MaxFilesCount = 5;
        }

        public static class ConfigKeyName
        {
            public const string DashboardSite = "DashboardSite";
            public const string PublicApiSite = "PublicApiSite";
            public const string AllSites = "DashboardSite,PublicApiSite";
        }

        public static class OpenAPIClaims
        {
            public const string ClaimType = "heddoko:open:api";
            public const string ClaimValue = "authorized";
        }

        public static class ClaimTypes
        {
            public const string ParentLoggedInUser = "ParentLoggedInUser";
        }    }
}
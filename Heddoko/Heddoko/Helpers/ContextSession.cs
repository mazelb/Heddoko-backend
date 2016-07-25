using System;
using System.Web;
using System.Web.SessionState;
using DAL.Models;

namespace Heddoko
{
    public static class ContextSession
    {
        private const string LastErrorKey = "LastError";
        private const string CurrentUserKey = "CurrentUser";
        private const string NewUserKey = "NewUser";
        private const string ResendUserKey = "ResendUser";
        private const string CurrentCultureKey = "CurrentCulture";
        private const string LoggedAsKey = "LoggedAs";
        private const string GoogleOAuthProvider = "GoogleOAuthProvider";

        public static HttpSessionState Current => HttpContext.Current.Session;

        public static User User
        {
            get { return Current.Get<User>(CurrentUserKey); }
            set { Current.Set(CurrentUserKey, value); }
        }

        public static User NewUser
        {
            get { return Current.Get<User>(NewUserKey); }
            set { Current.Set(NewUserKey, value); }
        }

        public static User ResendUser
        {
            get { return Current.Get<User>(ResendUserKey); }
            set { Current.Set(ResendUserKey, value); }
        }

        public static User LoggedAs
        {
            get { return Current.Get<User>(LoggedAsKey); }
            set { Current.Set(LoggedAsKey, value); }
        }

        public static Exception LastError
        {
            get { return Current.Get<Exception>(LastErrorKey); }
            set { Current.Set(LastErrorKey, value); }
        }
    }
}
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Heddoko
{
    public class ContextSession
    {
        private const string LastErrorKey = "LastError";
        private const string CurrentUserKey = "CurrentUser";
        private const string NewUserKey = "NewUser";
        private const string ResendUserKey = "ResendUser";
        private const string CurrentCultureKey = "CurrentCulture";
        private const string LoggedAsKey = "LoggedAs";
        private const string GoogleOAuthProvider = "GoogleOAuthProvider";

        public static HttpSessionState Current
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static User User
        {
            get
            {
                return ContextSession.Current.Get<User>(CurrentUserKey);
            }
            set
            {
                ContextSession.Current.Set(CurrentUserKey, value);
            }
        }

        public static User NewUser
        {
            get
            {
                return ContextSession.Current.Get<User>(NewUserKey);
            }
            set
            {
                ContextSession.Current.Set(NewUserKey, value);
            }
        }

        public static User ResendUser
        {
            get
            {
                return ContextSession.Current.Get<User>(ResendUserKey);
            }
            set
            {
                ContextSession.Current.Set(ResendUserKey, value);
            }
        }

        public static User LoggedAs
        {
            get
            {
                return ContextSession.Current.Get<User>(LoggedAsKey);
            }
            set
            {
                ContextSession.Current.Set(LoggedAsKey, value);
            }
        }

        public static Exception LastError
        {
            get
            {
                return ContextSession.Current.Get<Exception>(LastErrorKey);
            }
            set
            {
                ContextSession.Current.Set(LastErrorKey, value);
            }
        }
    }
}
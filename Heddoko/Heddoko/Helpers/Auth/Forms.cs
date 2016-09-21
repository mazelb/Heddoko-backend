using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using DAL;
using DAL.Models;
using Newtonsoft.Json;

namespace Heddoko.Helpers.Auth
{
    public static class Forms
    {
        private const int Version = 1;

        public static User SignIn(UnitOfWork uow, string username, string password)
        {
            User user = uow.UserRepository.GetByUsernameCached(username?.ToLower().Trim());

            //if (user != null
            //    &&
            //    PasswordHasher.Equals(password?.Trim(), user.Salt, user.Password))
            //{
            //    return user;
            //}

            return null;
        }

        public static User Authorize(User user)
        {
            SignOut();

            AuthCookie data = new AuthCookie
            {
                ID = user.ID,
                Roles = new List<string>
                {
                    user.Role.GetStringValue()
                }
            };

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                Version,
                user.ID.ToString(),
                DateTime.Now,
                DateTime.Now.AddMonths(1),
                true,
                JsonConvert.SerializeObject(data)
                );
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            HttpContext.Current.Response.Cookies.Add(authCookie);

            ContextSession.User = user;

            return user;
        }

        public static User ValidateSession(UnitOfWork uow, bool isForce = false)
        {
            User user = ContextSession.User;
            if ((user == null || isForce)
                &&
                HttpContext.Current.User != null
                &&
                HttpContext.Current.User.Identity != null
                &&
                HttpContext.Current.User.Identity.IsAuthenticated
                )
            {
                user = uow.UserRepository.GetIDCached(int.Parse(HttpContext.Current.User.Identity.Name));
                ContextSession.User = user;

                if (user != null
                    &&
                    user.IsBanned)
                {
                    SignOut();
                    HttpContext.Current.Response.Redirect("", true);
                }
            }
            if (user == null)
            {
                SignOut();
            }
            return user;
        }

        public static void SignOut()
        {
            ContextSession.User = null;
            HttpContext.Current.User = null;
            FormsAuthentication.SignOut();
        }
    }
}
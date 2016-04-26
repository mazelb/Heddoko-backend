using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Heddoko
{
    public static class Utility
    {
        private const string XRealIP = "X-Real-IP";
        private const string XForwardedFor = "X-Forwarded-For";
        private const string HttpContext = "MS_HttpContext";

        #region HttpRequestBase
        public static string UserIP(this HttpRequestMessage request)
        {
            if (request != null)
            {
                string realIP = request.Headers.FirstOrDefault(c => c.Key == XRealIP).Value?.FirstOrDefault();

                if (String.IsNullOrEmpty(realIP))
                {
                    realIP = request.Headers.FirstOrDefault(c => c.Key == XForwardedFor).Value?.FirstOrDefault();
                }

                if (String.IsNullOrEmpty(realIP))
                {
                    if (request.Properties.ContainsKey(HttpContext))
                    {
                        HttpContextWrapper ctx = (HttpContextWrapper)request.Properties[HttpContext];
                        if (ctx != null)
                        {
                            realIP = ctx.Request.Headers[XRealIP];

                            if (String.IsNullOrEmpty(realIP))
                            {
                                realIP = ctx.Request.Headers[XForwardedFor];
                            }

                            realIP = ctx.Request.UserHostAddress;
                        }
                    }
                }

                return realIP;
            }

            return null;
        }
        public static string UserIP(this HttpRequestBase request)
        {
            if (request != null)
            {
                string realIP = request.Headers[XRealIP];

                if (String.IsNullOrEmpty(realIP))
                {
                    realIP = request.Headers[XForwardedFor];
                }

                return !String.IsNullOrEmpty(realIP) ? realIP : request.UserHostAddress;
            }

            return null;
        }

        public static string UserIP(this HttpContext context)
        {
            if (context != null)
            {
                return context.UserIP();
            }

            return null;
        }
        #endregion
        #region UserRepository
        //public static void UpdateCurrentUser(this IUserRepository repository, User user)
        //{
        //    repository.AttachAndUpdate(user);
        //    ContextSession.User = user;
        //}
        #endregion
        #region TempData
        public static T Get<T>(this TempDataDictionary tempData, string key)
        {

            return tempData.Get<T>(key, () => default(T));
        }


        public static T Get<T>(this TempDataDictionary tempData, string key, Func<T> createDefault)
        {
            if (tempData != null)
            {
                object value = tempData[key];
                if (value != null)
                {
                    return (T)value;
                }
            }
            return createDefault();
        }

        public static void Set(this TempDataDictionary tempData, string key, object obj)
        {
            if (tempData != null)
            {
                if (obj != null)
                {
                    tempData[key] = obj;
                }
                else
                {
                    tempData[key] = null;
                }
            }
        }

        public static void Remove(this TempDataDictionary tempData, string key)
        {
            if (tempData != null)
            {
                tempData[key] = null;
            }
        }
        #endregion
        #region Session
        public static T Get<T>(this HttpSessionState session, string key)
        {

            return session.Get<T>(key, () => default(T));
        }


        public static T Get<T>(this HttpSessionState session, string key, Func<T> createDefault)
        {
            if (session != null)
            {
                object value = session[key];
                if (value != null)
                {
                    return (T)value;
                }
            }
            return createDefault();
        }

        public static void Set(this HttpSessionState session, string key, object obj)
        {
            if (session != null)
            {
                if (obj != null)
                {
                    session[key] = obj;
                }
                else
                {
                    session[key] = null;
                }
            }
        }

        public static void Remove(this HttpSessionState session, string key)
        {
            if (session != null)
            {
                session[key] = null;
            }
        }
        #endregion
    }
}
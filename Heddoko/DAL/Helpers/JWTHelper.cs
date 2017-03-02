/**
 * @file JWTHelper.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using JWT;
using System.Diagnostics;

namespace DAL.Helpers
{
    public static class JwtHelper
    {
        public static string Create(string payload)
        {
            Dictionary<string, object> obj = new Dictionary<string, object>()
            {
                { "token", payload }
            };
            return JsonWebToken.Encode(obj, Config.JwtSecret, JwtHashAlgorithm.HS256);
        }

        public static string Verify(string token)
        {
            try
            {
                var obj = JsonWebToken.DecodeToObject(token, Config.JwtSecret) as IDictionary<string, object>;

                if (obj != null)
                {
                    return (string)obj["token"];
                }
            }
            catch (SignatureVerificationException)
            {
                Trace.TraceError($"JWTHelper.Verify.SignatureVerificationException token:{token}");
            }
            return null;
        }
    }
}
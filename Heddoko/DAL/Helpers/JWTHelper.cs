using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JWT;
using System.Diagnostics;

namespace DAL.Helpers
{
    public class JWTHelper
    {
        public static string Create(string payload)
        {
            var obj = new Dictionary<string, object>()
            {
                { "token", payload }
            };
            return JsonWebToken.Encode(obj, Config.JWTSecret, JwtHashAlgorithm.HS256);
        }

        public static string Verify(string token)
        {
            try
            {
                var obj = JsonWebToken.DecodeToObject(token, Config.JWTSecret) as IDictionary<string, object>;

                return (string)obj["token"];
            }
            catch (SignatureVerificationException)
            {
                Trace.TraceError($"JWTHelper.Verify.SignatureVerificationException token:{token}");
            }
            return null;
        }
    }
}
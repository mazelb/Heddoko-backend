using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Helpers.Auth
{
    public class AuthCookie
    {
        public AuthCookie()
        {
            Roles = new List<string>();
        }
        public int ID { get; set; }

        public List<string> Roles { get; set; }
    }
}
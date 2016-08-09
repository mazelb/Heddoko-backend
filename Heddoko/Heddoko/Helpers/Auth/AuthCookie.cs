using System.Collections.Generic;

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
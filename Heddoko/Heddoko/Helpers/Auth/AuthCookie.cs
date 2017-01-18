/**
 * @file AuthCookie.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
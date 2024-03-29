﻿/**
 * @file Auth.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using DAL;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using DAL.Models;

namespace Heddoko
{
    public static class Auth
    {
        public static User CurrentUser
        {
            get
            {
                ApplicationUserManager manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return manager.FindByIdCached(HttpContext.Current.User.Identity.GetUserId<int>());
            }
        }
    }
}
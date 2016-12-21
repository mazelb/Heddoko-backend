using DAL;
using Heddoko.Controllers;
using Heddoko.Helpers.DomainRouting.Mvc;
using Heddoko.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.AuthorizationServer.Controllers
{
    public class OAuthAccountController : Controller
    {
        [DomainRoute("login", Constants.ConfigKeyName.PublicApiSite)]
        public ActionResult Login()
        {
            var authentication = HttpContext.GetOwinContext().Authentication;
            if (Request.HttpMethod == "POST")
            {
                var isPersistent = !string.IsNullOrEmpty(Request.Form.Get("isPersistent"));

                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Signin")))
                {
                    authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = isPersistent },
                        new ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Request.Form["username"]) }, Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie));
                }
            }

            BaseViewModel model = new BaseViewModel
            {
                Title = i18n.Resources.Login
            };
            return View(model);
        }
    }
}
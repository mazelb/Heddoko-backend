/**
 * @file OAuthController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL;
using DAL.Models;
using Heddoko.Controllers;
using Heddoko.Helpers.DomainRouting.Mvc;
using Heddoko.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using static DAL.Constants;

namespace Heddoko.AuthorizationServer.Controllers
{
    public class OAuthController : BaseController
    {
        public OAuthController(): base() { }

        public OAuthController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

        [HttpGet]
        public ActionResult Authorize(string client_id, string secret)
        {
            ErrorViewModel errModel = new ErrorViewModel();

            if (Response.StatusCode != 200)
            {  
                return View("AuthorizeError", errModel);
            }

            Application client = UoW.ApplicationRepository.GetByClientAndSecret(client_id, secret);

            if (client == null)
            {
                return View("AuthorizeError", errModel);
            }

            var authentication = HttpContext.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie).Result;
            var identity = ticket != null ? ticket.Identity : null;
            if (identity == null)
            {
                authentication.Challenge(DefaultAuthenticationTypes.ApplicationCookie);
                return new HttpUnauthorizedResult();
            }

            AuthorizeViewModel model = new AuthorizeViewModel
            {
                Client = client_id,
                ClientSecret = secret
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Authorize(AuthorizeViewModel model)
        {
            ErrorViewModel errModel = new ErrorViewModel();

            if (Response.StatusCode != 200)
            {
                return View("AuthorizeError", errModel);
            }

            Application client = UoW.ApplicationRepository.GetByClientAndSecret(model.Client, model.ClientSecret);

            if (client == null)
            {
                return View("AuthorizeError", errModel);
            }

            var authentication = HttpContext.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie).Result;
            var identity = ticket != null ? ticket.Identity : null;
            if (identity == null)
            {
                authentication.Challenge(DefaultAuthenticationTypes.ApplicationCookie);
                return new HttpUnauthorizedResult();
            }

            if (!string.IsNullOrEmpty(Request.Form.Get("submit.Grant")))
            {
                identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);
                identity.AddClaim(new Claim(OpenAPIClaims.ClaimType, OpenAPIClaims.ClaimValue));
                authentication.SignIn(identity);
            }
            if (!string.IsNullOrEmpty(Request.Form.Get("submit.Login")))
            {
                authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                authentication.Challenge(DefaultAuthenticationTypes.ApplicationCookie);
                return new HttpUnauthorizedResult();
            }

            return View(model);
        }
    }
}
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

        public ActionResult Authorize()
        {
            BaseViewModel errModel = new BaseViewModel
            {
                Title = i18n.Resources.Error
            };

            if (Response.StatusCode != 200)
            {  
                return View("AuthorizeError", errModel);
            }

            string clientId = Request.QueryString.Get("client_id");
            string clientSecret = Request.QueryString.Get("secret");

            Application client = UoW.ApplicationRepository.GetByClientAndSecret(clientId, clientSecret);

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

            if (Request.HttpMethod == "POST")
            {
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
            }

            BaseViewModel model = new BaseViewModel
            {
                Title = i18n.Resources.Development
            };
            return View(model);
        }
    }
}
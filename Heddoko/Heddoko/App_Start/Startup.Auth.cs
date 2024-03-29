﻿/**
 * @file Startup.Auth.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using DAL.Models;
using Heddoko.Helpers.Auth;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Services;
using Services.MailSending;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace Heddoko
{
    public partial class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ProxyEmailService.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                        getUserIdCallback: (id) => (id.GetUserId<int>())),
                    OnApplyRedirect = ctx =>
                    {
                        if (!IsApiOrSignalRRequest(ctx.Request))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                }
            });

            string PublicClientId = "self";

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                Provider = new SimpleAuthorizationServerProvider(PublicClientId)
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            OAuthAuthorizationServerOptions OpenAPIOAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AuthorizeEndpointPath = new PathString("/authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                ApplicationCanDisplayErrors = true,
                AllowInsecureHttp = true,
                Provider = new ApplicationOAuthProvider()
            };

            app.UseOAuthAuthorizationServer(OpenAPIOAuthServerOptions);
        }
            
        private static bool IsApiOrSignalRRequest(IOwinRequest request)
        {
            string apiPath = VirtualPathUtility.ToAbsolute("~/api/");
            string signalrPath = VirtualPathUtility.ToAbsolute("~/signalr/");
            return request.Uri.LocalPath.StartsWith(apiPath) || request.Uri.LocalPath.StartsWith(signalrPath);
        }
    }
}
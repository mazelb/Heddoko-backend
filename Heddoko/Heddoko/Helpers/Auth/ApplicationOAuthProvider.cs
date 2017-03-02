/**
 * @file ApplicationOAuthProvider.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using DAL;
using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using i18n;
using System.Security.Principal;

namespace Heddoko.Helpers.Auth
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private Application client;

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var UoW = context.OwinContext.Get<UnitOfWork>();

            client = UoW.ApplicationRepository.GetByClient(context.ClientId);

            if (client == null)
            {
                context.SetError("invalid_grant", "The cliet is incorrect.");
                context.Rejected();
            }
            else if (!client.Enabled)
            {
                context.SetError("invalid_grant", "This client is disabled.");
                context.Rejected();
            }

            if (context.Error == null)
            {
                context.Validated(client.RedirectUrl);
            }

            return Task.FromResult(0);
        }
    }
}
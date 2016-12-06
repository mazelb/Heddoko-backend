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

namespace Heddoko.Helpers.Auth
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private Development client;

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            context.TryGetFormCredentials(out clientId, out clientSecret);

            var UoW = context.OwinContext.Get<UnitOfWork>();
            client = UoW.DevelopmentRepository.GetByClient(clientId, clientSecret);

            if (client == null)
            {
                context.SetError("invalid_grant", "The cliet or secret is incorrect.");
            }
            else if (!client.Enabled)
            {
                context.SetError("invalid_grant", "This client is disabled.");
            }

            if (!context.HasError)
            {
                context.Validated(clientId);
            }

            return base.ValidateClientAuthentication(context);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {          
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.ClientId));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }
    }
}
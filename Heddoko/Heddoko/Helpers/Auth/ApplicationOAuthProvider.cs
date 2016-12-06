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

namespace Heddoko.Helpers.Auth
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private DevelopmentRepository clientService;

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string client;
            string secret;
            context.TryGetFormCredentials(out client, out secret);

            if (client == "1234" && secret == "12345")
            {
                context.Validated(client);
            }

            return base.ValidateClientAuthentication(context);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var client = clientService.GetByClient(context.ClientId);
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, client.Name));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }
    }
}
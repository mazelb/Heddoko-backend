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
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public string PublicClientId { get; set; }
        public SimpleAuthorizationServerProvider(string publicClientId)
        {
            PublicClientId = publicClientId;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            string allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            ApplicationUserManager userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            User user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError("invalid_grant", "User did not confirm email.");
                return;
            }

            ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            foreach (UserRole role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }
            context.Validated(identity);
        }
    }
}
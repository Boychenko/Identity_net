using System;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;

namespace Sample
{
    public class ClaimsAuthentication : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return base.Authenticate(resourceName, incomingPrincipal);
            }

            ClaimsPrincipal claimsPrincipal = CreateAppPrincipal(incomingPrincipal);
            EstablishSession(claimsPrincipal);

            return claimsPrincipal;
        }

        private ClaimsPrincipal CreateAppPrincipal(ClaimsPrincipal incomingPrincipal)
        {
            var principal = new ClaimsPrincipal(incomingPrincipal);
            if (principal.Identities.First().Name == "alex")
            {
                principal.Identities.First().AddClaim(new Claim("canDoEverything", "true", ClaimValueTypes.Boolean));
            }
            return principal;
        }

        private void EstablishSession(ClaimsPrincipal principal)
        {
            var token = new SessionSecurityToken(principal, TimeSpan.FromHours(8));
            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(token);
        }
    }
}
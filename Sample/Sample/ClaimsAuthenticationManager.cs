using System;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace Sample
{
    public class ClaimsAuthenticationManager : System.Security.Claims.ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return base.Authenticate(resourceName, incomingPrincipal);
            }

            EstablishSession(incomingPrincipal);

            return incomingPrincipal;
        }

        private void EstablishSession(ClaimsPrincipal principal)
        {
            var token = new SessionSecurityToken(principal, TimeSpan.FromHours(8));
            FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(token);
        }
    }
}
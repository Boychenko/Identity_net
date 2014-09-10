using System.Security.Claims;

namespace Sample
{
    public class ClaimsAuthorization : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var claim = context.Principal.FindFirst("canDoEverything");
            return claim != null && claim.Value == "true";

        }
    }
}
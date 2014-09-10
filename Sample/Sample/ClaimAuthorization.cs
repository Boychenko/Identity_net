using System.Security.Claims;

namespace Sample
{
    public class ClaimAuthorization : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            return base.CheckAccess(context);
        }
    }
}
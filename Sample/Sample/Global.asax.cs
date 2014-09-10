using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

/*        protected void Application_PostAuthenticateRequest()
        {
            var principal = ClaimsPrincipal.Current;
            var authenticateManager = new ClaimsAuthentication();
            var newPrincipal = authenticateManager.Authenticate(String.Empty, principal);

            Thread.CurrentPrincipal = newPrincipal;
            HttpContext.Current.User = newPrincipal;
        }*/
    }
}

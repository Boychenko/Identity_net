using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Claims()
        {
            var b = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthorizationManager.CheckAccess
                (new System.Security.Claims.AuthorizationContext(ClaimsPrincipal.Current, "Claims", "View"));
            ViewBag.Message = "Claims";

            return View(ClaimsPrincipal.Current);
        }
    }
}
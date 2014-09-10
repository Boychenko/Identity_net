using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

using Thinktecture.IdentityModel;
using Thinktecture.IdentityModel.SystemWeb.Mvc;

using AuthorizationContext = System.Security.Claims.AuthorizationContext;

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
            ViewBag.Message = "Claims";

            return View(ClaimsPrincipal.Current);
        }

/*        [ClaimsPrincipalPermission(SecurityAction.Demand, 
            Operation = "Print", 
            Resource = "Document")]
        [PrincipalPermission(SecurityAction.Demand, Role = "PrinterStaff")]
        [ResourceActionAuthorize("Print", "Document")]
        [Authorize(Roles = "Admin,PrinterStaff")]
        public ActionResult PrintDocument(string documentText)
        {
            string result = "Printed";
            var authorizationManager = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthorizationManager;
            result =
                authorizationManager.CheckAccess(new AuthorizationContext(ClaimsPrincipal.Current, "Document", "Print"))
                    ? "Printed"
                    : "Forbidden";
            result = Thinktecture.IdentityModel.ClaimsAuthorization.CheckAccess("Document", "Print") ? "Printed"
                    : "Forbidden"; ;
            return Content(result);
        }*/
    }
}
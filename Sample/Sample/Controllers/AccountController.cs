using System.IdentityModel.Services;
using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sample.Models;

namespace Sample.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            using (var store = new UserStore<IdentityUser>(new IdentityDbContext()))
            {
                using(var manager = new UserManager<IdentityUser>(store))
                {
                    var claimsUser = manager.Find(model.UserName, model.Password);
                    if (claimsUser != null)
                    {
                        Authenticate(manager, claimsUser);
                        return RedirectToLocal(returnUrl);
                    }
                };
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        private static void Authenticate(UserManager<IdentityUser> manager, IdentityUser claimsUser)
        {
            var authenticateManager =
                FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager;
            var principal = new ClaimsPrincipal(manager.CreateIdentity(claimsUser, "Forms"));
            authenticateManager.Authenticate(string.Empty, principal);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                using (var store = new UserStore<IdentityUser>(new IdentityDbContext()))
                {
                    using (var manager = new UserManager<IdentityUser>(store))
                    {
                        var user = new IdentityUser { UserName = model.UserName};
                        var result = manager.Create(user, model.Password);
                        if (result.Succeeded)
                        {
                            Authenticate(manager, user);

                            return RedirectToAction("Index", "Home");
                        }
                        AddErrors(result);
                    };
                }
                /*// Attempt to register the user
                try
                {
                 * 
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    System.Web.Mvc.ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }*/
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using DBModel;
using SchoolManagmentSystem.csScripts;

namespace SchoolManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        Context context = new Context();
        SessionContext sessionContext = new SessionContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            var authenticatedUser = context.Users.Where(u => u.UserName == model.UserName & u.Password == model.Password).FirstOrDefault();
            if (authenticatedUser != null)
            {
                sessionContext.SetAuthenticationToken(authenticatedUser.Id.ToString(), false, authenticatedUser);
                return RedirectToAction("Profile", "UserProfile", "Account");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login", "Home");
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
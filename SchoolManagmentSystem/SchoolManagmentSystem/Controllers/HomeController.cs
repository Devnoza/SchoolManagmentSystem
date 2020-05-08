using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using DBModel;

namespace SchoolManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
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
            using(Context context = new Context())
            {
                User authenticatedUser = context.Users.Where(u => u.UserName == model.UserName && u.Password == model.Password).FirstOrDefault();
                if (authenticatedUser != null)
                {
                    return RedirectToAction("UserProfile", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View(model);
                }
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
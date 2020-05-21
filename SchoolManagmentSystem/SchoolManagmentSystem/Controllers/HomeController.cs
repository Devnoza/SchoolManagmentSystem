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
        public ActionResult Login(User user)
        {
            using (Context context = new Context())
            {
                User authenticatedUser = context.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
                if (authenticatedUser != null)
                {
                    Session["Username"] = authenticatedUser.UserName;
                    Session["UserId"] = authenticatedUser.Id;

                    return RedirectToAction("UserProfile", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View(user);
                }
            }
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
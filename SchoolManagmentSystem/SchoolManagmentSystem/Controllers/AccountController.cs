using DBModel;
using SchoolManagmentSystem.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SchoolManagmentSystem.Controllers
{
    [CustomAuthenticationFilter]
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize("Admin")]
        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["UserId"] = null;
            return RedirectToAction("Login", "Login", "Home");
        }

        [CustomAuthorize("Admin")]
        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult UnAuthorizedView()
        {
            return View();
        }
    }
}
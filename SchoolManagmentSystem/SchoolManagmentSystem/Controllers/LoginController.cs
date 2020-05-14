using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBModel;

namespace SchoolManagmentSystem.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult LoginView()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginView(User incominguser)
        {
            using (Context context = new Context())
            {
                User user = context.Users
                .Where(u => u.UserName == incominguser.UserName && u.Password == incominguser.Password)
                .FirstOrDefault();

                if (user != null)
                {
                    Session["Username"] = user.UserName;
                    Session["UserId"] = user.Id;
                    
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View(incominguser);
                }
            }
        }

        public ActionResult UnAuthorizedView()
        {
            ViewBag.Message = "UnAuthorized";

            return View();
        }





    }
}
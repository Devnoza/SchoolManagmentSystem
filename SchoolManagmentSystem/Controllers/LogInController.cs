using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagmentSystem.Controllers
{
    public class LogInController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new Context())
                {
                    Users_Table user = context.Users_Table
                                       .Where(u => u.Username == model.UserName && u.Password == model.Password)
                                       .FirstOrDefault();

                    if (user != null)
                    {
                        Session["UserName"] = user.Username;
                        Session["UserId"] = user.Id;
                        return RedirectToAction("GetList", "Registration");
                    }
                    else
                    {
                        ModelState.AddModelError("", "არასწორი მომხმარებელი ან პაროლი");
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            return RedirectToAction("GetList", "Registration");
        }
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "Un Authorized Page!";

            return View();
        }
    }
}
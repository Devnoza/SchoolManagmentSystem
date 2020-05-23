using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBModel;
using SchoolManagmentSystem.CustomAuth;
using SchoolManagmentSystem.Helper;

namespace SchoolManagmentSystem.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                Context context = new Context();
                User login = context.Users.Where(x => (x.UserName == user.UserName) && (x.Password == user.Password)).FirstOrDefault();
                Session.Add("Username", login.UserName);
                Session.Add("userID", login.Id);
                Session.Add("RoleId", login.RoleId);
                return RedirectToAction("Home", "Site");
            }
            else
            {
                ModelState.AddModelError("", "მონაცემები არასწორია.");
                return View(user);
            }
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["userId"] = null;
            Session["RoleId"] = null;
            return RedirectToAction("Home", "Site");
        }
        [HttpGet]
        [CustomAttributes("Admin")]
        public ActionResult Register()
        {
            RegistrationObject user = new RegistrationObject();
            return View(user);
        }
        [HttpPost]
        [CustomAttributes("Admin")]
        public ActionResult Register(RegistrationObject user)
        {
            if (ModelState.IsValid)
            {
                Context context = new Context();
                Person p = new Person { FirstName = user.FirstName, LastName = user.LastName, Gender = user.Gender, BirthDate = user.BirthDate };
                User u = new User { UserName = user.UserName, Password = user.Password, Email = user.Email, Person = p, Role = user.Role, RoleId = user.RoleId, PersonId = user.PersonId };
                context.Users.Add(u);
                context.People.Add(p);
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "არასწორადაა შევსებული მონაცემები.");
                return View(user);
            }
        }
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}
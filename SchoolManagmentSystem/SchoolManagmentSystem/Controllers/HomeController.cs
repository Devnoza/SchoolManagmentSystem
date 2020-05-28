using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
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

        [HttpGet]
        public ActionResult Register()
        {
            CsScripts.RegistrationModel registrationModel = new CsScripts.RegistrationModel();
            
            return View(registrationModel);
        }

        [HttpPost]
        public ActionResult Register(CsScripts.RegistrationModel registrationModel)
        {
            User user = registrationModel.user;
            Person person = registrationModel.person;

            if(user != null && person != null)
            {
                try
                {
                    using (Context context = new Context())
                    {
                        person.Users.Add(user);
                        person = context.People.Add(person);

                        user.PersonId = person.Id;
                        user.RoleId = 2;
                        context.Users.Add(user);

                        context.SaveChanges();

                        Login(user);
                    }
                }
                
                catch (DbEntityValidationException e)
                {
                    string error = "";
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        error = String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        
                        {
                            error +=String.Format("\n- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw new Exception(error);
                }
            }

            return View();
        }

    }
}
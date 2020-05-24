using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBModel;
using SchoolManagmentSystem;
using SchoolManagmentSystem.Filters;

namespace SchoolManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        Context context = new Context();

        public ActionResult Authentication()
        {
            User u = new User();
            return View(u);
        }

        [HttpPost]
        public ActionResult Authentication(User u)
        {

            if (context.Users.Count() != 0)
            {

                User momxmarebeli = context.Users.FirstOrDefault(x => x.UserName == u.UserName && x.Password == u.Password);

                if (momxmarebeli != null)
                {
                    Session["UserId"] = momxmarebeli.Id;
                    Session["UserName"] = momxmarebeli.UserName;
                    Session["UserRoleId"] = momxmarebeli.RoleId;
                    Session["viewid"] = momxmarebeli.PersonId;
                    return RedirectToAction("Profile", "Home");
                }
                else if (momxmarebeli == null)
                {
                    ViewBag.Message = "არასწორი მომხმარებელი ან პაროლი";
                    return View();
                }
                else
                {
                    ViewBag.Message = "მოხდა შეცდმა";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "არ არსებობს არცერთი ანგარიში, გაიარეთ რეგისტრაცია";
                return View();
            }

        }



        [HttpGet]
        public ActionResult Registration()
        {
            RegistrationView view = new RegistrationView();
            return View(view);
        }

        [HttpPost]
        public ActionResult Registration(RegistrationView p)
        {

            var argameordes = context.Users.FirstOrDefault(x => x.UserName == p.Username);
            if (argameordes == null)
            {


                if (p.Email.ToLower().EndsWith("@mail.ru") || p.Email.ToLower().EndsWith("@gmail.com"))
                {
                    Person persondsm = new Person() { FirstName = p.FirstName, LastName = p.LastName, Gender = p.Gender };
                    context.People.Add(persondsm);
                    context.SaveChanges();
                    int intIdt = context.People.Max(u => u.Id);
                    User userdsm = new User() { Email = p.Email, UserName = p.Username, Password = p.Password, PersonId = intIdt, RoleId = p.Role };
                    context.Users.Add(userdsm);
                    context.SaveChanges();
                    User userdsm1 = context.Users.FirstOrDefault(x => x.UserName == userdsm.UserName);
                    int introle = userdsm1.RoleId;
                    if (introle == 1)
                    {
                        Student studentdsm = new Student() { PersonId = intIdt, TypeId = 1 };
                        context.Students.Add(studentdsm);
                        context.SaveChanges();
                    }
                    else if (introle == 2)
                    {
                        Teacher teacherdsm = new Teacher() { PersonId = intIdt, TypeId = 1 };
                        context.Teachers.Add(teacherdsm);
                        context.SaveChanges();
                    }

                    return RedirectToAction("Authentication", "Account");
                }
                else
                {
                    ViewBag.Message = string.Empty;
                    ViewBag.Message = "არასწორი იმეილი";
                    return View(p);
                }
            }
            else
            {
                ViewBag.Message = "ასეთი მომხმარებელი უკვე არსებობს";
                return View(p);
            }

        }
     

        public ActionResult UnAuthorized()
        {
            return View();
        }
        public ActionResult UnPermissioned()
        {
            return View();
        }


    }
}
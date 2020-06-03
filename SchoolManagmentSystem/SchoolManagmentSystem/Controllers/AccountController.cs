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
                    Role role = context.Roles.FirstOrDefault(x => x.Id == momxmarebeli.RoleId);
                    Session["UserId"] = momxmarebeli.Id;
                    Session["UserName"] = momxmarebeli.UserName;
                    Session["UserRoleId"] = momxmarebeli.RoleId;
                    Session["viewid"] = momxmarebeli.PersonId;
                    Session["ViewRole"] = role.Name;
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
                    Role addrole = context.Roles.FirstOrDefault(x => x.Id == userdsm1.RoleId);
                    if (addrole.Name == "Student")
                    {
                        StudentType type = context.StudentTypes.FirstOrDefault(x => x.Name == "Student");
                        Student studentdsm = new Student() { PersonId = intIdt, TypeId = type.Id };
                        context.Students.Add(studentdsm);
                        Permission upleba = context.Permissions.FirstOrDefault(x => x.Name == "ViewProfile");
                        userdsm1.Role.Permissions.Add(upleba);
                        context.SaveChanges();
                    }
                    else if (addrole.Name == "Teacher")
                    {
                        TeacherType type = context.TeacherTypes.FirstOrDefault(x => x.Name == "Teacher");
                        Teacher teacherdsm = new Teacher() { PersonId = intIdt, TypeId = type.Id };
                        context.Teachers.Add(teacherdsm);
                        Permission upleba = context.Permissions.FirstOrDefault(x => x.Name == "ViewProfile");
                        Permission upleba1 = context.Permissions.FirstOrDefault(x => x.Name == "AddSubjectOrStudent");
                        userdsm1.Role.Permissions.Add(upleba);
                        userdsm1.Role.Permissions.Add(upleba1);
                        context.SaveChanges();
                    }
                    else if (addrole.Name == "Admin")
                    {
                        Permission upleba = context.Permissions.FirstOrDefault(x => x.Name == "ViewProfile");
                        Permission upleba1 = context.Permissions.FirstOrDefault(x => x.Name == "AddSubjectOrStudent");
                        userdsm1.Role.Permissions.Add(upleba);
                        userdsm1.Role.Permissions.Add(upleba1);
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
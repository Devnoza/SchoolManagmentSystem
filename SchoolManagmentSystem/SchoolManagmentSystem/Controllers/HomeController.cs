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
    public class HomeController : Controller
    {        
        Context context = new Context();
        public ActionResult Index()
        {
            if (context.Users.Count() == 0)
            {
                ViewBag.Message = "არ არსებობს არცერთი ანგარიში, გაიარეთ რეგისტრაცია";               
            }
            else
            {
                ViewBag.Message = context.Users.Count();
            }
            return View();
        }

        [HttpGet]
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
                    else if(introle == 2)
                    {
                        Teacher teacherdsm = new Teacher() { PersonId = intIdt, TypeId = 1 };
                        context.Teachers.Add(teacherdsm);
                        context.SaveChanges();
                    }
                
                    Session["viewid"] = intIdt;
                    return RedirectToAction("Profile", "Home");
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
        public ActionResult Profile()
        {

                int viewid = int.Parse(Session["viewid"].ToString());
                User viewuser = context.Users.FirstOrDefault(x => x.PersonId == viewid);
                Person viewperson = context.People.FirstOrDefault(x => x.Id == viewid);
                ViewBag.Firstname = viewperson.FirstName;
                ViewBag.Lastname = viewperson.LastName;
                if (viewperson.Gender == false)
                {
                    ViewBag.Gender = "მამრობითი";
                }
                else
                {
                    ViewBag.Gender = "მდედრობითი";
                }
                ViewBag.Username = viewuser.UserName;
                ViewBag.Email = viewuser.Email;
                Teacher teacher = context.Teachers.FirstOrDefault(x => x.PersonId == viewid);
                Student student = context.Students.FirstOrDefault(x => x.PersonId == viewid);
                if (teacher != null)
                {
                    if (teacher.TypeId == 1)
                    {
                        TeacherType view = context.TeacherTypes.FirstOrDefault(x => x.Id == 1);
                        ViewBag.Role = view.Name;
                    }
                    else
                    {
                        TeacherType view = context.TeacherTypes.FirstOrDefault(x => x.Id == 2);
                        ViewBag.Role = view.Name;
                    }
                }
                else if (student != null)
                {
                    if (student.TypeId == 1)
                    {
                        StudentType view = context.StudentTypes.FirstOrDefault(x => x.Id == 1);
                        ViewBag.Role = view.Name;
                    }
                }
                return View();

        }


        public ActionResult UnAuthorized()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditProfile() 
        {
            int id = int.Parse(Session["viewid"].ToString());
            Person view = context.People.FirstOrDefault(x => x.Id == id);
            User viewuser = context.Users.FirstOrDefault(x => x.PersonId == id);
            RegistrationView viewreg = new RegistrationView()
            { FirstName = view.FirstName, LastName = view.LastName, Gender = view.Gender, Username = viewuser.UserName,
                Email = viewuser.Email, Password = viewuser.Password, Role = viewuser.RoleId };
            return View(viewreg);
        }
        [HttpPost]
        public ActionResult EditProfile(RegistrationView redact)
        {
            int id = int.Parse(Session["viewid"].ToString());
            Person predact = context.People.FirstOrDefault(x => x.Id == id);
            predact.FirstName = redact.FirstName;
            predact.LastName = redact.LastName;
            predact.Gender = redact.Gender;
            User uredact = context.Users.FirstOrDefault(x => x.PersonId == id);
            uredact.UserName = redact.Username;
            uredact.Email = redact.Email;
            uredact.Password = redact.Password;
            context.SaveChanges();
            return RedirectToAction("Profile", "Home");
        }
       
        public ActionResult DeleteProfile()
        {
            int id = int.Parse(Session["viewid"].ToString());
            Person pdelet = context.People.FirstOrDefault(x => x.Id == id);
            User udelet = context.Users.FirstOrDefault(x => x.PersonId == id);
            Teacher tdelet = context.Teachers.FirstOrDefault(x => x.PersonId == id);
            Student sdelet = context.Students.FirstOrDefault(x => x.PersonId == id);
            context.People.Remove(pdelet);
            context.Users.Remove(udelet);
            if (sdelet != null)
            {
            context.Students.Remove(sdelet);
            }
            else
            {
            context.Teachers.Remove(tdelet);
            }            
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
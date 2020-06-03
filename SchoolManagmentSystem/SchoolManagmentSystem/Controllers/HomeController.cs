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
    [CustomAuthenticationFilter]
    public class HomeController : Controller
    {        
        Context context = new Context();
        [CustomAuthorize("ViewProfile")]
        public ActionResult Index()
        {
            if (context.Users.Count() == 0)
            {
                ViewBag.Message = "არ არსებობს არცერთი ანგარიში, გაიარეთ რეგისტრაცია";               
            }
            return View();
        }
        [CustomAuthorize("ViewProfile")]
        public ActionResult Profile()
        {
                
                int viewid = int.Parse(Session["viewid"].ToString());
                User viewuser = context.Users.FirstOrDefault(x => x.PersonId == viewid);
                Person viewperson = context.People.FirstOrDefault(x => x.Id == viewid);
                RegistrationView ProfileView = new RegistrationView();
                ProfileView.FirstName = viewperson.FirstName;
                ProfileView.LastName = viewperson.LastName;
                ProfileView.Username = viewuser.UserName;
                ProfileView.Email = viewuser.Email;
                Teacher teacher = context.Teachers.FirstOrDefault(x => x.PersonId == viewid);
                Student student = context.Students.FirstOrDefault(x => x.PersonId == viewid);
                if (teacher != null)
                {
                    TeacherType wodeba = context.TeacherTypes.FirstOrDefault(x => x.Name == "Teacher");
                    TeacherType wodeba1 = context.TeacherTypes.FirstOrDefault(x => x.Name == "Senior Teacher");
                    if (teacher.TypeId == wodeba.Id)
                    {
                        ProfileView.Rank = wodeba.Name;
                    }
                    else
                    {
                        ProfileView.Rank = wodeba1.Name;
                    }
                }
                else if (student != null)
                {
                    StudentType wodeba = context.StudentTypes.FirstOrDefault(x => x.Name == "Student"); 
                    if (student.TypeId == wodeba.Id)
                    {
                        ProfileView.Rank = wodeba.Name;
                    }
                }
                else
                {
                    Role view = context.Roles.FirstOrDefault(x => x.Id == viewuser.RoleId);
                    ProfileView.Rank = view.Name;
                }

                if (viewperson.Gender == false)
                {
                    ProfileView.GenderStr = "მამრობითი";
                }
                else
                {
                    ProfileView.GenderStr = "მდედრობითი";
                }
                return View(ProfileView);

        }


        public ActionResult UnAuthorized()
        {
            return View();
        }
        [HttpGet]
        [CustomAuthorize("ViewProfile")]
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
        [CustomAuthorize("ViewProfile")]
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
        [CustomAuthorize("ViewProfile")]
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
            else if (tdelet != null)
            {
             context.Teachers.Remove(tdelet);
            }
            else
            {
                
            }
          
            context.SaveChanges();
            return RedirectToAction("Authentication", "Account");
        }
        [CustomAuthorize("ViewProfile")]
        public ActionResult LogOutProfile()
        {
            Session["viewid"] = null;
            Session["UserName"] = null;
            Session["UserId"] = null;
            Session["UserRoleId"] = null;
            Session["ViewRole"] = null;
            return RedirectToAction("Authentication", "Account");
        }

    }
}
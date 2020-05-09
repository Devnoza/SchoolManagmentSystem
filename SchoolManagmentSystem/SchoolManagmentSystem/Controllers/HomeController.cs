using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBModel;


namespace SchoolManagmentSystem.Controllers
{
    public class HomeController : Controller
    {

        Context context = new Context();
        bool b;
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

                User momxmarebeli = context.Users.FirstOrDefault(x => x.Email == u.Email && x.Password == u.Password);

                if (momxmarebeli != null)
                {
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
            Person p = new Person();
            return View(p);
        }

        [HttpPost]
        public ActionResult Registration(Person p)
        {
            Person momxmarebeli = context.People.FirstOrDefault(x => x.FirstName == p.FirstName && x.LastName == p.LastName && x.Gender == p.Gender);
            if (momxmarebeli != null)
            {
                ViewBag.Message = "ესეთი ანგარიში უკვე არსებობს";
                return View(p);
            }
            else
            {
                context.People.Add(p);
                context.SaveChanges();
                Session["activeaprofile"] = p.Id;
                bool b = true;
                return RedirectToAction("Profile", "Home");
            }
        }

        [HttpPost]
        public ActionResult Profile <T>(T personoruser)
        {
            string activeprofile = Session["activeprofile"].ToString();
            return View();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBModel;
using SchoolManagmentSystem.Filters;

namespace SchoolManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            return View();
        }
        [CustomAuthorize("Administrator")]
        public ActionResult UserView()
        {
            Context Co = new Context();

            return View(Co.Users.ToList());
        }
        public ActionResult TeacherView()
        {
            Context Co = new Context();

            return View(Co.Teachers.ToList());
        }
        public ActionResult StudenView()
        {
            Context Co = new Context();

            return View(Co.Students.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult NoRole()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}
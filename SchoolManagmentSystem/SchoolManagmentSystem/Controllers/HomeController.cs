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
        public ActionResult Index()
        {
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
            try
            {

                User momxmareblebi = context.Users.Where(x => x.UserName == u.UserName & x.Password == u.Password).FirstOrDefault();
                if (context.Users.Contains(momxmareblebi))
                {
                    ViewBag.Message = string.Empty;
                    return View();                    
                }
                else
                {
                    ViewBag.Message = "არასწორი მომხმარებელი ან პაროლი";
                    return View();
                }

            }
            catch (Exception)
            {
                ViewBag.Message = "არასწორი მომხმარებელი ან პაროლი";
                return View();
            }

        }
        public ActionResult Registration()
        {
            return View();
        }

    }
}
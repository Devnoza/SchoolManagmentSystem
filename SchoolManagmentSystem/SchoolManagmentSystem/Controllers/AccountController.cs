using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SchoolManagmentSystem.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}
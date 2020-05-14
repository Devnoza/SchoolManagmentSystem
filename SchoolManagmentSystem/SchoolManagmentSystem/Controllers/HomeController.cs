using SchoolManagmentSystem.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagmentSystem.Controllers
{
    [CustomAuthenticationFilter]
    public class HomeController : Controller
    {
        [CustomAuthorize("Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
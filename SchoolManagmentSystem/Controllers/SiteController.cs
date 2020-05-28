using System.Web.Mvc;

namespace SchoolManagmentSystem.Controllers
{
    public class SiteController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}
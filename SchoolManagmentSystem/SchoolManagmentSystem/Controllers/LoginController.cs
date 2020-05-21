using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBModel;
namespace SchoolManagmentSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Autherize(User userModel)
        {

            using (Context db = new Context())
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    ModelState.AddModelError("UserName", "Email not found or matched");
                    return View("Index", userDetails);  //, userModel
                }

                else
                {
                    Session["userID"] = userDetails.Id;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}
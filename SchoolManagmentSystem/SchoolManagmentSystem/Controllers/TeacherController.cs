using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagmentSystem.CustomAuth;
using DBModel;
namespace SchoolManagmentSystem.Controllers
{
    public class TeacherController : Controller
    {
        [CustomAttributes("ViewCourseTeacher")]
        [ValidateAntiForgeryToken]
        public ActionResult Courses()
        {
            Context context = new Context();
            var PersonID = context.Users.Where(x => x.Id == Convert.ToInt32(Session["userID"])).FirstOrDefault().PersonId;
            var subjects = context.Subjects.Where(x => x.Teacher.PersonId == PersonID).ToList();
            return View(subjects);
        }
    }
}
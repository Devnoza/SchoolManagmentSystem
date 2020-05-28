using DBModel;
using SchoolManagmentSystem.CustomAuth;
using System;
using System.Linq;
using System.Web.Mvc;

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
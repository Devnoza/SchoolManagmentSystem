using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using SchoolManagmentSystem.Filters;
using DBModel;


namespace SchoolManagmentSystem.Controllers
{
    public class AddController : Controller
    {
        Context context = new Context();

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        public ActionResult AddSubject()
        {
            return View();
        }

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        [HttpGet]
        public ActionResult AddStudent()
        {
            AddStudentModel model = new AddStudentModel();
            return View(model);
        }

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        [HttpPost]
        public ActionResult AddStudent(AddStudentModel model)
        {
            User u = context.Users.FirstOrDefault(x => x.UserName == model.Name);
            Person p = u.Person;
            Student dasamatebeli = context.Students.FirstOrDefault(x => x.PersonId == p.Id);
            Course c = context.Courses.FirstOrDefault(x => x.Name == model.Course);
            List<string> sagnebi = context.Subjects.Where(x => x.CourseId == c.Id).Select(x => x.Name).ToList();
            foreach (var sagani in sagnebi)
            {
                Subject sub = context.Subjects.FirstOrDefault(x => x.Name == sagani);
                dasamatebeli.Subjects.Add(sub);
                context.SaveChanges();
            }
            return View(model);
        }

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        [HttpGet]
        public ActionResult AddCourse()
        {
            Course model = new Course();
            return View(model);
        }

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        [HttpPost]
        public ActionResult AddCourse(Course model)
        {
                var argameordes = context.Courses.FirstOrDefault(x => x.Name == model.Name);
                if (argameordes == null)
                {
                    ViewBag.Message = "კურსი დაემატა!";
                    context.Courses.Add(model);
                    context.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "ასეთი კურსი უკვე არსებობს!";
                }

            return View();
        }

    }
}
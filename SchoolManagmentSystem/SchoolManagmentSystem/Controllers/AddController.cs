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
    [CustomAuthenticationFilter]
    public class AddController : Controller
    {
        Context context = new Context();

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        [HttpGet]
        public ActionResult AddSubject()
        {
            AddSubjectModel model = new AddSubjectModel();
            if (Session["Course"].ToString() != null)
                model.Course = Session["Course"].ToString();
            return View(model);
        }

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        [HttpPost]
        public ActionResult AddSubject(AddSubjectModel model)
        {

            Course exitcourse = context.Courses.FirstOrDefault(x => x.Name == model.Course);
            if (exitcourse != null)
            {
                try
                {
                    User u = context.Users.FirstOrDefault(x => x.UserName == model.Teacher);
                    Person p = context.People.FirstOrDefault(x => x.Id == u.PersonId);
                    Teacher existsteacher = context.Teachers.FirstOrDefault(x => x.PersonId == p.Id);
                    Subject existssub = context.Subjects.FirstOrDefault(x => x.Name == model.Name && x.CourseId == exitcourse.Id);

                    if (existsteacher != null)
                    {
                        if (exitcourse.Subjects.FirstOrDefault(x => x.Name == model.Name) == null)
                        {
                            if (exitcourse.Subjects.Count() != 0)
                            {
                                Subject sub = exitcourse.Subjects.FirstOrDefault();
                                List<Student> studentstoadd = sub.Students.ToList();
                                Subject toadd = new Subject { CourseId = exitcourse.Id, Name = model.Name, Teacher = existsteacher };
                                context.Subjects.Add(toadd);
                                foreach (var item in studentstoadd)
                                {
                                    item.Subjects.Add(toadd);
                                }
                                context.SaveChanges();
                            }
                            else
                            {
                                Subject toadd = new Subject { CourseId = exitcourse.Id, Name = model.Name, Teacher = existsteacher };
                                context.Subjects.Add(toadd);
                                context.SaveChanges();
                            }
                            return RedirectToAction("CourseSubjects", "Course", new { courseid = exitcourse.Id });
                        }
                        else
                        {
                            ViewBag.Message = "ამ კურსში ასეთი საგანი უკვე არსებობს";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "ასეთი მასწავლებელი არარსებობს";
                    }
                }
                catch
                {
                    ViewBag.Message = "ასეთი მასწავლებელი არარსებობს";
                }
            }
            else if(exitcourse == null && model.Name != null)
            {
                ViewBag.Message = "ასეთი კურსი არარსებობს2";
            }
            return View(model);
        }


        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        [HttpGet]
        public ActionResult AddStudent()
        {
            AddStudentModel model = new AddStudentModel();
            if (Session["Course"].ToString() != null)
            {
                model.Course = Session["Course"].ToString();
            }
            return View(model);
        }

        [CustomAddSubjectOrStudent("AddSubjectOrStudent")]
        [HttpPost]
        public ActionResult AddStudent(AddStudentModel model)
        {
            User u = context.Users.FirstOrDefault(x => x.UserName == model.Name);
            try
            {
                if (u != null)
                {
                    Person p = u.Person;
                    Student dasamatebeli = context.Students.FirstOrDefault(x => x.PersonId == p.Id);
                    Course c = context.Courses.FirstOrDefault(x => x.Name == model.Course);
                    var ukvearis = context.Subjects.FirstOrDefault(x => x.CourseId == c.Id);
                    if (c != null)
                    {
                        if (c.Subjects.Count() != 0)
                        {
                            if (!dasamatebeli.Subjects.Contains(ukvearis))
                            {
                                List<string> sagnebi = context.Subjects.Where(x => x.CourseId == c.Id).Select(x => x.Name).ToList();
                                foreach (var sagani in sagnebi)
                                {
                                    Subject sub = context.Subjects.FirstOrDefault(x => x.Name == sagani);
                                    dasamatebeli.Subjects.Add(sub);
                                    context.SaveChanges();
                                }
                                return RedirectToAction("CourseMembers", "Course", new { courseid = c.Id });
                            }
                            else
                            {
                                ViewBag.Message = "სტუდენტი უკვე დამატებულია კურსში";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "კურსში არ არსებობს საგნები, ამიტომ ვერ დაამატებთ სტუდენტს";
                        }
                    }
                    else if (model.Name != null || model.Course != null)
                    {
                        ViewBag.Message = "ასეთი კურსი არარსებობს";
                    }
                }
                else if (model.Name != null || model.Course != null)
                {
                    ViewBag.Message = "ასეთი სტუდენტი არარსებობს";
                }
            }
            catch
            {
                ViewBag.Message = "ასეთი სტუდენტი ან კურსი არარსებობს";
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
                if (argameordes == null && model.Name != null && model.Name != "")
                {
                    context.Courses.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("Courses", "Course");
                }
                else if(argameordes != null)
                {
                    ViewBag.Message = "ასეთი კურსი უკვე არსებობს!";
                }
                return View();

        }
    }
}
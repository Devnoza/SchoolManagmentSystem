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
    public class CourseController : Controller
    {
        Context context = new Context();
        public ActionResult Courses()
        {
            if (context.Courses.Count() != 0)
            {
                List<Course> courses = context.Courses.ToList();
                return View(courses);
            }
            else
            {
                ViewBag.Message = "არარსებობს არცერთი კურსი";
                return View();
            }

        }

        public ActionResult CourseMembers(int courseid)
        {
            Course course = context.Courses.FirstOrDefault(x => x.Id == courseid);
            Session["Course"] = course.Name;
            Subject coursesubject = course.Subjects.FirstOrDefault();
            try
            {
                if (coursesubject.Students.Count() != 0)
                {
                    List<Student> students = coursesubject.Students.ToList();
                    List<User> users = new List<User>();
                    foreach (var item in students)
                    {
                        User u = context.Users.FirstOrDefault(x => x.PersonId == item.PersonId);
                        users.Add(u);
                    }
                    List<CourseMembersModel> coursemembers = new List<CourseMembersModel>();
                    Role student = context.Roles.FirstOrDefault(x => x.Name == "Student");
                    Role teacher = context.Roles.FirstOrDefault(x => x.Name == "teacher");
                    foreach (var item in users)
                    {
                        string role;
                        if (item.Role.Name == student.Name)
                        {
                            role = student.Name;
                        }
                        else
                        {
                            role = teacher.Name;
                        }
                        coursemembers.Add(new CourseMembersModel { Role = role, Name = item.UserName });
                        return View(coursemembers);
                    }
                }
                else
                {
                    ViewBag.Message = "კურსი ცარიელია";
                }

            }
            catch
            {
                ViewBag.Message = "კურსი ცარიელია";
            }
            return View();

        }

        [HttpGet]
        public ActionResult EditCourse(int courseId)
        {
            Course toedit = context.Courses.Where(x => x.Id == courseId).FirstOrDefault();
            Session["courseid"] = toedit.Id;
            return View(toedit);
        }

        [HttpPost]
        public ActionResult EditCourse(Course toedt)
        {
            try
            {
                Course unical = context.Courses.Where(x => x.Name == toedt.Name).FirstOrDefault();
                string newname = toedt.Name;
                int courseid = int.Parse(Session["courseid"].ToString());
                if (unical == null || unical.Name == newname)
                {
                    Course toredact = context.Courses.Where(x => x.Id == courseid).FirstOrDefault();
                    toredact.Name = newname;
                    context.SaveChanges();
                    Session["courseid"] = null;
                    return RedirectToAction("Courses", "Course");
                }
                else
                {
                    ViewBag.Message = "ასეთი კურსი უკვე არსებობს";
                }
            }
            catch
            {

            }
            return View();
        }

        public ActionResult DeleteCourse(int courseId)
        {
            Course todelete = context.Courses.Where(x => x.Id == courseId).FirstOrDefault();
            var todeletesubs = context.Subjects.Where(x => x.CourseId == courseId);
            foreach (var item in todeletesubs)
            {
                item.Students.Clear();
                context.Subjects.Remove(item);
            }
            context.Courses.Remove(todelete);
            context.SaveChanges();
            return RedirectToAction("Courses", "Course");
        }

        public ActionResult CourseSubjects(int courseId)
        {
            Course course = context.Courses.FirstOrDefault(x => x.Id == courseId);
            Session["Course"] = course.Name;
            if (course.Subjects.Count() != 0)
            {
                List<CourseSubjectModel> courssubjects = new List<CourseSubjectModel>();
                foreach (var item in course.Subjects)
                {
                    Teacher teacher = context.Teachers.FirstOrDefault(x => x.Id == item.TeacherId);
                    User user = context.Users.FirstOrDefault(x => x.PersonId == teacher.PersonId );
                    courssubjects.Add(new CourseSubjectModel { Teacher = user.UserName, Name = item.Name });
                }
                return View(courssubjects);
            }
            else
            {
                ViewBag.Message = "კურსის საგნები არარსებობს";
                return View();
            }
        }

        public ActionResult RemoveCourseMember(string username)
        {
            User forremove = context.Users.FirstOrDefault(x => x.UserName == username);
            Student forremovestudent = context.Students.FirstOrDefault(x => x.PersonId == forremove.PersonId);
            Subject subject = forremovestudent.Subjects.FirstOrDefault();
            int courseid = subject.CourseId;
            List<Subject> subjects = context.Subjects.Where(x => x.CourseId == courseid).ToList();
            foreach (var item in subjects)
            {
                forremovestudent.Subjects.Remove(item);
            }
            context.SaveChanges();
            return RedirectToAction("CourseMembers", "Course", new { courseid = courseid });
        }

        public ActionResult RemoveCourseSubject(string SubjectName)
        {
            Subject subject = context.Subjects.FirstOrDefault(x => x.Name == SubjectName);
            Course course = context.Courses.FirstOrDefault(x => x.Id == subject.CourseId);
            List<Student> students = subject.Students.ToList();
            course.Subjects.Remove(subject);
            context.Subjects.Remove(subject);
            foreach (var item in students)
            {
                item.Subjects.Remove(subject);
            }
            context.SaveChanges();
            return RedirectToAction("CourseSubjects", "Course", new { courseid = course.Id });
        }
    }
}
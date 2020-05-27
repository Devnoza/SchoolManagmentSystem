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
    public class ManagmentController : Controller
    {
        Context context = new Context();

        [HttpGet]
        public ActionResult Manage()
        {
            Role adminid = context.Roles.FirstOrDefault(x => x.Name == "Admin");
            if (Session["UserRoleId"] != null)
            {
                if (int.Parse(Session["UserRoleId"].ToString()) == adminid.Id)
                {
                    List<bool> Change = new List<bool>();

                    return View(Change);
                }
                else
                {
                    return RedirectToAction("UnPermissioned", "Account");
                }
            }
            else
            {
                return RedirectToAction("UnAuthorized", "Account");
            }
        }
        [HttpPost]
        public ActionResult Manage(List<bool> Change)
        {
            Role studentId = context.Roles.FirstOrDefault(x => x.Name == "Student");
            Role teacherId = context.Roles.FirstOrDefault(x => x.Name == "Teacher");
            Permission viewprofile = context.Permissions.FirstOrDefault(x => x.Name == "ViewProfile");
            if (Change[0] == true)
            {
                Role student = context.Roles.FirstOrDefault(x => x.Id == studentId.Id);
                var permission = context.Permissions.Where(x => x.Id == viewprofile.Id).FirstOrDefault();
                student.Permissions.Add(permission);
            }
            else
            {
                Role role = context.Roles.Where(x => x.Id == studentId.Id).FirstOrDefault();
                var permission = role.Permissions.Where(x => x.Id == viewprofile.Id).FirstOrDefault();
                if (permission != null)
                {
                    role.Permissions.Remove(permission);                   
                }
            }
            context.SaveChanges();

            if (Change[1] == true)
            {
                Role student = context.Roles.FirstOrDefault(x => x.Id == teacherId.Id);
                var permission = context.Permissions.Where(x => x.Id == viewprofile.Id).FirstOrDefault();
                student.Permissions.Add(permission);
            }
            else
            {
                Role role = context.Roles.Where(x => x.Id == teacherId.Id).FirstOrDefault();
                var permission = role.Permissions.Where(x => x.Id == viewprofile.Id).FirstOrDefault();
                if (permission != null)
                {
                    role.Permissions.Remove(permission);
                }
            }
            context.SaveChanges();
            return View();
        }

        [HttpPost]
        public ActionResult AddSubjectOrStudent(List<bool> Change)
        {
            Role teacherId = context.Roles.FirstOrDefault(x => x.Name == "Teacher");
            Permission viewprofile = context.Permissions.FirstOrDefault(x => x.Name == "AddSubjectOrStudent");
            if (Change[0] == true)
            {
                Role student = context.Roles.FirstOrDefault(x => x.Id == teacherId.Id);
                var permission = context.Permissions.Where(x => x.Id == viewprofile.Id).FirstOrDefault();
                student.Permissions.Add(permission);
            }
            else
            {
                Role role = context.Roles.Where(x => x.Id == teacherId.Id).FirstOrDefault();
                var permission = role.Permissions.Where(x => x.Id == viewprofile.Id).FirstOrDefault();
                if (permission != null)
                {
                    role.Permissions.Remove(permission);
                }
            }
            context.SaveChanges();
            return RedirectToAction("Manage", "Managment");
        }

    }
}
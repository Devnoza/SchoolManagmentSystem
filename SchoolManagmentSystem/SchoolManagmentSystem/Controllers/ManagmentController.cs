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
                    Role student = context.Roles.FirstOrDefault(x => x.Name == "Student");
                    Role teacher = context.Roles.FirstOrDefault(x => x.Name == "Teacher");
                    Permission viewprofile = context.Permissions.FirstOrDefault(x => x.Name == "ViewProfile");
                    Permission AddSubjectOrStudent = context.Permissions.FirstOrDefault(x => x.Name == "AddSubjectOrStudent");
                    if (student.Permissions.Contains(viewprofile))
                    {
                        Change.Add(true);
                    }
                    else
                    {
                        Change.Add(false);
                    }
                    if (teacher.Permissions.Contains(viewprofile))
                    {
                        Change.Add(true);
                    }
                    else
                    {
                        Change.Add(false);
                    }
                    if (student.Permissions.Contains(AddSubjectOrStudent))
                    {
                        Change.Add(true);
                    }
                    else
                    {
                        Change.Add(false);
                    }
                    if (teacher.Permissions.Contains(AddSubjectOrStudent))
                    {
                        Change.Add(true);
                    }
                    else
                    {
                        Change.Add(false);
                    }
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
            Role adminid = context.Roles.FirstOrDefault(x => x.Name == "Admin");
            if (Session["UserRoleId"] != null)
            {
                if (int.Parse(Session["UserRoleId"].ToString()) == adminid.Id)
                {
                    Role studentId = context.Roles.FirstOrDefault(x => x.Name == "Student");
                    Role teacherId = context.Roles.FirstOrDefault(x => x.Name == "Teacher");
                    Permission viewprofile = context.Permissions.FirstOrDefault(x => x.Name == "ViewProfile");
                    Permission viewprofile1 = context.Permissions.FirstOrDefault(x => x.Name == "AddSubjectOrStudent");
                    //
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
                    //
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
                    //
                    if (Change[2] == true)
                    {
                        Role student = context.Roles.FirstOrDefault(x => x.Id == studentId.Id);
                        var permission = context.Permissions.Where(x => x.Id == viewprofile1.Id).FirstOrDefault();
                        student.Permissions.Add(permission);
                    }
                    else
                    {
                        Role role = context.Roles.Where(x => x.Id == studentId.Id).FirstOrDefault();
                        var permission = role.Permissions.Where(x => x.Id == viewprofile1.Id).FirstOrDefault();
                        if (permission != null)
                        {
                            role.Permissions.Remove(permission);
                        }
                    }
                    //
                    if (Change[3] == true)
                    {
                        Role student = context.Roles.FirstOrDefault(x => x.Id == teacherId.Id);
                        var permission = context.Permissions.Where(x => x.Id == viewprofile1.Id).FirstOrDefault();
                        student.Permissions.Add(permission);
                    }
                    else
                    {
                        Role role = context.Roles.Where(x => x.Id == teacherId.Id).FirstOrDefault();
                        var permission = role.Permissions.Where(x => x.Id == viewprofile1.Id).FirstOrDefault();
                        if (permission != null)
                        {
                            role.Permissions.Remove(permission);
                        }
                    }
                    context.SaveChanges();
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
            return View(Change);
        }

    }
}
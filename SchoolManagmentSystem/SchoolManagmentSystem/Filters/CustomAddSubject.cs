using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using DBModel;
using System.Web.Routing;

namespace SchoolManagmentSystem.Filters
{
    public class CustomAddSubjectOrStudent : AuthorizeAttribute
    {
        private readonly string[] allowedaddsubjectsorstudents;
        public CustomAddSubjectOrStudent(params string[] roles)
        {
            this.allowedaddsubjectsorstudents = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userId = Convert.ToInt32(httpContext.Session["UserId"]);
            if (userId != 0)
            {
                using (var context = new Context())
                {

                    User user = context.Users
                        .Where(u => u.Id == userId).FirstOrDefault();
                    Role UserRole = user.Role;
                    List<string> userpermissions = UserRole.Permissions.Select(o => o.Name).ToList();
                    foreach (var permission in allowedaddsubjectsorstudents)
                    {
                        if (userpermissions.Contains(permission))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Account" },
                    { "action", "UnPermissioned" }
               });
        }

    }
}
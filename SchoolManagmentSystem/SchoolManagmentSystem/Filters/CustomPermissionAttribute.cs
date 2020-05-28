using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolManagmentSystem.Filters
{
    public class CustomPermissionAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedPermissions;
        public CustomPermissionAttribute(params string[] permissions)
        {
            this.allowedPermissions = permissions;
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
                    var Permissions = UserRole.Permissions.Select(o=>o.Name).ToList();

                    foreach (var permission in allowedPermissions)
                    {
                        Permission a = new Permission();
                        if (Permissions.Contains(a.Name)) return true;
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
                { "action", "PermissionViolationView" }
               });
        }
        
    }
}
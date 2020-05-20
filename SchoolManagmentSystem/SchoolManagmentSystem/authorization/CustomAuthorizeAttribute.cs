using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagmentSystem.authorization
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedpermissions;
        public CustomAuthorizeAttribute(params string[] permissions)
        {
            this.allowedpermissions = permissions;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToInt32(httpContext.Session["UserId"]);
            if (userId != 0)
                using (var context = new Context())
                {
                    var userRole = (from u in context.Users_Table
                                    join r in context.Permissions on u.PermissionId equals r.Id
                                    where u.Id == userId
                                    select new
                                    {
                                        r.Name
                                    }).FirstOrDefault();
                    foreach (var role in allowedpermissions)
                    {
                        if (role == userPermission.Name) return true;
                    }
                }


            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Account" },
                    { "action", "UnAuthorized" }
               });
        }
    }
}
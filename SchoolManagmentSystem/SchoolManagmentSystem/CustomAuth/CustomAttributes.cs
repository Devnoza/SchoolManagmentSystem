using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DBModel;
namespace SchoolManagmentSystem.CustomAuth
{
    public class CustomAttributes : AuthorizeAttribute
    {
        private readonly string[] allowedPermissions;
        public CustomAttributes(params string[] permissions)
        {
            allowedPermissions = permissions;
        }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToInt32(httpContext.Session["UserId"]);
            if (userId != 0)
                using (Context context = new Context())
                {
                    var userPermissions = context.Users.Where(o => o.Id == userId).FirstOrDefault().Role.Permissions.Select(o => o.Name).ToList();

                    foreach (var permission in userPermissions)
                    {
                        if (allowedPermisins.Contains(permission)) return true;
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
                    { "action", "Login" }
               });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using DBModel;
using System.Web.Routing;

namespace SchoolManagmentSystem.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
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
                    foreach (var role in allowedroles)
                    {
                        if (role == UserRole.Name)
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
                    { "action", "UnAuthorizedView" }
               });
        }

    }
}
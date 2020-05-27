using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DBModel;

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
                bool authorize = false;
                var userId = Convert.ToInt32(httpContext.Session["UserId"]);
                if (userId != 0)
                    using (var Context = new Context())
                    {
                        var userRole = Context.Users.Where(o => o.Id == userId).FirstOrDefault().Role.Name;
                        foreach (var role in allowedroles)
                        {
                            if (role == userRole) return true;
                        }
                    }


                return authorize;
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                    { "controller", "Home" },
                    { "action", "NoRole" }
                   });
            }
        }
    }
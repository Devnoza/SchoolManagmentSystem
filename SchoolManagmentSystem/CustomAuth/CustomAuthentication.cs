using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace SchoolManagmentSystem.CustomAuth
{
    public class CustomAuthentication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(filterContext.HttpContext.Session["Username"].ToString()))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || user is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Auth"},
                        { "action", "Unauthorized"}
                    }
                    );
            }
        }
    }
}
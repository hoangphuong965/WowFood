using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WowFood.CustomFilters
{
    public class AdminAuthorizationFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var admin = filterContext.RequestContext.HttpContext.Session["CurrentUserIsAdmin"];

            if (admin == null)
            {
                filterContext.Result = new RedirectResult("~/");
            }

            if (admin != null && admin.Equals(false))
            {
                filterContext.Result = new RedirectResult("~/");
            }
        }

    }
}
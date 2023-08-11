using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WowFood.CustomFilters
{
    public class UserAuthorizationFilterAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.RequestContext.HttpContext.Session["CurrentUserName"];

            if (user == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
        }
    }
}
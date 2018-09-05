using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcureEaseAPI.Providers
{
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult(401);

            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(401);

            }
        }

    }
}
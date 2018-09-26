using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcureEaseAPI.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public string GetEmailHeader()
        {
            string email = Request.Headers["Email"];
            return email;
        }
        [Providers.Authorize]
        public string GetAuthenticatedUser()
        {
            var user = System.Web.HttpContext.Current.User.Identity.Name;
            return user;
        }
    }
}
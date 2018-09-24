using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcureEaseAPI.Models;
using Utilities;

namespace ProcureEaseAPI.Controllers
{
    public class CatalogsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: Catalogs
        public ActionResult Index()
        {
            var catalog = db.Catalog.Include(c => c.OrganizationSettings).Include(c => c.RequestForDemo);
            return View(catalog.ToList());
        }

        public Guid? GetTenantIDFromClientURL(string email)
        {         
            List<UserProfile> users = db.UserProfile.Where(x => x.UserEmail == email).ToList();
            if (users != null && users.Count != 0)
            {
                return users[0].TenantID;
            }
            return null;
        }


        public Guid? GetOrganizationID(string email)
        {
            List<UserProfile> users = db.UserProfile.Where(x => x.UserEmail == email).ToList();
            if (users != null && users.Count != 0)
            {
                return users[0].OrganizationID;
            }
            return null;
        }


        [HttpPost]
        public string GetSubDomainFromClientURL(string email)
        {
            List<UserProfile> users = db.UserProfile.Where(x => x.UserEmail == email).ToList();
            if (users != null && users.Count != 0 && users[0].Catalog != null)
            {
                return users[0].Catalog.SubDomain;
            }
            return null;
        }

        public ActionResult CheckForNullTenantID(Guid? tenantId)
        {
            if (tenantId == null)
            {
                return Json(new
                {
                    success = false,
                    message = "TenantId is null",
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        public ActionResult CheckForNullOrganizationID(Guid? OrganizationId)
        {
            if (OrganizationId == null)
            {
                return Json(new
                {
                    success = false,
                    message = "OrganizationId is null",
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
            else
                return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

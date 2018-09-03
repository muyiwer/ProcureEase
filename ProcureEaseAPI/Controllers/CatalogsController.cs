using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcureEaseAPI.Models;

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

        public Guid GetTenantID(string url)
        {
            var getTenantId = db.Catalog.Where(x => x.SubDomain == url).Select(x => x.TenantID).FirstOrDefault();
            
            return getTenantId;
        }

        public Guid GetOrganizationID(string url)
        {
            var getOrganizationID = db.Catalog.Where(x => x.SubDomain == url).Select(x => x.OrganizationSettings.OrganizationID).FirstOrDefault();

            return getOrganizationID;
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

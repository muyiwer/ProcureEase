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

        public Guid? GetTenantID()
        {
            string url = System.Web.HttpContext.Current.Request.Url.Host; // expecting format nitda.procureease.ng
            string[] hostUrlParts = url.Split('.');// extract sub domain from URL
            string subDomain = hostUrlParts[0];
            List<Catalog> records = db.Catalog.Where(x => x.SubDomain == subDomain).ToList();
            if (records == null || records.Count == 0)
            {
                return null;
            }
            else
            {
                return records.Select(x => x.TenantID).First();
            }
        }

        public Guid? GetOrganizationID()
        {

            string url = System.Web.HttpContext.Current.Request.Url.Host; // expecting format nitda.procureease.ng
            string[] hostUrlParts = url.Split('.');// extract sub domain from URL
            string subDomain = hostUrlParts[0];
            List<Catalog> records = db.Catalog.Where(x => x.SubDomain == subDomain).ToList();
            if (records == null || records.Count == 0)
            {
                return null;
            }
            else
            {
                return records.Select(x => x.OrganizationID).First();
            }
        }

        public ActionResult Add(Catalog catalog)
        {
            catalog.TenantID = Guid.NewGuid();
            db.Catalog.Add(catalog);
            db.SaveChanges();
            return Json(catalog, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddOrganization(OrganizationSettings catalog)
        {
            catalog.OrganizationID = Guid.NewGuid();
            db.OrganizationSettings.Add(catalog);
            db.SaveChanges();
            return Json(catalog, JsonRequestBehavior.AllowGet);
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

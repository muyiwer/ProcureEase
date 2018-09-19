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
            try
            {
                string backendUrl = Request.Url.Host;               
            }
            catch(NullReferenceException)
            {
                string backendUrl = "localhost";
                string[] hostbackendUrlParts = backendUrl.Split('.');// extract sub domain from URL
                string backendSubDomain = hostbackendUrlParts[0];
                List<Catalog> records = db.Catalog.Where(x => x.SubDomain == backendSubDomain).ToList();
                if (records == null || records.Count == 0)
                {
                    return null;
                }
                else
                {
                    return records.Select(x => x.TenantID).First();
                }
            }
           
          
            try
            {
                string checkIfClientUrl = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();
                if (checkIfClientUrl != "")
                {
                    string url = Request.UrlReferrer.Host;
                    string[] hostUrlParts = url.Split('.');// extract sub domain from URL
                    string subDomain = hostUrlParts[0];
                    List<Catalog> record = db.Catalog.Where(x => x.SubDomain == subDomain).ToList();
                    if (record == null || record.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return record.Select(x => x.TenantID).First();
                    }
                }
                else
                {
                    string backendUrl = Request.Url.Host;
                    string[] hostbackendUrlParts = backendUrl.Split('.');// extract sub domain from URL
                    string backendSubDomain = hostbackendUrlParts[0];
                    List<Catalog> records = db.Catalog.Where(x => x.SubDomain == backendSubDomain).ToList();
                    if (records == null || records.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return records.Select(x => x.TenantID).First();
                    }
                }
            }
            catch (NullReferenceException)
            {
                string backendUrl = Request.Url.Host;
                string[] hostbackendUrlParts = backendUrl.Split('.');// extract sub domain from URL
                string backendSubDomain = hostbackendUrlParts[0];
                List<Catalog> records = db.Catalog.Where(x => x.SubDomain == backendSubDomain).ToList();

                if (records == null || records.Count == 0)
                {
                    return null;
                }
                else
                {
                    return records.Select(x => x.TenantID).First();
                }
            }
  
        }

        public Guid? GetOrganizationID()
        {
            try
            {
                string backendUrl = Request.Url.Host;
            }
            catch (NullReferenceException)
            {
                string backendUrl = "localhost";
                string[] hostbackendUrlParts = backendUrl.Split('.');// extract sub domain from URL
                string backendSubDomain = hostbackendUrlParts[0];
                List<Catalog> records = db.Catalog.Where(x => x.SubDomain == backendSubDomain).ToList();
                if (records == null || records.Count == 0)
                {
                    return null;
                }
                else
                {
                    return records.Select(x => x.OrganizationID).First();
                }
            }


            try
            {
                string checkIfClientUrl = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();
                if (checkIfClientUrl != "")
                {
                    string url = Request.UrlReferrer.Host;
                    string[] hostUrlParts = url.Split('.');// extract sub domain from URL
                    string subDomain = hostUrlParts[0];
                    List<Catalog> record = db.Catalog.Where(x => x.SubDomain == subDomain).ToList();
                    if (record == null || record.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return record.Select(x => x.OrganizationID).First();
                    }
                }
                else
                {
                    string backendUrl = Request.Url.Host;
                    string[] hostbackendUrlParts = backendUrl.Split('.');// extract sub domain from URL
                    string backendSubDomain = hostbackendUrlParts[0];
                    List<Catalog> records = db.Catalog.Where(x => x.SubDomain == backendSubDomain).ToList();
                    if (records == null || records.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return records.Select(x => x.OrganizationID).First();
                    }
                }
            }
            catch (NullReferenceException)
            {
                string backendUrl = Request.Url.Host;
                string[] hostbackendUrlParts = backendUrl.Split('.');// extract sub domain from URL
                string backendSubDomain = hostbackendUrlParts[0];
                List<Catalog> records = db.Catalog.Where(x => x.SubDomain == backendSubDomain).ToList();

                if (records == null || records.Count == 0)
                {
                    return null;
                }
                else
                {
                    return records.Select(x => x.OrganizationID).First();
                }
            }
        }


        [HttpPost]
        public string GetSubDomain()
        {
            try
            {
                string Baackendurl = Request.Url.Host;
                string[] hostUrlParts = Baackendurl.Split('.');// extract sub domain from URL
                string backendSubDomain = hostUrlParts[0];
            }
            catch (NullReferenceException)
            {
                return "localhost";
            }
           
            try
            {
                string urll = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();
                if (urll != "")
                {
                    string url = Request.UrlReferrer.Host;
                    string[] hostBackendUrlParts = url.Split('.');// extract sub domain from URL
                    string subDomain = hostBackendUrlParts[0];

                    return subDomain;
                }
            }
            catch (NullReferenceException)
            {
                string Baackendurl = Request.Url.Host;
                string[] hostUrlParts = Baackendurl.Split('.');// extract sub domain from URL
                string backendSubDomain = hostUrlParts[0];
                return backendSubDomain;
            }

            return "localhost";
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

        
        public ActionResult AddOrganization(OrganizationSettings organizationSettings)
        {
            Guid? GettenantId = GetTenantID();
            var tenantId = GettenantId.Value;
            organizationSettings.OrganizationID = Guid.NewGuid();
            organizationSettings.TenantID = tenantId;
            db.OrganizationSettings.Add(organizationSettings);
            db.SaveChanges();
            return Json(tenantId, JsonRequestBehavior.AllowGet);
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

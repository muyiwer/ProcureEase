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

        // GET: Catalogs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalog.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // GET: Catalogs/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationID = new SelectList(db.OrganizationSettings, "OrganizationID", "OrganizationNameInFull");
            ViewBag.RequestID = new SelectList(db.RequestForDemo, "RequestID", "OrganizationFullName");
            return View();
        }

        // POST: Catalogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CatalogItemID,RequestID,SubDomain,OrganizationID")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                catalog.TenantID= Guid.NewGuid();
                db.Catalog.Add(catalog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationID = new SelectList(db.OrganizationSettings, "OrganizationID", "OrganizationNameInFull", catalog.OrganizationID);
            ViewBag.RequestID = new SelectList(db.RequestForDemo, "RequestID", "OrganizationFullName", catalog.RequestID);
            return View(catalog);
        }

        // GET: Catalogs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalog.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationID = new SelectList(db.OrganizationSettings, "OrganizationID", "OrganizationNameInFull", catalog.OrganizationID);
            ViewBag.RequestID = new SelectList(db.RequestForDemo, "RequestID", "OrganizationFullName", catalog.RequestID);
            return View(catalog);
        }

        // POST: Catalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatalogItemID,RequestID,SubDomain,OrganizationID")] Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationID = new SelectList(db.OrganizationSettings, "OrganizationID", "OrganizationNameInFull", catalog.OrganizationID);
            ViewBag.RequestID = new SelectList(db.RequestForDemo, "RequestID", "OrganizationFullName", catalog.RequestID);
            return View(catalog);
        }

        // GET: Catalogs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalog catalog = db.Catalog.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        // POST: Catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Catalog catalog = db.Catalog.Find(id);
            db.Catalog.Remove(catalog);
            db.SaveChanges();
            return RedirectToAction("Index");
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

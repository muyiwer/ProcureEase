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
    public class RequestForDemoController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: RequestForDemo
        public ActionResult Index()
        {
            return View(db.RequestForDemo.ToList());
        }

        // GET: RequestForDemo/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestForDemo requestForDemo = db.RequestForDemo.Find(id);
            if (requestForDemo == null)
            {
                return HttpNotFound();
            }
            return View(requestForDemo);
        }

        // GET: RequestForDemo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestForDemo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestID,OrganizationFullName,OrganizationShortName,AdministratorEmail,AdministratorFirstName,AdministratorLastName,AdministratorPhoneNumber,DateCreated")] RequestForDemo requestForDemo)
        {
            if (ModelState.IsValid)
            {
                requestForDemo.RequestID = Guid.NewGuid();
                db.RequestForDemo.Add(requestForDemo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requestForDemo);
        }

        // GET: RequestForDemo/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestForDemo requestForDemo = db.RequestForDemo.Find(id);
            if (requestForDemo == null)
            {
                return HttpNotFound();
            }
            return View(requestForDemo);
        }

        // POST: RequestForDemo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestID,OrganizationFullName,OrganizationShortName,AdministratorEmail,AdministratorFirstName,AdministratorLastName,AdministratorPhoneNumber,DateCreated")] RequestForDemo requestForDemo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestForDemo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestForDemo);
        }

        // GET: RequestForDemo/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestForDemo requestForDemo = db.RequestForDemo.Find(id);
            if (requestForDemo == null)
            {
                return HttpNotFound();
            }
            return View(requestForDemo);
        }

        // POST: RequestForDemo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            RequestForDemo requestForDemo = db.RequestForDemo.Find(id);
            db.RequestForDemo.Remove(requestForDemo);
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

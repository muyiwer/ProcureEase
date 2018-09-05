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
using static Utilities.EmailHelper;
using System.Threading.Tasks;

namespace ProcureEaseAPI.Controllers
{
    public class RequestForDemoController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: RequestForDemo
        public ActionResult Index()
        {
            try
            {
                return Json(new
                {
                    success = true,
                    message = "Ok",
                    data = db.RequestForDemo.Select(x => new
                    {
                        x.RequestID,
                        x.OrganizationFullName,
                        x.OrganizationShortName,
                        x.AdministratorEmail,
                        x.AdministratorFirstName,
                        x.AdministratorLastName,
                        x.AdministratorPhoneNumber,
                        DateCreated = x.DateCreated.Value.ToString()
                    }), 
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.REQUESTFORDEMO, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: RequestForDemoes/Details/5
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

        // GET: RequestForDemoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestForDemoes/Create
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

        // GET: RequestForDemoes/Edit/5
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

        // POST: RequestForDemoes/Edit/5
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

        // GET: RequestForDemoes/Delete/5
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

        // POST: RequestForDemoes/Delete/5
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


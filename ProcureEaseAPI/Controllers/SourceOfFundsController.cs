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
    public class SourceOfFundsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: SourceOfFunds
        public ActionResult Index()
        {
            return View(db.SourceOfFunds.ToList());
        }

        //http://localhost:85/SourceOfFunds/AddSourceOfFund
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddSourceOfFund(SourceOfFunds sourceOfFunds)
        {
            DateTime dt = DateTime.Now;
            sourceOfFunds.SourceOfFundID = Guid.NewGuid();
            sourceOfFunds.DateCreated = dt;
            sourceOfFunds.DateModified = dt;
            sourceOfFunds.CreatedBy = "Admin";
            db.SourceOfFunds.Add(sourceOfFunds);
            db.SaveChanges();
            return Json(db.SourceOfFunds.Where(y => y.SourceOfFundID == sourceOfFunds.SourceOfFundID).Select(x => new
            {
                sucess = true,
                message = "Source Of Fund added successfully!!!",
                data = new
                {
                    x.SourceOfFundID,
                    x.SourceOfFund,
                    x.EnableSourceOfFund,
                    x.CreatedBy,
                    x.DateCreated,
                    x.DateModified
                }
            }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        // GET: SourceOfFunds/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceOfFunds sourceOfFunds = db.SourceOfFunds.Find(id);
            if (sourceOfFunds == null)
            {
                return HttpNotFound();
            }
            return View(sourceOfFunds);
        }

        // GET: SourceOfFunds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SourceOfFunds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SourceOfFundID,SourceOfFund,EnableSourceOfFund,DateModified,CreatedBy,DateCreated")] SourceOfFunds sourceOfFunds)
        {
            if (ModelState.IsValid)
            {
                sourceOfFunds.SourceOfFundID = Guid.NewGuid();
                db.SourceOfFunds.Add(sourceOfFunds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sourceOfFunds);
        }

        // GET: SourceOfFunds/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceOfFunds sourceOfFunds = db.SourceOfFunds.Find(id);
            if (sourceOfFunds == null)
            {
                return HttpNotFound();
            }
            return View(sourceOfFunds);
        }

        // POST: SourceOfFunds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SourceOfFundID,SourceOfFund,EnableSourceOfFund,DateModified,CreatedBy,DateCreated")] SourceOfFunds sourceOfFunds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sourceOfFunds).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sourceOfFunds);
        }

        // GET: SourceOfFunds/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SourceOfFunds sourceOfFunds = db.SourceOfFunds.Find(id);
            if (sourceOfFunds == null)
            {
                return HttpNotFound();
            }
            return View(sourceOfFunds);
        }

        // POST: SourceOfFunds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SourceOfFunds sourceOfFunds = db.SourceOfFunds.Find(id);
            db.SourceOfFunds.Remove(sourceOfFunds);
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

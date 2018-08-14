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
    public class AdvertsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: Adverts
        public ActionResult Index()
        {
            var adverts = db.Adverts.Include(a => a.AdvertStatus).Include(a => a.BudgetYear);
            return View(adverts.ToList());
        }

        // GET: Adverts/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adverts adverts = db.Adverts.Find(id);
            if (adverts == null)
            {
                return HttpNotFound();
            }
            return View(adverts);
        }

        // GET: Adverts/Create
        public ActionResult Create()
        {
            ViewBag.AdvertStatusID = new SelectList(db.AdvertStatus, "AdvertStatusID", "Status");
            ViewBag.BudgetYearID = new SelectList(db.BudgetYear, "BudgetYearID", "CreatedBy");
            return View();
        }

        // POST: Adverts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdvertID,BudgetYearID,AdvertStatusID,Headline,Introduction,ScopeOfWork,EligibiltyRequirement,CollectionOfTenderDocument,BidSubmission,OtherImportantInformation,BidOpeningDate,BidClosingDate,CreatedBy,DateCreated,DateModified")] Adverts adverts)
        {
            if (ModelState.IsValid)
            {
                adverts.AdvertID = Guid.NewGuid();
                db.Adverts.Add(adverts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdvertStatusID = new SelectList(db.AdvertStatus, "AdvertStatusID", "Status", adverts.AdvertStatusID);
            ViewBag.BudgetYearID = new SelectList(db.BudgetYear, "BudgetYearID", "CreatedBy", adverts.BudgetYearID);
            return View(adverts);
        }

        // GET: Adverts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adverts adverts = db.Adverts.Find(id);
            if (adverts == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdvertStatusID = new SelectList(db.AdvertStatus, "AdvertStatusID", "Status", adverts.AdvertStatusID);
            ViewBag.BudgetYearID = new SelectList(db.BudgetYear, "BudgetYearID", "CreatedBy", adverts.BudgetYearID);
            return View(adverts);
        }

        // POST: Adverts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdvertID,BudgetYearID,AdvertStatusID,Headline,Introduction,ScopeOfWork,EligibiltyRequirement,CollectionOfTenderDocument,BidSubmission,OtherImportantInformation,BidOpeningDate,BidClosingDate,CreatedBy,DateCreated,DateModified")] Adverts adverts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adverts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdvertStatusID = new SelectList(db.AdvertStatus, "AdvertStatusID", "Status", adverts.AdvertStatusID);
            ViewBag.BudgetYearID = new SelectList(db.BudgetYear, "BudgetYearID", "CreatedBy", adverts.BudgetYearID);
            return View(adverts);
        }

        // GET: Adverts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adverts adverts = db.Adverts.Find(id);
            if (adverts == null)
            {
                return HttpNotFound();
            }
            return View(adverts);
        }

        // POST: Adverts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Adverts adverts = db.Adverts.Find(id);
            db.Adverts.Remove(adverts);
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

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
    public class SourceOfFundsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        private CatalogsController catalog = new CatalogsController();

        // GET: SourceOfFunds
        public ActionResult Index()
        {
            return View(db.SourceOfFunds.ToList());
        }

        // POST: SourceOfFunds/AddSourceOfFunds
        [HttpPost]
        public ActionResult AddSourceOfFunds(SourceOfFunds sourceOfFunds)
        {
            try
            {
                DateTime dt = DateTime.Now;
                sourceOfFunds.SourceOfFundID = Guid.NewGuid();
                sourceOfFunds.TenantID = catalog.GetTenantID();
                sourceOfFunds.DateCreated = dt;
                sourceOfFunds.DateModified = dt;
                sourceOfFunds.CreatedBy = "MDA Administrator";
                db.SourceOfFunds.Add(sourceOfFunds);

                SourceOfFundsOrganizationSettings sourceOfFundsOrganisationSettings = new SourceOfFundsOrganizationSettings();
                sourceOfFundsOrganisationSettings.SourceOfFundID = sourceOfFunds.SourceOfFundID;
                sourceOfFundsOrganisationSettings.TenantID = catalog.GetTenantID();
                //sourceOfFundsOrganisationSettings.OrganisationID = catalog.GetOrganizationID();
                sourceOfFundsOrganisationSettings.EnableSourceOfFund = sourceOfFundsOrganisationSettings.EnableSourceOfFund;
                sourceOfFundsOrganisationSettings.DateCreated = dt;
                sourceOfFundsOrganisationSettings.DateModified = dt;
                db.SourceOfFundsOrganisationSettings.Add(sourceOfFundsOrganisationSettings);

                db.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Source Of Fund added successfully!!!",
                    data = db.SourceOfFunds.Select(x => new
                    {
                        TenantID = db.SourceOfFunds.Where(y => y.TenantID == x.TenantID).Select(y => x.TenantID),
                        x.SourceOfFundID,
                        x.SourceOfFund,
                        Enabled = db.SourceOfFundsOrganisationSettings.Where(y => y.SourceOfFundID == y.SourceOfFundID).Select(y => y.EnableSourceOfFund)
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_SOURCEOFFUNDS, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
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

        // POST: SourceOfFunds/Edit
        [HttpPost]
        public ActionResult Edit([Bind(Include = "SourceOfFundID,SourceOfFund,EnableSourceOfFund,DateModified,CreatedBy,DateCreated")] SourceOfFunds sourceOfFunds)
        {
            try
            {

                DateTime dt = DateTime.Now;
                var currentSourceOfFund = db.SourceOfFunds.FirstOrDefault(s => s.SourceOfFundID == s.SourceOfFundID);
                var sourceOfFundOrganizationSettings = db.SourceOfFundsOrganisationSettings.FirstOrDefault(s => s.SourceOfFundID == s.SourceOfFundID);

                if (currentSourceOfFund == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_SOURCEOFFUNDS, "SourceOfFundID not found");
                    return Json(new
                    {
                        success = false,
                        message = "SourceOfFundID not found",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }

                currentSourceOfFund.DateModified = dt;

                sourceOfFundOrganizationSettings.EnableSourceOfFund = sourceOfFundOrganizationSettings.EnableSourceOfFund;
                sourceOfFundOrganizationSettings.DateModified = dt;

                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Edited successfully",
                    data = db.SourceOfFunds.Select(x => new
                    {
                        TenantID = db.SourceOfFunds.Where(y => y.TenantID == x.TenantID).Select(y => x.TenantID),
                        x.SourceOfFundID,
                        x.SourceOfFund,
                        Enabled = db.SourceOfFundsOrganisationSettings.Where(y => y.SourceOfFundID == y.SourceOfFundID).Select(y => y.EnableSourceOfFund)
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_SOURCEOFFUNDS, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
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

﻿using System;
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
    public class TelephoneNumbersController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        CatalogsController catalog = new CatalogsController();

        // GET: TelephoneNumbers
        public ActionResult Index()
        {
            var TelephoneNumbers = db.TelephoneNumbers.Include(b => b.OrganizationID);
            return Json(TelephoneNumbers, JsonRequestBehavior.AllowGet);
            //var telephoneNumbers = db.TelephoneNumbers.Include(t => t.OrganizationSettings);
            //return Json(telephoneNumbers.ToArray(), JsonRequestBehavior.AllowGet);
        }

        // GET: TelephoneNumbers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelephoneNumbers telephoneNumbers = db.TelephoneNumbers.Find(id);
            if (telephoneNumbers == null)
            {
                return HttpNotFound();
            }
            return View(telephoneNumbers);
        }

        // GET: TelephoneNumbers/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationID = new SelectList(db.OrganizationSettings, "OrganizationID", "OrganizationNameInFull");
            return View();
        }

        // POST: TelephoneNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TelephoneNumberID,TelephoneNumber,DateModified,CreatedBy,DateCreated,OrganizationID")] TelephoneNumbers telephoneNumbers)
        {
            if (ModelState.IsValid)
            {
                telephoneNumbers.TelephoneNumberID = Guid.NewGuid();
                db.TelephoneNumbers.Add(telephoneNumbers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationID = new SelectList(db.OrganizationSettings, "OrganizationID", "OrganizationNameInFull", telephoneNumbers.OrganizationID);
            return View(telephoneNumbers);
        }

        // GET: TelephoneNumbers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelephoneNumbers telephoneNumbers = db.TelephoneNumbers.Find(id);
            if (telephoneNumbers == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationID = new SelectList(db.OrganizationSettings, "OrganizationID", "OrganizationNameInFull", telephoneNumbers.OrganizationID);
            return View(telephoneNumbers);
        }

        // POST: TelephoneNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TelephoneNumberID,TelephoneNumber,DateModified,CreatedBy,DateCreated,OrganizationID")] TelephoneNumbers telephoneNumbers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telephoneNumbers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationID = new SelectList(db.OrganizationSettings, "OrganizationID", "OrganizationNameInFull", telephoneNumbers.OrganizationID);
            return View(telephoneNumbers);
        }

        // GET: TelephoneNumbers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TelephoneNumbers telephoneNumbers = db.TelephoneNumbers.Find(id);
            if (telephoneNumbers == null)
            {
                return HttpNotFound();
            }

            return View(telephoneNumbers);
        }

        // POST: TelephoneNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
            try
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
                TelephoneNumbers telephoneNumbers = db.TelephoneNumbers.Find(id);
                db.TelephoneNumbers.Remove(telephoneNumbers);
                db.SaveChanges();
                var TelephoneNumbers = db.TelephoneNumbers.Select(x => new
                {

                });
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.DELETE_TELEPHONENUMBER, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "Deleted Successfully",
                data = db.TelephoneNumbers.Where(x => x.TenantID == tenantId).Select(x => new
                {
                    x.TelephoneNumberID,
                    x.TelephoneNumber,
                    x.OrganizationID,
                })
            });
        }

        private ActionResult ExceptionError(string message, string StackTrace)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { InternalError = StackTrace }
            }, JsonRequestBehavior.AllowGet);
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

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
    public class ProcurementMethodsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        private CatalogsController catalog = new CatalogsController();

        // GET: ProcurementMethods
        public ActionResult Index()
        {
            return View(db.ProcurementMethod.ToList());
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

        // POST: ProcurementMethod/AddProcurementMethod
        [HttpPost]
        public ActionResult AddProcurementMethod(ProcurementMethod procurementMethod)
        {
            try
            {
                DateTime dt = DateTime.Now;
                procurementMethod.ProcurementMethodID = Guid.NewGuid();
                procurementMethod.DateCreated = dt;
                procurementMethod.DateModified = dt;
                procurementMethod.CreatedBy = "Techspecialist";
                db.ProcurementMethod.Add(procurementMethod);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_PROCUREMENTMETHOD, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "Procurement Method added successfully!!!",
                data = db.ProcurementMethodOrganizationSettings.Select(x => new
                {
                    x.ProcurementMethodID,
                    x.ProcurementMethod.Name,
                    x.EnableProcurementMethod,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: ProcurementMethods/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcurementMethod procurementMethod = db.ProcurementMethod.Find(id);
            if (procurementMethod == null)
            {
                return HttpNotFound();
            }
            return View(procurementMethod);
        }

        // GET: ProcurementMethods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProcurementMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProcurementMethodID,ProcurementMethod1,EnableProcurementMethod,DateModified,CreatedBy,DateCreated")] ProcurementMethod procurementMethod)
        {
            if (ModelState.IsValid)
            {
                procurementMethod.ProcurementMethodID = Guid.NewGuid();
                db.ProcurementMethod.Add(procurementMethod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(procurementMethod);
        }

        // GET: ProcurementMethods/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcurementMethod procurementMethod = db.ProcurementMethod.Find(id);
            if (procurementMethod == null)
            {
                return HttpNotFound();
            }
            return View(procurementMethod);
        }

        // POST: ProcurementMethod/Edit
        [HttpPost]
        public ActionResult Edit(ProcurementMethodOrganizationSettings procurementMethodOrganizationSettings, bool EnableProcurementMethod)
        {
            Guid? tenantId = catalog.GetTenantID();
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
                DateTime dt = DateTime.Now;
                var currentProcurementMethodID = db.ProcurementMethodOrganizationSettings.FirstOrDefault(p => p.ProcurementMethodID == procurementMethodOrganizationSettings.ProcurementMethodID);

                if (currentProcurementMethodID == null)
                { 
                    LogHelper.Log(Log.Event.UPDATE_PROCUREMENTMETHOD, "ProcurementMethodID not found");
                    return Json(new
                    {
                        success = false,
                        message = "ProcurementMethodID not found",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }

                currentProcurementMethodID.EnableProcurementMethod = EnableProcurementMethod;
                currentProcurementMethodID.DateModified = dt;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_PROCUREMENTMETHOD, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "Editted successfully!!!",
                data = db.ProcurementMethodOrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
                {
                    x.ProcurementMethodID,
                    x.ProcurementMethod.Name,
                    x.EnableProcurementMethod,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: ProcurementMethods/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcurementMethod procurementMethod = db.ProcurementMethod.Find(id);
            if (procurementMethod == null)
            {
                return HttpNotFound();
            }
            return View(procurementMethod);
        }

        // POST: ProcurementMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProcurementMethod procurementMethod = db.ProcurementMethod.Find(id);
            db.ProcurementMethod.Remove(procurementMethod);
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

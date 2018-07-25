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
    public class ProcurementMethodsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: ProcurementMethods
        public ActionResult Index()
        {
            return View(db.ProcurementMethod.ToList());
        }

        //http://localhost:85/ProcurementMethods/AddProcurementMethod
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddProcurementMethod(ProcurementMethod procurementMethod)
        {
            DateTime dt = DateTime.Now;
            procurementMethod.ProcurementMethodID = Guid.NewGuid();
            procurementMethod.DateCreated = dt;
            procurementMethod.DateModified = dt;
            procurementMethod.CreatedBy = "Admin";
            db.ProcurementMethod.Add(procurementMethod);
            db.SaveChanges();
            return Json(db.ProcurementMethod.Where(y => y.ProcurementMethodID == procurementMethod.ProcurementMethodID).Select(x => new
            {
                sucess = true,
                message = "Procurement Method added successfully!!!",
                data = new
                {
                    x.ProcurementMethodID,
                    x.Name,
                    x.EnableProcurementMethod,
                    x.CreatedBy,
                    x.DateCreated,
                    x.DateModified
                }
            }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
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

        // POST: ProcurementMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProcurementMethodID,ProcurementMethod1,EnableProcurementMethod,DateModified,CreatedBy,DateCreated")] ProcurementMethod procurementMethod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(procurementMethod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(procurementMethod);
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

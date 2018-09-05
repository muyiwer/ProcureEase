using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcureEaseAPI.Models;
using System.Threading.Tasks;
using Utilities;

namespace ProcureEaseAPI.Controllers
{
    public class OrganizationSettingsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: OrganizationSettings
        public ActionResult Index()
        {
            return View(db.OrganizationSettings.ToList());
        }

        //http://localhost:8/OrganizationSettings/UpdateBasicDetails
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddBasicDetails([Bind(Include = "OrganizationID,OrganizationNameInFull,OrganizationNameAbbreviation,OrganizationEmail,Address,Country,State,AboutOrganization,DateModified,CreatedBy,DateCreated")]OrganizationSettings organizationSettings)
        {
            if (ModelState.IsValid)
            {
                DateTime dt = DateTime.Now;
                organizationSettings.OrganizationID = Guid.NewGuid();
                organizationSettings.DateCreated = dt;
                organizationSettings.CreatedBy = "Admin";
                db.OrganizationSettings.Add(organizationSettings);
                db.SaveChanges();
                var BasicDetails = db.OrganizationSettings.Select(x => new
                {
                        x.OrganizationID,
                        DateCreated = x.DateCreated.Value.ToString(),
                        x.CreatedBy
                });
                var AdminDashboard = new
                {
                    success = true,
                    message = "Organization onboarded successfully!!!",
                    data = new
                    {
                        BasicDetails = BasicDetails,
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        public void AddTelephone(OrganizationSettings organizationSettings, params string[] telephoneNumbers)
        {
            foreach (var telephone in telephoneNumbers)
            {
                DateTime dt = DateTime.Now;
                db.TelephoneNumbers.Add(new TelephoneNumbers
                {
                    TelephoneNumberID = Guid.NewGuid(),
                    OrganizationID = organizationSettings.OrganizationID,
                    TelephoneNumber = telephone,
                    DateCreated = dt,
                    DateModified = dt,
                    CreatedBy = "Admin"
                });
                db.SaveChanges();
            }
        }
        // GET: OrganizationSettings/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationSettings organizationSettings = db.OrganizationSettings.Find(id);
            if (organizationSettings == null)
            {
                return HttpNotFound();
            }
            return View(organizationSettings);
        }

        // GET: OrganizationSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizationSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrganizationID,OrganizationNameInFull,OrganizationNameAbbreviation,OrganizationEmail,Address,Country,State,AboutOrganization,DateModified,CreatedBy,DateCreated,OrganizationLogoPath")] OrganizationSettings organizationSettings)
        {
            if (ModelState.IsValid)
            {
                organizationSettings.OrganizationID = Guid.NewGuid();
                db.OrganizationSettings.Add(organizationSettings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(organizationSettings);
        }

        // GET: OrganizationSettings/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationSettings organizationSettings = db.OrganizationSettings.Find(id);
            if (organizationSettings == null)
            {
                return HttpNotFound();
            }
            return View(organizationSettings);
        }

        // POST: OrganizationSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrganizationID,OrganizationNameInFull,OrganizationNameAbbreviation,OrganizationEmail,Address,Country,State,AboutOrganization,DateModified,CreatedBy,DateCreated,OrganizationLogoPath")] OrganizationSettings organizationSettings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizationSettings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organizationSettings);
        }

        // GET: OrganizationSettings/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationSettings organizationSettings = db.OrganizationSettings.Find(id);
            if (organizationSettings == null)
            {
                return HttpNotFound();
            }
            return View(organizationSettings);
        }

        // POST: OrganizationSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            OrganizationSettings organizationSettings = db.OrganizationSettings.Find(id);
            db.OrganizationSettings.Remove(organizationSettings);
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

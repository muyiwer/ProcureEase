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
            catch (Exception)
            {

                throw;
            }
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

        // POST: RequestForDemo/Add
        [HttpPost]
        public async Task<ActionResult> Add(RequestForDemo requestForDemo)
        {
            try
            {
                DateTime dt = DateTime.Now;
                requestForDemo.RequestID = Guid.NewGuid();
                requestForDemo.DateCreated = dt;
                db.RequestForDemo.Add(requestForDemo);

                OrganizationSettings organizationSettings = new OrganizationSettings();
                organizationSettings.OrganizationID = Guid.NewGuid();
                organizationSettings.OrganizationNameInFull = requestForDemo.OrganizationFullName;
                organizationSettings.OrganizationNameAbbreviation = requestForDemo.OrganizationShortName;
                organizationSettings.DateCreated = dt;
                db.OrganizationSettings.Add(organizationSettings);

                db.SaveChanges();

                var RecipientEmail = requestForDemo.AdministratorEmail;
                string Subject = "Request For Demo";
                string Body = new EmailTemplateHelper().GetTemplateContent("RequestForDemoTemplate");
                string newTemplateContent = string.Format(Body,requestForDemo.AdministratorEmail);
              //  newTemplateContent = newTemplateContent.Replace("[RecipientEmail]", RecipientEmail.Trim());
                Message message = new Message(RecipientEmail, Subject, newTemplateContent);
                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.AddEmailToQueue(message);
                return Json(new
                {
                    success = true,
                    message = "Request Sent Successfully",
                    data = db.RequestForDemo.Where(x=> x.RequestID == x.RequestID).Select(x => new
                    {
                        x.RequestID,
                        x.OrganizationFullName,
                        x.OrganizationShortName,
                        x.AdministratorEmail,
                        x.AdministratorFirstName,
                        x.AdministratorLastName,
                        x.AdministratorPhoneNumber,
                        DateCreated = x.DateCreated.Value.ToString()
                    })
                });
            }
            catch (Exception)
            {

                throw;
            }
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

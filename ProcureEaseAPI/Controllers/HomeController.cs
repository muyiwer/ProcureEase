using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities;
using static Utilities.EmailHelper;

namespace ProcureEaseAPI.Controllers
{
    public class HomeController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        // POST: Home/RequestForDemo
        [HttpPost]
        public async Task<ActionResult> RequestForDemo(RequestForDemo requestForDemo)
        {
            try
            {
                var url = System.Web.HttpContext.Current.Request.Url.Host;
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

                Catalog catalog = new Catalog();
                catalog.TenantID = Guid.NewGuid();
                catalog.RequestID = requestForDemo.RequestID;
                catalog.SubDomain = url;
                catalog.OrganizationID = organizationSettings.OrganizationID;
                catalog.DateCreated = dt;
                catalog.DateModified = dt;
                db.Catalog.Add(catalog);
                db.SaveChanges();

                requestForDemo = db.RequestForDemo.Find(requestForDemo.RequestID);
                requestForDemo.TenantID = catalog.TenantID;
                db.Entry(requestForDemo).State = EntityState.Modified;

                organizationSettings = db.OrganizationSettings.Find(organizationSettings.OrganizationID);
                organizationSettings.TenantID = catalog.TenantID;
                db.Entry(organizationSettings).State = EntityState.Modified;
                db.SaveChanges();

                var RecipientEmail = requestForDemo.AdministratorEmail;
                string Subject = "Request For Demo";
                string Body = new EmailTemplateHelper().GetTemplateContent("RequestForDemoTemplate");
                string newTemplateContent = string.Format(Body, requestForDemo.AdministratorEmail);
                //newTemplateContent = newTemplateContent.Replace("[RecipientEmail]", RecipientEmail.Trim());
                Message message = new Message(RecipientEmail, Subject, newTemplateContent);
                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.AddEmailToQueue(message);
                return Json(new
                {
                    success = true,
                    message = "Request Sent Successfully",
                    data = db.RequestForDemo.Where(x => x.RequestID == requestForDemo.RequestID).Select(x => new
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
    }
}

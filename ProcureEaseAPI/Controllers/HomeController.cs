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
                    })
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
            //ViewBag.Title = "Home Page";

            //return View();
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
                db.SaveChanges();

                await SendMailToTechspecialist(requestForDemo);
                await SendMailToUser(requestForDemo);

                return Json(new
                {
                    success = true,
                    message = "Request Sent Successfully",
                    data = db.RequestForDemo.Where(x => x.RequestID == requestForDemo.RequestID).Select(x => new
                    {
                        x.TenantID,
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
        public async Task SendMailToTechspecialist(RequestForDemo requestForDemo)
        {
            var RecipientEmail = requestForDemo.AdministratorEmail;
            string Subject = "Request For Demo";
            string Body = new EmailTemplateHelper().GetTemplateContent("RequestForDemoTemplate_User");
            string newTemplateContent = string.Format(Body, requestForDemo.AdministratorEmail);
            //newTemplateContent = newTemplateContent.Replace("[RecipientEmail]", RecipientEmail.Trim());
            Message message = new Message(RecipientEmail, Subject, newTemplateContent);
            EmailHelper emailHelper = new EmailHelper();
            await emailHelper.AddEmailToQueue(message);
        }
        public async Task SendMailToUser(RequestForDemo requestForDemo)
        {
            var RecipientEmail = requestForDemo.AdministratorEmail;
            string Subject = "Request For Demo";
            string Body = new EmailTemplateHelper().GetTemplateContent("RequestForDemoTemplate_Techspecialist");
            string newTemplateContent = string.Format(Body, requestForDemo.AdministratorEmail);
            //newTemplateContent = newTemplateContent.Replace("[RecipientEmail]", RecipientEmail.Trim());
            Message message = new Message(RecipientEmail, Subject, newTemplateContent);
            EmailHelper emailHelper = new EmailHelper();
            await emailHelper.AddEmailToQueue(message);
        }
    }
}

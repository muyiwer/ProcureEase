using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: Home/RequestForDemo
        [HttpPost]
        public async Task<ActionResult> RequestForDemo(RequestForDemo requestForDemo)
        {
            try
            {
                var OrganizationFullName = db.RequestForDemo.Where(x => x.OrganizationFullName == requestForDemo.OrganizationFullName).Select(x => x.OrganizationFullName).FirstOrDefault();
                if(OrganizationFullName != null)
                {
                    LogHelper.Log(Log.Event.REQUESTFORDEMO, "Duplicate insertion attempt, OrganizationFullName already exist");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Duplicate insertion attempt, Organization Full Name already exist");
                }
                var OrganizationShortName = db.RequestForDemo.Where(x => x.OrganizationShortName == requestForDemo.OrganizationShortName).Select(x => x.OrganizationShortName).FirstOrDefault();
                if (OrganizationShortName != null)
                {
                    LogHelper.Log(Log.Event.REQUESTFORDEMO, "Duplicate insertion attempted, OrganizationShortName already exist");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Duplicate insertion attempt, Organization Short Name already exist");
                }
                var AdministratorEmail = db.RequestForDemo.Where(x => x.AdministratorEmail == requestForDemo.AdministratorEmail).Select(x => x.AdministratorEmail).FirstOrDefault();
                if (AdministratorEmail != null)
                {
                    LogHelper.Log(Log.Event.REQUESTFORDEMO, "Duplicate insertion attempted, AdministratorEmail already exist");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Duplicate insertion attempt, Administrator Email already exist");
                }
                var AdministratorPhoneNumber = db.RequestForDemo.Where(x => x.AdministratorPhoneNumber == requestForDemo.AdministratorPhoneNumber).Select(x => x.AdministratorPhoneNumber).FirstOrDefault();
                if (AdministratorPhoneNumber != null)
                {
                    LogHelper.Log(Log.Event.REQUESTFORDEMO, "Duplicate insertion attempted, AdministratorPhoneNumber already exist");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Duplicate insertion attempt, Administrator PhoneNumber already exist");
                }
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
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
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
            string newTemplateContent = string.Format(Body, "annieajeks@gmail.com");
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

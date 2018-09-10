﻿using ProcureEaseAPI.Models;
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
        private CatalogsController catalog = new CatalogsController();

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
                if (OrganizationFullName != null)
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

        // POST: Home/OrganizationUnBoarding
        [HttpPost]
        public ActionResult OrganizationOnboarding(Guid RequestID)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string url = System.Web.HttpContext.Current.Request.Url.Host;
                var tenantID = catalog.GetTenantID();
                var ThisTenant = db.OrganizationSettings.Where(x => x.OrganizationID == x.OrganizationID).Select(x => x.OrganizationNameAbbreviation).FirstOrDefault();
                if (ThisTenant != null)
                {
                    LogHelper.Log(Log.Event.ONBOARDING, "Duplicate insertion attempt, Organization already exist");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Duplicate insertion attempt, Organization already exist");
                }
                else
                {
                    Catalog Tenant = new Catalog();
                    Tenant.TenantID = Guid.NewGuid();
                    Tenant.RequestID = RequestID;
                    Tenant.DateCreated = dt;
                    Tenant.DateModified = dt;
                    db.Catalog.Add(Tenant);
                    db.SaveChanges();
                    SaveTenantsRequestOnOrganizationSettings(RequestID);
                }

                var TenantID = db.Catalog.Where(x => x.TenantID == x.TenantID).Select(x => x.TenantID).FirstOrDefault();
                var UpdateOrganizationRecord = db.OrganizationSettings.FirstOrDefault(o => o.OrganizationID == o.OrganizationID);
                UpdateOrganizationRecord.TenantID = TenantID;
                UpdateOrganizationRecord.DateCreated = dt;

                var OrganizationID = db.OrganizationSettings.Where(x => x.OrganizationID == x.OrganizationID).Select(x => x.OrganizationID).FirstOrDefault();
                var SubDomain = db.OrganizationSettings.Where(x => x.OrganizationID == x.OrganizationID).Select(x => x.OrganizationNameAbbreviation).FirstOrDefault();
                var UpdateTenantRecord = db.Catalog.FirstOrDefault(o => o.TenantID == o.TenantID);
                UpdateTenantRecord.OrganizationID = OrganizationID;
                UpdateTenantRecord.SubDomain = SubDomain;
                UpdateTenantRecord.DateModified = dt;
                db.SaveChanges();

                SaveDefaultSouceOfFundRecord();
                SaveDefaultProcurementMethodRecord();
                SaveDefaultProjectCategoryRecord();

                return Json(new
                {
                    success = true,
                    message = "Organization Onboarded Successfully",
                    data = new { }
                });
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ONBOARDING, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public void SaveTenantsRequestOnOrganizationSettings(Guid RequestID)
        {
            DateTime dt = DateTime.Now;
            var Request = db.RequestForDemo.Where(x => x.RequestID == RequestID).ToList();
            foreach (RequestForDemo record in Request)
            {
                db.OrganizationSettings.Add(new OrganizationSettings()
                {
                    OrganizationID = Guid.NewGuid(),
                    OrganizationNameInFull = record.OrganizationFullName,
                    OrganizationNameAbbreviation = record.OrganizationShortName,
                    DateCreated = dt,
                    CreatedBy = "Techspecialist"
                });
                db.SaveChanges();
            }
        }
        public void SaveDefaultSouceOfFundRecord()
        {
            DateTime dt = DateTime.Now;
            var OrganizationID = db.OrganizationSettings.Where(x => x.OrganizationID == x.OrganizationID).Select(x => x.OrganizationID).FirstOrDefault();
            var TenantID = db.Catalog.Where(x => x.TenantID == x.TenantID).Select(x => x.TenantID).FirstOrDefault();
            var Sourceoffund = db.SourceOfFunds.ToList();
            foreach (SourceOfFunds source in Sourceoffund)
            {
                db.SourceOfFundsOrganizationSettings.Add(new SourceOfFundsOrganizationSettings()
                {
                    SourceOfFundID = source.SourceOfFundID,
                    OrganizationID = OrganizationID,
                    TenantID = TenantID,
                    EnableSourceOFFund = true,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
        }
        public void SaveDefaultProcurementMethodRecord()
        {
            DateTime dt = DateTime.Now;
            var OrganizationID = db.OrganizationSettings.Where(x => x.OrganizationID == x.OrganizationID).Select(x => x.OrganizationID).FirstOrDefault();
            var TenantID = db.Catalog.Where(x => x.TenantID == x.TenantID).Select(x => x.TenantID).FirstOrDefault();
            var ProcurementMethod = db.ProcurementMethod.ToList();
            foreach (ProcurementMethod source in ProcurementMethod)
            {
                db.ProcurementMethodOrganizationSettings.Add(new ProcurementMethodOrganizationSettings()
                {
                    ProcurementMethodID = source.ProcurementMethodID,
                    OrganizationID = OrganizationID,
                    TenantID = TenantID,
                    EnableProcurementMethod = true,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
        }
        public void SaveDefaultProjectCategoryRecord()
        {
            DateTime dt = DateTime.Now;
            var OrganizationID = db.OrganizationSettings.Where(x => x.OrganizationID == x.OrganizationID).Select(x => x.OrganizationID).FirstOrDefault();
            var TenantID = db.Catalog.Where(x => x.TenantID == x.TenantID).Select(x => x.TenantID).FirstOrDefault();
            var ProjectCategory = db.ProjectCategory.ToList();
            foreach (ProjectCategory source in ProjectCategory)
            {
                db.ProjectCategoryOrganizationSettings.Add(new ProjectCategoryOrganizationSettings()
                {
                    ProjectCategoryID = source.ProjectCategoryID,
                    OrganizationID = OrganizationID,
                    TenantID = TenantID,
                    EnableProjectCategory = true,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
        }
    }
}

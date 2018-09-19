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
        private CatalogsController catalog = new CatalogsController();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        #region ProcessGetOnboardingRequests
        // GET: Home/OnboardingRequests
        public ActionResult OnboardingRequests()
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
        #endregion

        #region ProcessRequestForDemo
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
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.REQUESTFORDEMO, ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ExceptionError(ex.Message, ex.StackTrace);
            }
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
        #endregion

        #region ProcessSendMailToTechspecialist
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
        #endregion

        #region SendMailToUser
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
        #endregion

        #region ProcessOnboarding
        [HttpPost]
        public async Task<ActionResult>  Onboarding(string AdministratorEmail, string Password)
        {
            try
            {
                DateTime dt = DateTime.Now;
                Guid TenantID = Guid.NewGuid();
                var GetRequestID = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.RequestID).FirstOrDefault();
                var ThisTenant = db.Catalog.Where(x => x.RequestID == GetRequestID).Select(x => x.RequestID).FirstOrDefault();
                if (AdministratorEmail == null)
                {
                    LogHelper.Log(Log.Event.ONBOARDING, "RequestID is null");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Please Input RequestID");
                }
                if (GetRequestID == null)
                {
                    LogHelper.Log(Log.Event.ONBOARDING, "RequestID does not exist");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("This Organization has not requested for a demo, RequestID does not exist");
                }
                if (ThisTenant != null)
                {
                    LogHelper.Log(Log.Event.ONBOARDING, "Duplicate insertion attempt, Organization already exist");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Duplicate insertion attempt, Organization already exist");
                }
                else
                {
                    Catalog Tenant = new Catalog();
                    Tenant.TenantID = TenantID;
                    Tenant.RequestID = GetRequestID;
                    Tenant.DateCreated = dt;
                    Tenant.DateModified = dt;
                    db.Catalog.Add(Tenant);
                    db.SaveChanges();
                }
                Guid OrganizationID = Guid.NewGuid();
                SaveTenantsRequestOnOrganizationSettings(GetRequestID, TenantID, OrganizationID);

                var SubDomain = db.RequestForDemo.Where(x=> x.RequestID == GetRequestID).Select(x => x.OrganizationShortName).FirstOrDefault();
                var UpdateTenantRecord = db.Catalog.FirstOrDefault(o => o.TenantID == TenantID);
                UpdateTenantRecord.OrganizationID = OrganizationID;
                UpdateTenantRecord.SubDomain = SubDomain;
                UpdateTenantRecord.DateModified = dt;
                db.SaveChanges();

                SaveDefaultSouceOfFundRecord(TenantID, OrganizationID);
                SaveDefaultProcurementMethodRecord(TenantID, OrganizationID);
                SaveDefaultProjectCategoryRecord(TenantID, OrganizationID);

                AddUserModel UserModel = new AddUserModel
                {
                    Email = AdministratorEmail,
                    Password = Password,
                    UserName = AdministratorEmail
                };
                AuthRepository Repository = new AuthRepository();
                ApplicationUser User = await Repository.RegisterAdmin(UserModel);

                var GetAdministratorFirstName = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.AdministratorFirstName).FirstOrDefault();
                var GetAdministratorLastName = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.AdministratorLastName).FirstOrDefault();
                UserProfile userProfile = new UserProfile();
                userProfile.UserID = Guid.NewGuid();
                userProfile.Id = User.Id;
                userProfile.DepartmentID = null;
                userProfile.OrganizationID = OrganizationID;
                userProfile.TenantID = TenantID;
                userProfile.UserEmail = User.Email;
                userProfile.FirstName = GetAdministratorFirstName;
                userProfile.LastName = GetAdministratorLastName;
                userProfile.DateCreated = dt;
                db.UserProfile.Add(userProfile);
                db.SaveChanges();

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
        #endregion

        #region ProcessSaveTenantsRequestOnOrganizationSettings
        public void SaveTenantsRequestOnOrganizationSettings(Guid? RequestID, Guid TenantID, Guid OrganizationID)
        {
            DateTime dt = DateTime.Now;
            var Request = db.RequestForDemo.Where(x => x.RequestID == RequestID).ToList();
            foreach (RequestForDemo record in Request)
            {
                db.OrganizationSettings.Add(new OrganizationSettings()
                {
                    OrganizationID = OrganizationID,
                    TenantID = TenantID,
                    OrganizationNameInFull = record.OrganizationFullName,
                    OrganizationNameAbbreviation = record.OrganizationShortName,
                    DateCreated = dt,
                    CreatedBy = "Techspecialist"
                });
                db.SaveChanges();
            }
        }
        #endregion

        #region ProcessSaveDefaultSouceOfFundRecord
        public void SaveDefaultSouceOfFundRecord(Guid TenantID, Guid OrganizationID)
        {
            DateTime dt = DateTime.Now;
            var Sourceoffund = db.SourceOfFunds.ToList();
            foreach (SourceOfFunds source in Sourceoffund)
            {
                db.SourceOfFundsOrganizationSettings.Add(new SourceOfFundsOrganizationSettings()
                {
                    SourceOfFundID = source.SourceOfFundID,
                    OrganizationID = OrganizationID,
                    TenantID = TenantID,
                    EnableSourceOFFund = false,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
               
                var DefaultSourceOFFund1 = "Budgetary allocation/appropriation";
                var DefaultSourceOFFundID1 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund1)).Select(x => x.SourceOfFundID).FirstOrDefault();
                var sourceOfFund1 = db.SourceOfFundsOrganizationSettings.Where(x => x.SourceOfFundID == DefaultSourceOFFundID1 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
                SourceOfFundsOrganizationSettings sourceOfFundsOrganizationSettings = sourceOfFund1.First();
                sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
                sourceOfFundsOrganizationSettings.DateModified = dt;

                var DefaultSourceOFFund2 = "Internally generated fund";
                var DefaultSourceOFFundID2 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund2)).Select(x => x.SourceOfFundID).FirstOrDefault();
                var sourceOfFund2 = db.SourceOfFundsOrganizationSettings.Where(x => x.SourceOfFundID == DefaultSourceOFFundID2 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
                sourceOfFundsOrganizationSettings = sourceOfFund2.First();
                sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
                sourceOfFundsOrganizationSettings.DateModified = dt;

                var DefaultSourceOFFund3 = "Special intervention fund";
                var DefaultSourceOFFundID3 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund3)).Select(x => x.SourceOfFundID).FirstOrDefault();
                var sourceOfFund3 = db.SourceOfFundsOrganizationSettings.Where(x => x.SourceOfFundID == DefaultSourceOFFundID3 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
                sourceOfFundsOrganizationSettings = sourceOfFund3.First();
                sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
                sourceOfFundsOrganizationSettings.DateModified = dt;

                var DefaultSourceOFFund4 = "Power sector intervention fund";
                var DefaultSourceOFFundID4 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund4)).Select(x => x.SourceOfFundID).FirstOrDefault();
                var sourceOfFund4 = db.SourceOfFundsOrganizationSettings.Where(x => x.SourceOfFundID == DefaultSourceOFFundID4 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
                sourceOfFundsOrganizationSettings = sourceOfFund4.First();
                sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
                sourceOfFundsOrganizationSettings.DateModified = dt;

                var DefaultSourceOFFund5 = "ETF Special intervention fund";
                var DefaultSourceOFFundID5 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund5)).Select(x => x.SourceOfFundID).FirstOrDefault();
                var sourceOfFund5 = db.SourceOfFundsOrganizationSettings.Where(x => x.SourceOfFundID == DefaultSourceOFFundID5 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
                sourceOfFundsOrganizationSettings = sourceOfFund5.First();
                sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
                sourceOfFundsOrganizationSettings.DateModified = dt;

                db.SaveChanges();
           
        }
        #endregion

        #region ProcessSaveDefaultProcurementMethodRecord
        public void SaveDefaultProcurementMethodRecord(Guid TenantID, Guid OrganizationID)
        {
            DateTime dt = DateTime.Now;
            var CurrentProcurementMethod = db.ProcurementMethodOrganizationSettings.Where(s => s.ProcurementMethodID == s.ProcurementMethodID);
            var ProcurementMethod = db.ProcurementMethod.ToList();
            foreach (ProcurementMethod source in ProcurementMethod)
            {
                db.ProcurementMethodOrganizationSettings.Add(new ProcurementMethodOrganizationSettings()
                {
                    ProcurementMethodID = source.ProcurementMethodID,
                    OrganizationID = OrganizationID,
                    TenantID = TenantID,
                    EnableProcurementMethod = false,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
            var DefaultProcurementMethod1 = "Selective Tendering";
            var DefaultProcurementMethodID1 = db.ProcurementMethod.Where(x => x.Name.Contains(DefaultProcurementMethod1)).Select(x => x.ProcurementMethodID ).FirstOrDefault();
            var ProcurementMethod1 = db.ProcurementMethodOrganizationSettings.Where(x => x.ProcurementMethodID == DefaultProcurementMethodID1 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
            ProcurementMethodOrganizationSettings procurementMethodOrganizationSettings = ProcurementMethod1.First();
            procurementMethodOrganizationSettings.EnableProcurementMethod = true;
            procurementMethodOrganizationSettings.DateModified = dt;

            var DefaultProcurementMethod2 = "Direct procurement";
            var DefaultProcurementMethodID2 = db.ProcurementMethod.Where(x => x.Name.Contains(DefaultProcurementMethod2)).Select(x => x.ProcurementMethodID).FirstOrDefault();
            var ProcurementMethod2 = db.ProcurementMethodOrganizationSettings.Where(x => x.ProcurementMethodID == DefaultProcurementMethodID2 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
            procurementMethodOrganizationSettings = ProcurementMethod2.First();
            procurementMethodOrganizationSettings.EnableProcurementMethod = true;
            procurementMethodOrganizationSettings.DateModified = dt;

            var DefaultProcurementMethod3 = "Open Competitive method";
            var DefaultProcurementMethodID3 = db.ProcurementMethod.Where(x => x.Name.Contains(DefaultProcurementMethod3)).Select(x => x.ProcurementMethodID).FirstOrDefault();
            var ProcurementMethod3 = db.ProcurementMethodOrganizationSettings.Where(x => x.ProcurementMethodID == DefaultProcurementMethodID3 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
            procurementMethodOrganizationSettings = ProcurementMethod3.First();
            procurementMethodOrganizationSettings.EnableProcurementMethod = true;
            procurementMethodOrganizationSettings.DateModified = dt;

            db.SaveChanges();
        }
        #endregion

        #region ProcessSaveDefaultProjectCategoryRecord
        public void SaveDefaultProjectCategoryRecord(Guid TenantID, Guid OrganizationID)
        {
            DateTime dt = DateTime.Now;
            var ProjectCategory = db.ProjectCategory.ToList();
            foreach (ProjectCategory source in ProjectCategory)
            {
                db.ProjectCategoryOrganizationSettings.Add(new ProjectCategoryOrganizationSettings()
                {
                    ProjectCategoryID = source.ProjectCategoryID,
                    OrganizationID = OrganizationID,
                    TenantID = TenantID,
                    EnableProjectCategory = false,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
            var DefaultProjectCategory1 = "Goods";
            var DefaultProjectCategoryID1 = db.ProjectCategory.Where(x => x.Name.Contains(DefaultProjectCategory1)).Select(x => x.ProjectCategoryID).FirstOrDefault();
            var ProjectCategory1 = db.ProjectCategoryOrganizationSettings.Where(x => x.ProjectCategoryID == DefaultProjectCategoryID1 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
            ProjectCategoryOrganizationSettings projectCategoryOrganizationSettings = ProjectCategory1.First();
            projectCategoryOrganizationSettings.EnableProjectCategory = true;
            projectCategoryOrganizationSettings.DateModified = dt;

            var DefaultProjectCategory2 = "Services";
            var DefaultProjectCategoryID2 = db.ProjectCategory.Where(x => x.Name.Contains(DefaultProjectCategory2)).Select(x => x.ProjectCategoryID).FirstOrDefault();
            var ProjectCategory2 = db.ProjectCategoryOrganizationSettings.Where(x => x.ProjectCategoryID == DefaultProjectCategoryID2 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
            projectCategoryOrganizationSettings = ProjectCategory2.First();
            projectCategoryOrganizationSettings.EnableProjectCategory = true;
            projectCategoryOrganizationSettings.DateModified = dt;

            var DefaultProjectCategory3 = "Works";
            var DefaultProjectCategoryID3 = db.ProjectCategory.Where(x => x.Name.Contains(DefaultProjectCategory3)).Select(x => x.ProjectCategoryID).FirstOrDefault();
            var ProjectCategory3 = db.ProjectCategoryOrganizationSettings.Where(x => x.ProjectCategoryID == DefaultProjectCategoryID3 && x.OrganizationID == OrganizationID && x.TenantID == TenantID).ToList();
            projectCategoryOrganizationSettings = ProjectCategory3.First();
            projectCategoryOrganizationSettings.EnableProjectCategory = true;
            projectCategoryOrganizationSettings.DateModified = dt;

            db.SaveChanges();
        }
        #endregion

        #region ProcessError
        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ProcessExceptionError
        private ActionResult ExceptionError(string message, string StackTrace)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { InternalError = StackTrace }
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}

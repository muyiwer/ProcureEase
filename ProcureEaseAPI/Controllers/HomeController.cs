using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private string GetConfiguration(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.CONFIGURATION, ex.Message);
                return "";
            }
        }

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

            var RecipientEmail = GetConfiguration("EmailHost");
            string Subject = "Request For Demo";
            string Body = new EmailTemplateHelper().GetTemplateContent("RequestForDemoTemplate_Techspecialist");
            string newTemplateContent = string.Format(Body, requestForDemo.AdministratorEmail);
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
            string Subject = "Techspecialist - ProcureEase App";
            string Body = new EmailTemplateHelper().GetTemplateContent("RequestForDemoTemplate_User");
            string newTemplateContent = string.Format(Body, requestForDemo.AdministratorFirstName);
            //newTemplateContent = newTemplateContent.Replace("[RecipientEmail]", RecipientEmail.Trim());
            Message message = new Message(RecipientEmail, Subject, newTemplateContent);
            EmailHelper emailHelper = new EmailHelper();
            await emailHelper.AddEmailToQueue(message);
        }
        #endregion

        #region SendMailToAdministrator
        public async Task SendMailToAdministrator(string AdministratorEmail, string Password)
        {
            var AdministratorFirstName = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.AdministratorFirstName).FirstOrDefault();
            var OrganizationFullName = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.OrganizationFullName).FirstOrDefault();
            var OrganizationShortName = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.OrganizationShortName).FirstOrDefault();
            var GetAdministratorPhoneNumber = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.AdministratorPhoneNumber).FirstOrDefault();
            string Subject = "Techspecialist - ProcureEase App";
            string Body = new EmailTemplateHelper().GetTemplateContent("AdministratorSignUpTemplate");
            try
            {
                var clientUrl = Request.UrlReferrer.ToString();
                string newTemplateContent = string.Format(Body, "http://" + clientUrl + ".procureease.com.ng", AdministratorFirstName, OrganizationFullName, Password);
                Message message = new Message(AdministratorEmail, Subject, newTemplateContent);
                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.AddEmailToQueue(message);
            }
            catch (NullReferenceException)
            {
                var backendUrl = System.Web.HttpContext.Current.Request.Url.Host;
                string newTemplateContent = string.Format(Body, "http://" + backendUrl + ".procureease.com.ng", AdministratorFirstName, OrganizationFullName, Password);
                Message message = new Message(AdministratorEmail, Subject, newTemplateContent);
                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.AddEmailToQueue(message);
            }
        }
        #endregion

        #region ProcessOnboarding
        [HttpPost]
        public async Task<ActionResult>  Onboarding(string AdministratorEmail, string Password)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var CurrentTime = DateTime.Now.AddYears(1);
                Guid TenantID = Guid.NewGuid();
                var GetEmail = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.AdministratorEmail).FirstOrDefault();
                var GetRequestID = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.RequestID).FirstOrDefault();

                if (AdministratorEmail == null)
                {
                    LogHelper.Log(Log.Event.ONBOARDING, "AdministratorEmail is null");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Please Input AdministratorEmail");
                }
                if (GetEmail == null)
                {
                    LogHelper.Log(Log.Event.ONBOARDING, AdministratorEmail + "has not requested for a demo," + " " + "Email does not exist.");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error(AdministratorEmail + " " + "has not requested for a Demo");
                }
                var OrganizationNameInFull = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.OrganizationFullName).FirstOrDefault();
                var ThisTenant = db.Catalog.Where(x => x.RequestID == GetRequestID).Select(x => x.RequestID).FirstOrDefault();
                if (ThisTenant != null)
                {
                    LogHelper.Log(Log.Event.ONBOARDING, "Onboarding has been done for" + " " + OrganizationNameInFull + " " + "by the Email" + " " + AdministratorEmail);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Onboarding has been done for" + " " + OrganizationNameInFull + " " + "by the Email" + " " + AdministratorEmail);
                }

                else
                {
                    db.Catalog.Add(new Catalog()
                    {
                    TenantID = TenantID,
                    RequestID = GetRequestID,
                    IsDemo = true,
                    IsActive = false,
                    DateCreated = dt,
                    DateModified = dt
                });
                    Guid OrganizationID = Guid.NewGuid();
                    SaveTenantsRequestOnOrganizationSettings(GetRequestID, TenantID, OrganizationID);

                    var SubDomain = db.RequestForDemo.Where(x => x.RequestID == GetRequestID).Select(x => x.OrganizationShortName).FirstOrDefault();
                    var UpdateCatalog = db.Catalog.Find(TenantID);
                    UpdateCatalog.OrganizationID = OrganizationID;
                    UpdateCatalog.SubDomain = SubDomain;
                    UpdateCatalog.DateModified = dt;

                    var UpdateRequestForDemo = db.RequestForDemo.Find(GetRequestID);
                    UpdateRequestForDemo.DemoStartDate = dt;
                    UpdateRequestForDemo.DemoEndDate = CurrentTime;

                    AddUserModel UserModel = new AddUserModel
                    {
                        Email = AdministratorEmail,
                        Password = Password,
                        UserName = AdministratorEmail
                    };
                    AuthRepository Repository = new AuthRepository();
                    ApplicationUser User = await Repository.RegisterAdmin(UserModel);

                    var RoleId = db.AspNetRoles.Where(x => x.Name == "MDA Administrator").Select(x => x.Id).FirstOrDefault();
                    AspNetUserRoles userRole = new AspNetUserRoles();
                    userRole.UserId = User.Id;
                    userRole.RoleId = RoleId;
                    db.AspNetUserRoles.Add(userRole);

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

                    SaveDefaultSouceOfFundRecord(TenantID, OrganizationID);
                    SaveDefaultProcurementMethodRecord(TenantID, OrganizationID);
                    SaveDefaultProjectCategoryRecord(TenantID, OrganizationID);

                    await SendMailToAdministrator(AdministratorEmail, Password);

                }
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

        #region ProcessActivatateMDAAccount
        [HttpPost]
        public ActionResult Activate(string AdministratorEmail)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var CurrentTime = DateTime.Now.AddYears(1);

                var GetRequestID = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.RequestID).FirstOrDefault();
                var GetTenantID = db.Catalog.Where(x => x.RequestID == GetRequestID).Select(x => x.TenantID).FirstOrDefault();

                var Tenant = db.Catalog.Find(GetTenantID);
                Tenant.IsDemo = false;
                Tenant.IsActive = true;
                Tenant.DateModified = dt;

                var UpdateRequestForDemo = db.RequestForDemo.Find(GetRequestID);
                UpdateRequestForDemo.DemoEndDate = null;
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Organization Account has been Activated Successfully",
                    data = db.Catalog.Select(x => new
                    {
                        x.OrganizationID,
                        OrganizationName = db.OrganizationSettings.Where(y => x.OrganizationID == y.OrganizationID).Select(y => y.OrganizationNameInFull).FirstOrDefault(),
                        x.IsDemo,
                        x.IsActive
                    })
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

        #region ProcessDeActivatateMDAAccount
        [HttpPost]
        public ActionResult Deactivate(string AdministratorEmail)
        {
            try
            {
                DateTime CurrentTimeNow = new DateTime();
                CurrentTimeNow = DateTime.Now;

                var GetRequestID = db.RequestForDemo.Where(x => x.AdministratorEmail == AdministratorEmail).Select(x => x.RequestID).FirstOrDefault();
                var GetTenantID = db.Catalog.Where(x => x.RequestID == GetRequestID).Select(x => x.TenantID).FirstOrDefault();
                //var CheckActiveEndDate = db.Catalog.Where(x => x.RequestID == GetRequestID).Select(x => x.ActiveEndDate).FirstOrDefault();
                //var NumberOfActiveDaysLeft =  (CheckActiveEndDate - CurrentTimeNow).Value.Days;  
                //if (CheckActiveEndDate >= CurrentTimeNow)
                //{
                //    LogHelper.Log(Log.Event.DEACTIVATE, "This Account has" + " "  + NumberOfActiveDaysLeft + " active days left before deactivation" );
                //    Response.StatusCode = (int)HttpStatusCode.Forbidden;
                //    return Error("This Account has" + " " + NumberOfActiveDaysLeft + " active days left before deactivation");
                //}
                var Tenant = db.Catalog.Find(GetTenantID);
                Tenant.IsActive = false;
                Tenant.DateModified = CurrentTimeNow;
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Organization Account has been Deactivated",
                    data = db.Catalog.Select(x => new
                    {
                        x.OrganizationID,
                        OrganizationName = db.OrganizationSettings.Where(y => x.OrganizationID == y.OrganizationID).Select(y => y.OrganizationNameInFull).FirstOrDefault(),
                        x.IsDemo,
                        x.IsActive
                    })
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

        #region ProcessGetListOfMDAs
        public ActionResult ListOfMDAs()
        {
            try
            {
                DateTime CurrentTimeNow = new DateTime();
                CurrentTimeNow = DateTime.Now;
                DateTime EndDate = CurrentTimeNow;

                return Json(new
                {
                    success = true,
                    message = "Ok",
                    data = db.OrganizationSettings.Select(x => new
                    {
                        x.OrganizationID,
                        x.OrganizationNameInFull,
                        Subdomain = db.Catalog.Where(y => y.OrganizationID == x.OrganizationID).Select(y => y.SubDomain).FirstOrDefault(),
                    })
                }, JsonRequestBehavior.AllowGet);
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

        #region ProcessManageOrganizationAccounts
        public ActionResult ManageOrganizationAccounts()
        {
            try
            {
                DateTime CurrentTimeNow = new DateTime();
                CurrentTimeNow = DateTime.Now;
                DateTime EndDate = CurrentTimeNow;
                return Json(new
                {
                    success = true,
                    message = "Ok",
                    data = db.OrganizationSettings.Select(x => new
                    {
                        x.OrganizationID,
                        x.OrganizationNameInFull,
                        Subdomain = db.Catalog.Where(y => y.OrganizationID == x.OrganizationID).Select(y => y.SubDomain).FirstOrDefault(),
                        IsDemo = db.Catalog.Where(y=> y.OrganizationID == x.OrganizationID).Select(y=> y.IsDemo).FirstOrDefault(),
                        IsActive = db.Catalog.Where(y => y.OrganizationID == x.OrganizationID).Select(y => y.IsActive).FirstOrDefault(),
                        AccountStatus = db.Catalog.Where(y => y.OrganizationID == x.OrganizationID && (y.IsDemo == true || y.IsActive == true)).Select(y => (true) || (false)).FirstOrDefault(),
                        NumberOfDemoDaysLeft = DbFunctions.DiffDays(CurrentTimeNow, db.RequestForDemo.Where(z => z.OrganizationFullName == x.OrganizationNameInFull).Select(z => z.DemoEndDate).FirstOrDefault()),
                       // NumberOfActiveDaysLeft = DbFunctions.DiffDays(CurrentTimeNow, db.Catalog.Where(z => z.OrganizationID == x.OrganizationID).Select(z => z.ActiveEndDate).FirstOrDefault())
                    })
                }, JsonRequestBehavior.AllowGet);
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

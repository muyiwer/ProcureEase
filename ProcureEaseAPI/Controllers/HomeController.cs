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

        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: Home/OnBoarding
        [HttpPost]
        public ActionResult Onboarding(Guid RequestID)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var tenantID = catalog.GetTenantID();
                var ThisTenant = db.Catalog.Where(x => x.TenantID == x.OrganizationID).Select(x => x.RequestID).FirstOrDefault();
                var GetRequestID = db.RequestForDemo.Where(x => x.RequestID == x.RequestID).Select(x=> x.RequestID).FirstOrDefault();
                if (RequestID == null)
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

                SaveDefaultProcurementMethodRecord();
                SaveDefaultSouceOfFundRecord();
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
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
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
                    EnableSourceOFFund = false,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
            var DefaultSourceOFFund1 = "Budgetary allocation/appropriation";
            var DefaultSourceOFFundID1 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund1)).Select(x => x.SourceOfFundID).FirstOrDefault();
            SourceOfFundsOrganizationSettings sourceOfFundsOrganizationSettings = db.SourceOfFundsOrganizationSettings.Find(DefaultSourceOFFundID1);
            sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
            sourceOfFundsOrganizationSettings.DateModified = dt;

            var DefaultSourceOFFund2 = "Internally generated fund";
            var DefaultSourceOFFundID2 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund2)).Select(x => x.SourceOfFundID).FirstOrDefault();
            sourceOfFundsOrganizationSettings = db.SourceOfFundsOrganizationSettings.Find(DefaultSourceOFFundID2);
            sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
            sourceOfFundsOrganizationSettings.DateModified = dt;

            var DefaultSourceOFFund3 = "Special intervention fund";
            var DefaultSourceOFFundID3 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund3)).Select(x => x.SourceOfFundID).FirstOrDefault();
            sourceOfFundsOrganizationSettings = db.SourceOfFundsOrganizationSettings.Find(DefaultSourceOFFundID3);
            sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
            sourceOfFundsOrganizationSettings.DateModified = dt;

            var DefaultSourceOFFund4 = "Power sector intervention fund";
            var DefaultSourceOFFundID4 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund4)).Select(x => x.SourceOfFundID).FirstOrDefault();
            sourceOfFundsOrganizationSettings = db.SourceOfFundsOrganizationSettings.Find(DefaultSourceOFFundID4);
            sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
            sourceOfFundsOrganizationSettings.DateModified = dt;

            var DefaultSourceOFFund5 = "ETF Special intervention fund";
            var DefaultSourceOFFundID5 = db.SourceOfFunds.Where(x => x.SourceOfFund.Contains(DefaultSourceOFFund5)).Select(x => x.SourceOfFundID).FirstOrDefault();
            sourceOfFundsOrganizationSettings = db.SourceOfFundsOrganizationSettings.Find(DefaultSourceOFFundID5);
            sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
            sourceOfFundsOrganizationSettings.DateModified = dt;

            db.Entry(sourceOfFundsOrganizationSettings).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void SaveDefaultProcurementMethodRecord()
        {
            DateTime dt = DateTime.Now;
            var CurrentProcurementMethod = db.ProcurementMethodOrganizationSettings.Where(s => s.ProcurementMethodID == s.ProcurementMethodID);
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
                    EnableProcurementMethod = false,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
            var DefaultProcurementMethod1 = "Selective Tendering";
            var DefaultProcurementMethodID1 = db.ProcurementMethod.Where(x => x.Name.Contains(DefaultProcurementMethod1)).Select(x => x.ProcurementMethodID).FirstOrDefault();
            ProcurementMethodOrganizationSettings procurementMethodOrganizationSettings = db.ProcurementMethodOrganizationSettings.Find(DefaultProcurementMethodID1);
            procurementMethodOrganizationSettings.EnableProcurementMethod = true;
            procurementMethodOrganizationSettings.DateModified = dt;

            var DefaultProcurementMethod2 = "Direct procurement";
            var DefaultProcurementMethodID2 = db.ProcurementMethod.Where(x => x.Name.Contains(DefaultProcurementMethod2)).Select(x => x.ProcurementMethodID).FirstOrDefault();
            procurementMethodOrganizationSettings = db.ProcurementMethodOrganizationSettings.Find(DefaultProcurementMethodID2);
            procurementMethodOrganizationSettings.EnableProcurementMethod = true;
            procurementMethodOrganizationSettings.DateModified = dt;

            var DefaultProcurementMethod3 = "Open Competitive method";
            var DefaultProcurementMethodID3 = db.ProcurementMethod.Where(x => x.Name.Contains(DefaultProcurementMethod3)).Select(x => x.ProcurementMethodID).FirstOrDefault();
            procurementMethodOrganizationSettings = db.ProcurementMethodOrganizationSettings.Find(DefaultProcurementMethodID3);
            procurementMethodOrganizationSettings.EnableProcurementMethod = true;
            procurementMethodOrganizationSettings.DateModified = dt;

            db.Entry(procurementMethodOrganizationSettings).State = EntityState.Modified;
            db.SaveChanges();
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
                    EnableProjectCategory = false,
                    DateCreated = dt
                });
                db.SaveChanges();
            }
            var DefaultProjectCategory1 = "Goods";
            var DefaultProjectCategoryID1 = db.ProjectCategory.Where(x => x.Name.Contains(DefaultProjectCategory1)).Select(x => x.ProjectCategoryID).FirstOrDefault();
            ProjectCategoryOrganizationSettings projectCategoryOrganizationSettings = db.ProjectCategoryOrganizationSettings.Find(DefaultProjectCategoryID1);
            projectCategoryOrganizationSettings.EnableProjectCategory = true;
            projectCategoryOrganizationSettings.DateModified = dt;

            var DefaultProjectCategory2 = "Services";
            var DefaultProjectCategoryID2 = db.ProjectCategory.Where(x => x.Name.Contains(DefaultProjectCategory2)).Select(x => x.ProjectCategoryID).FirstOrDefault();
            projectCategoryOrganizationSettings = db.ProjectCategoryOrganizationSettings.Find(DefaultProjectCategoryID2);
            projectCategoryOrganizationSettings.EnableProjectCategory = true;
            projectCategoryOrganizationSettings.DateModified = dt;

            var DefaultProjectCategory3 = "Works";
            var DefaultProjectCategoryID3 = db.ProjectCategory.Where(x => x.Name.Contains(DefaultProjectCategory3)).Select(x => x.ProjectCategoryID).FirstOrDefault();
            projectCategoryOrganizationSettings = db.ProjectCategoryOrganizationSettings.Find(DefaultProjectCategoryID3);
            projectCategoryOrganizationSettings.EnableProjectCategory = true;
            projectCategoryOrganizationSettings.DateModified = dt;

            db.Entry(projectCategoryOrganizationSettings).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}

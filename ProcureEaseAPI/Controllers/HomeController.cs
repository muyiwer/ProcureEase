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
                    //LogHelper.Log(Log.Event.ONBOARDING, "Duplicate insertion attempt, Organization already exist");
                    //Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //return Error("Duplicate insertion attempt, Organization already exist");
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

                var SourceOfFunds = db.SourceOfFundsOrganizationSettings.Where(x => x.TenantID == tenantID).Select(x => new
                {
                    x.SourceOfFundID,
                    x.SourceOfFunds.SourceOfFund,
                    x.EnableSourceOFFund
                });

                var ProcurementMethod = db.ProcurementMethodOrganizationSettings.Where(x => x.TenantID == tenantID).Select(x => new
                {
                    x.ProcurementMethodID,
                    x.ProcurementMethod.Name,
                    x.EnableProcurementMethod
                });
                var ProjectCategory = db.ProjectCategoryOrganizationSettings.Where(x => x.TenantID == tenantID).Select(x => new
                {
                    x.ProjectCategoryID,
                    x.ProjectCategory.Name,
                    x.EnableProjectCategory
                });
                return Json(new
                {
                    success = true,
                    message = "Organization Onboarded Successfully",
                    data = db.Catalog.Where(x => x.TenantID == tenantID).Select(x => new
                    {
                        SourceOfFunds = SourceOfFunds,
                        ProcurementMethod = ProcurementMethod,
                        ProjectCategory = ProjectCategory
                    })
                });
            }
            catch (Exception ex)
            {
                //LogHelper.Log(Log.Event.ONBOARDING, ex.Message);
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
            //List<string> string1 = new List<string>();
            //string1.Add("test1");
            //string1.Add("test2");
            //string1.Add("test3");
            //string1.Add("test4");
            //var UpdateSourceOfFund = db.SourceOfFunds.Select(x => x.SourceOfFund == "Budgetary allocation/appropriation" && x.SourceOfFund == "Internally generated fund" && x.SourceOfFund == "Special intervention fund" && x.SourceOfFund == "Power sector intervention fund" && x.SourceOfFund == "ETF Special intervention fund").ToList();
            //foreach (string str in string1)
            //{
            //    SourceOfFundsOrganizationSettings sourceOfFundsOrganizationSettings = db.SourceOfFundsOrganizationSettings.Find(source.SourceOfFundID);
            //    sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
            //    sourceOfFundsOrganizationSettings.DateModified = dt;
            //    db.Entry(sourceOfFundsOrganizationSettings).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
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
            var UpdateProcurementMethod = db.ProcurementMethod.Select(x => x.Name == "Selective Tendering" && x.Name == "Direct procurement" && x.Name == "Open Competitive method").ToList();
            bool[] DefaultProcurementMethod = new bool[3];
            DefaultProcurementMethod.ToString();
            foreach (ProcurementMethod source in ProcurementMethod)
            {
                ProcurementMethodOrganizationSettings procurementMethodOrganizationSettings = db.ProcurementMethodOrganizationSettings.Find(source.ProcurementMethodID);
                procurementMethodOrganizationSettings.EnableProcurementMethod = true;
                procurementMethodOrganizationSettings.DateModified = dt;
                db.Entry(procurementMethodOrganizationSettings).State = EntityState.Modified;
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
                    EnableProjectCategory = false,
                    DateCreated = dt
                });
                db.SaveChanges();
            }

            var UpdateProjectCategory = db.ProjectCategory.Where(x => x.Name == "Goods" && x.Name == "Services" && x.Name == "Works").ToList();
            foreach (ProjectCategory source in UpdateProjectCategory)
            {
                ProjectCategoryOrganizationSettings projectCategoryOrganizationSettings = db.ProjectCategoryOrganizationSettings.Find(source.ProjectCategoryID);
                projectCategoryOrganizationSettings.EnableProjectCategory = true;
                projectCategoryOrganizationSettings.DateModified = dt;
                db.Entry(projectCategoryOrganizationSettings).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ActionResult test()
        {
            DateTime dt = DateTime.Now;
            List<string> string1 = new List<string>();
            string1.Add("Budgetary allocation/appropriation");
            string1.Add("Internally generated fund");
            string1.Add("Special intervention fund");
            string1.Add("Power sector intervention fund");
            var databaselist = db.SourceOfFunds.Where(x=> x.SourceOfFundID == x.SourceOfFundID).Select(x=> x.SourceOfFund).FirstOrDefault();
            var sourceOfFundID = db.SourceOfFundsOrganizationSettings.Where(x => x.SourceOfFundID == x.SourceOfFundID).Select(x => x.SourceOfFundID).FirstOrDefault();
            foreach (string str in string1)
            {
                for (int i = 0; i < string1.Count(); i++)
                {
                    if (string1[i] == databaselist)
                    {
                        foreach (var source in databaselist)
                        {
                            SourceOfFundsOrganizationSettings sourceOfFundsOrganizationSettings = db.SourceOfFundsOrganizationSettings.Find(sourceOfFundID);
                            sourceOfFundsOrganizationSettings.EnableSourceOFFund = true;
                            sourceOfFundsOrganizationSettings.DateModified = dt;
                            db.Entry(sourceOfFundsOrganizationSettings).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                }
            }
            var result = db.SourceOfFunds.ToList();
            return Json(new
            {
                result = result,
            }, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SourceOfFunds> GetElements(IEnumerable<string> hugeList, int chunkSize = 100)
        {
            List<string> string1 = new List<string>();
            string1.Add("Budgetary allocation/appropriation");
            string1.Add("Internally generated fund");
            string1.Add("Special intervention fund");
            string1.Add("Power sector intervention fund");
            foreach (var chunk in hugeList)
            {
                var q = db.SourceOfFunds.Where(a => chunk.Contains(a.SourceOfFund));
                foreach (var item in q)
                {
                    yield return item;
                }
            }
        }
    }
}

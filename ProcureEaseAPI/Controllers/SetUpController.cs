using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProcureEaseAPI.Controllers
{
    public class SetUpController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        // GET: SetUp
        public ActionResult Index()
        {
            return View();
        }

        //http://localhost:85/SetUp/SourceOfFunds
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SourceOfFunds(SourceOfFunds sourceOfFunds)
        {
            if (ModelState.IsValid)
            {
                var SourceOfFunds = db.SourceOfFunds.Select(x => new
                {
                    x.SourceOfFundID,
                    x.SourceOfFund,
                    x.EnableSourceOfFund
                });
                var AdminDashboard = new
                {
                    sucess = true,
                    message = "All source of funds",
                    data = new
                    {
                        SourceOfFunds = SourceOfFunds
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //http://localhost:85/SetUp/ProcurementMethod
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ProcurementMethod(ProcurementMethod procurementMethod)
        {
            if (ModelState.IsValid)
            {
                var ProcurementMethod = db.ProcurementMethod.Select(x => new
                {
                    x.ProcurementMethodID,
                    x.Name,
                    x.EnableProcurementMethod,
                });
                var AdminDashboard = new
                {
                    sucess = true,
                    message = "All Procurement Method",
                    data = new
                    {
                        ProcurementMethod = ProcurementMethod
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //http://localhost:85/SetUp/ProjectCategory
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ProjectCategory(ProjectCategory projectCategory)
        {
            if (ModelState.IsValid)
            {
                var ProjectCategory = db.ProjectCategory.Select(x => new
                {
                    x.ProjectCategoryID,
                    x.Name,
                    x.EnableProjectCategory,
                });
                var AdminDashboard = new
                {
                    sucess = true,
                    message = "All Project Category",
                    data = new
                    {
                        ProjectCategory = ProjectCategory
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        //http://localhost:85/SetUp/UpdateBasicDetails
        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdateBasicDetails(OrganizationSettings organizationSetting, List<TelephoneNumbers> TelephoneNumber)
        {
            if (ModelState.IsValid)
            {
                DateTime dt = DateTime.Now;
                organizationSetting.OrganizationID = Guid.NewGuid();
                organizationSetting.DateCreated = dt;
                organizationSetting.DateModified = dt;
                organizationSetting.CreatedBy = "Admin";
                db.OrganizationSettings.Add(organizationSetting);
                db.SaveChanges();
                AddTelephone(TelephoneNumber);
                return Json(db.OrganizationSettings.Where(y => y.OrganizationID == organizationSetting.OrganizationID).Select(x => new
                {
                    sucess = true,
                    message = "Organization settings added successfully!!!",
                    data = new
                    {
                        x.OrganizationID,
                        x.OrganizationNameInFull,
                        x.OrganizationNameAbbreviation,
                        x.OrganizationEmail,
                        x.State,
                        x.Country,
                        x.AboutOrganization,
                        x.Address,
                        x.TelephoneNumbers,
                        x.OrganizationLogoPath,
                        x.DateCreated,
                        x.DateModified
                    }
                }).FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public void AddTelephone(List<TelephoneNumbers> telephoneNumbers)
        {
            var OrganizationName = db.OrganizationSettings.Where(y => y.OrganizationNameInFull == y.OrganizationNameInFull).Select(y => y.OrganizationID).FirstOrDefault();
            var organizationID = db.OrganizationSettings.Where(y => y.OrganizationID == OrganizationName).Select(y => y.OrganizationID).FirstOrDefault();
            foreach (var Telephone in telephoneNumbers)
            {
                DateTime dt = DateTime.Now;
                db.TelephoneNumbers.Add(new TelephoneNumbers
                {
                    TelephoneNumberID = Guid.NewGuid(),
                    OrganizationID = organizationID,
                    TelephoneNumber = Telephone.TelephoneNumber,
                    DateCreated = dt,
                    DateModified = dt,
                    CreatedBy = "Admin"
                });
                db.SaveChanges();
            }
        }

    }
}
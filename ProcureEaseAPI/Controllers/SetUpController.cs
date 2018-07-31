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
                    success = true,
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
                    success = true,
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
                    success = true,
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
        public async Task<ActionResult> UpdateBasicDetails([Bind(Include = "OrganizationID,OrganizationNameInFull,OrganizationNameAbbreviation,OrganizationEmail,Address,Country,State,AboutOrganization,DateModified,CreatedBy,DateCreated")]OrganizationSettings organizationSettings, HttpPostedFileBase image, params string[] telephoneNumbers)
        {
            if (ModelState.IsValid)
            {
                DateTime dt = DateTime.Now;
                var currentOrganizationDetails = db.OrganizationSettings.FirstOrDefault(o => o.OrganizationID == o.OrganizationID);

                if (currentOrganizationDetails == null)
                    return HttpNotFound();

                currentOrganizationDetails.OrganizationNameInFull = organizationSettings.OrganizationNameInFull;
                currentOrganizationDetails.OrganizationNameAbbreviation = organizationSettings.OrganizationNameAbbreviation;
                currentOrganizationDetails.OrganizationEmail = organizationSettings.OrganizationEmail;
                currentOrganizationDetails.Address = organizationSettings.Address;
                currentOrganizationDetails.Country = organizationSettings.Country;
                currentOrganizationDetails.State = organizationSettings.State;
                currentOrganizationDetails.AboutOrganization = organizationSettings.AboutOrganization;
                currentOrganizationDetails.DateModified = dt;
                currentOrganizationDetails.CreatedBy = "MDA Administrator";
                if (image != null)
                {
                    currentOrganizationDetails.OrganizationLogoPath = await new FileUploadHelper().UploadImageToAzureStorage(image) + "";
                }
                db.SaveChanges();
                if (telephoneNumbers != null && telephoneNumbers.Length > 0)
                {
                    AddTelephone(organizationSettings, telephoneNumbers);
                }
                var BasicDetails = db.OrganizationSettings.Select(x => new
                {
                    x.OrganizationID,
                    x.OrganizationNameInFull,
                    x.OrganizationNameAbbreviation,
                    x.OrganizationEmail,
                    x.State,
                    x.Country,
                    x.AboutOrganization,
                    x.Address,
                    x.OrganizationLogoPath,
                    DateModified = x.DateModified.Value.ToString(),
                    x.CreatedBy,
                    TelephoneNumbers = db.TelephoneNumbers.Where(y => y.OrganizationID == x.OrganizationID).Select(y => new
                    {
                        y.TelephoneNumber
                    })
                });
                var AdminDashboard = new
                {
                    success = true,
                    message = "Basic details added successfully!!!",
                    data = new
                    {
                        BasicDetails = BasicDetails,
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        public void AddTelephone(OrganizationSettings organizationSettings, params string[] telephoneNumbers)
        {
            // TODO: Anita. check if this telephone number has already been added for this organization    
            var TelephoneNumbersFromDB = db.TelephoneNumbers.Select(x => x.TelephoneNumber).ToList();
            var DistinctTelephoneNumbers =  telephoneNumbers.Except(TelephoneNumbersFromDB);
            foreach (var telephone in DistinctTelephoneNumbers)
            {
                    DateTime dt = DateTime.Now;               
                    db.TelephoneNumbers.Add(new TelephoneNumbers
                    {
                        TelephoneNumberID = Guid.NewGuid(),
                        OrganizationID = organizationSettings.OrganizationID,
                        TelephoneNumber = telephone,
                        DateCreated = dt,
                        DateModified = dt,
                        CreatedBy = "Admin"
                    });
                    db.SaveChanges();
               
            }
        }
        //http://localhost:85/SetUp/OrganizationSettings
        [AllowAnonymous]
        [HttpGet]
        public ActionResult OrganizationSettings(OrganizationSettings OrganizationSettings)
        {
            if (ModelState.IsValid)
            {
                var BasicDetails = db.OrganizationSettings.Select(x => new
                {
                    x.OrganizationID,
                    x.OrganizationEmail,
                    x.OrganizationNameInFull,
                    x.OrganizationNameAbbreviation,
                    x.State,
                    x.Country,
                    x.AboutOrganization,
                    x.OrganizationLogoPath,
                    x.CreatedBy,
                    TelephoneNumbers = db.TelephoneNumbers.Where(y => y.OrganizationID == x.OrganizationID).Select(y => new
                    {
                        y.TelephoneNumber
                    })

                });

                var DepartmentSetup = db.Department.Select(x => new
                {
                    Department = db.Department.Where(y => y.DepartmentID == x.DepartmentID).Select(y => new
                    {
                        y.DepartmentID,
                        y.DepartmentName
                    }),

                    Head = db.UserProfile.Where(y => x.DepartmentID == y.DepartmentID).Select(y => new
                    {
                        x.DepartmentHeadUserID,
                        FullName = y.FirstName + " " + y.LastName
                    })
                });

                var UserManagement = db.UserProfile.Select(x => new
                {
                    User = db.UserProfile.Where(y => y.UserID == x.UserID).Select(y => new
                    {
                        y.UserID,
                        FullName = x.FirstName + " " + x.LastName
                    }),
                    Department = db.UserProfile.Where(y => y.UserID == x.UserID).Select(y => new
                    {
                        x.DepartmentID,
                        y.Department1.DepartmentName
                    })
                });

                var SourceOfFunds = db.SourceOfFunds.Select(x => new
                {
                    x.SourceOfFundID,
                    x.SourceOfFund,
                    x.EnableSourceOfFund,
                });

                var ProcurementMethod = db.ProcurementMethod.Select(x => new
                {
                    x.ProcurementMethodID,
                    x.Name,
                    x.EnableProcurementMethod,
                });

                var ProjectCategory = db.ProjectCategory.Select(x => new
                {
                    x.ProjectCategoryID,
                    x.Name,
                    x.EnableProjectCategory,
                });

                var Users = db.UserProfile.Select(x => new
                {
                    x.UserID,
                    FullName = x.FirstName + " " + x.LastName
                });

                var Departments = db.Department.Select(x => new
                {
                    x.DepartmentID,
                    x.DepartmentName
                });

                var AdminDashboard = new
                {
                    success = true,
                    message = "OK",
                    data = new
                    {
                        BasicDetails = BasicDetails,
                        DepartmentSetup = DepartmentSetup,
                        UserManagement = UserManagement,
                        SourceOfFunds = SourceOfFunds,
                        ProcurementMethod = ProcurementMethod,
                        ProjectCategory = ProjectCategory,
                        Users = Users,
                        Departments = Departments
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}
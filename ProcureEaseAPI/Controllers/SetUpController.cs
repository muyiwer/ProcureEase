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
        private CatalogsController catalog = new CatalogsController();
        // GET: SetUp
        public ActionResult Index()
        {
            return View();
        }

        // GET: SetUp/SourceOfFunds
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SourceOfFunds()
        {
            try
            {
                return Json(new
                {
                    success = true,
                    message = "All source of funds",
                    data = db.SourceOfFunds.Select(x => new
                    {
                        x.SourceOfFundID,
                        x.SourceOfFund,
                        Enabled = db.SourceOfFundsOrganizationSettings.Where(y => y.TenantID == y.SourceOfFundID).Select(y => y.EnableSourceOFFund)
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ALL_SOURCEOFFUNDS, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: SetUp/ProcurementMethod
        [HttpGet]
        public ActionResult ProcurementMethod()
        {
            try
            {
                return Json(new
                {
                    success = true,
                    message = "All Procurement Method",
                    data = db.ProcurementMethodOrganizationSettings.Select(x => new
                    {
                        x.ProcurementMethodID,
                        x.ProcurementMethod.Name,
                        x.EnableProcurementMethod,
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ALL_PROCUREMENTMETHOD, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: SetUp/ProjectCategory
        [HttpGet]
        public ActionResult ProjectCategory()
        {
            try
            {
                return Json(new
                {
                    success = true,
                    message = "All Project Category",
                    data = db.ProjectCategoryOrganizationSettings.Select(x => new
                    {
                        x.ProjectCategoryID,
                        x.ProjectCategory.Name,
                        x.EnableProjectCategory,
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ALL_PROJECTCATEGORY, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // PUT: SetUp/UpdateBasicDetails
        [HttpPut]
        public async Task<ActionResult> UpdateBasicDetails([Bind(Include = "OrganizationID,OrganizationNameInFull,OrganizationNameAbbreviation,OrganizationEmail,Address,Country,State,AboutOrganization,DateModified,CreatedBy,DateCreated")]OrganizationSettings organizationSettings, HttpPostedFileBase image, params string[] telephoneNumbers)
        {
            try
            {
                var tenantID = catalog.GetTenantID();
                DateTime dt = DateTime.Now;
                var currentOrganizationDetails = db.OrganizationSettings.FirstOrDefault(o => o.OrganizationID == o.OrganizationID);
                var Organization = db.OrganizationSettings.Where(x => x.TenantID == tenantID).Select(x => x.OrganizationNameInFull).FirstOrDefault();
                if (currentOrganizationDetails == null && tenantID == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_BASICDETAILS, "OrgasnizationID not found");
                    return Json(new
                    {
                        success = false,
                        message = "OrgasnizationID not found",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                if (tenantID == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_BASICDETAILS, "TenantID is null");
                    return Json(new
                    {
                        success = false,
                        message = "TenantID is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }

                currentOrganizationDetails.TenantID = tenantID;
                currentOrganizationDetails.OrganizationNameInFull = organizationSettings.OrganizationNameInFull;
                currentOrganizationDetails.OrganizationNameAbbreviation = organizationSettings.OrganizationNameAbbreviation;
                currentOrganizationDetails.OrganizationEmail = organizationSettings.OrganizationEmail;
                currentOrganizationDetails.Address = organizationSettings.Address;
                currentOrganizationDetails.Country = organizationSettings.Country;
                currentOrganizationDetails.State = organizationSettings.State;
                currentOrganizationDetails.AboutOrganization = organizationSettings.AboutOrganization;
                currentOrganizationDetails.DateModified = dt;
                currentOrganizationDetails.CreatedBy = Organization;
                if (image != null)
                {
                    currentOrganizationDetails.OrganizationLogoPath = await new FileUploadHelper().UploadImageToAzureStorage(image) + "";
                }
                db.SaveChanges();
                if (telephoneNumbers != null && telephoneNumbers.Length > 0)
                {
                    AddTelephone(organizationSettings, telephoneNumbers);
                }
                return Json(new
                {
                    success = true,
                    message = "Basic details added successfully!!!",
                    data = db.OrganizationSettings.Where(x => x.OrganizationID == organizationSettings.OrganizationID).Select(x => new
                    {
                        TenantID = db.OrganizationSettings.Where(y => y.TenantID == tenantID).Select(y => y.TenantID).FirstOrDefault(),
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
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_BASICDETAILS, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public void AddTelephone(OrganizationSettings organizationSettings, params string[] telephoneNumbers)
        {
            // TODO: Anita. check if this telephone number has already been added for this organization
            var tenantID = catalog.GetTenantID();
            var TelephoneNumbersFromDB = db.TelephoneNumbers.Select(x => x.TelephoneNumber).ToList();
            var DistinctTelephoneNumbers = telephoneNumbers.Except(TelephoneNumbersFromDB);
            foreach (var telephone in DistinctTelephoneNumbers)
            {
                DateTime dt = DateTime.Now;
                db.TelephoneNumbers.Add(new TelephoneNumbers
                {
                    TelephoneNumberID = Guid.NewGuid(),
                    TenantID = tenantID,
                    OrganizationID = organizationSettings.OrganizationID,
                    TelephoneNumber = telephone,
                    DateCreated = dt,
                    DateModified = dt,
                    CreatedBy = "Admin"
                });
                db.SaveChanges();

            }
        }

        // GET: SetUp/OrganizationSettings
        [HttpGet]
        public ActionResult OrganizationSettings()
        {
            try
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
                   // x.EnableSourceOfFund,
                });

                var ProcurementMethod = db.ProcurementMethodOrganizationSettings.Select(x => new
                {
                    x.ProcurementMethodID,
                    x.ProcurementMethod.Name,
                    x.EnableProcurementMethod,
                });

                var ProjectCategory = db.ProjectCategory.Select(x => new
                {
                    x.ProjectCategoryID,
                    x.Name,
                   // x.EnableProjectCategory,
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

                return Json(new
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
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ORGANIZATIONSETTINGS, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
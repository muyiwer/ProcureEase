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

        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult ExceptionError(string message, string StackTrace)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { InternalError = StackTrace }
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: SetUp/SourceOfFunds
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SourceOfFunds()
        {
            Guid? tenantId = catalog.GetTenantID();
            try
            {
                if (tenantId == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }       
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ALL_SOURCEOFFUNDS, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "All source of funds",
                data = db.SourceOfFundsOrganizationSettings.Where(y => y.TenantID == tenantId).Select(x => new
                {
                    x.SourceOfFundID,
                    x.SourceOfFunds.SourceOfFund,
                    Enabled = x.EnableSourceOFFund
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: SetUp/ProcurementMethod
        [HttpGet]
        public ActionResult ProcurementMethod()
        {
            Guid? tenantId = catalog.GetTenantID();
            try
            {
                if (tenantId == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }        
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ALL_PROCUREMENTMETHOD, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "All Procurement Method",
                data = db.ProcurementMethodOrganizationSettings.Where(y => y.TenantID == tenantId).Select(x => new
                {
                    x.ProcurementMethodID,
                    x.ProcurementMethod.Name,
                    x.EnableProcurementMethod,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: SetUp/ProjectCategory
        [HttpGet]
        public ActionResult ProjectCategory()
        {
            Guid? tenantId = catalog.GetTenantID();
            try
            {
                if (tenantId == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ALL_PROJECTCATEGORY, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "All Project Category",
                data = db.ProjectCategoryOrganizationSettings.Where(x=> x.TenantID == tenantId).Select(x => new
                {
                    x.ProjectCategoryID,
                    x.ProjectCategory.Name,
                    x.EnableProjectCategory,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // PUT: SetUp/UpdateBasicDetails
        [HttpPut]
        public async Task<ActionResult> UpdateBasicDetails(OrganizationSettings organizationSettings, HttpPostedFileBase image, params string[] TelephoneNumbers)
        {
            Guid? tenantId = catalog.GetTenantID();
            var OrganizationSettingsTenantID = db.OrganizationSettings.Where(x => x.OrganizationID == organizationSettings.OrganizationID).Select(x => x.TenantID).FirstOrDefault();
            try
            {
                if (OrganizationSettingsTenantID != tenantId)
                {
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                DateTime dt = DateTime.Now;
                var currentOrganizationDetails = db.OrganizationSettings.FirstOrDefault(o => o.OrganizationID == o.OrganizationID);
                var Organization = db.OrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => x.OrganizationNameInFull).FirstOrDefault();
                if (currentOrganizationDetails == null && tenantId == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_BASICDETAILS, "OrgasnizationID not found");
                    return Json(new
                    {
                        success = false,
                        message = "OrgasnizationID not found",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                currentOrganizationDetails.TenantID = tenantId;
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
                if (TelephoneNumbers != null && TelephoneNumbers.Length > 0 )
                {
                    AddTelephone(organizationSettings, TelephoneNumbers);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_BASICDETAILS, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "Basic details added successfully!!!",
                data = db.OrganizationSettings.Where(x => x.OrganizationID == organizationSettings.OrganizationID).Select(x => new
                {
                    TenantID = db.OrganizationSettings.Where(y => y.TenantID == tenantId).Select(y => y.TenantID).FirstOrDefault(),
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
                    TelephoneNumbers = db.TelephoneNumbers.Where(y => y.OrganizationID == x.OrganizationID).Select(y => y.TelephoneNumber)
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public void AddTelephone(OrganizationSettings organizationSettings, params string[] TelephoneNumbers)
        {
            // TODO: Anita. check if this telephone number has already been added for this organization
            var tenantID = catalog.GetTenantID();
            var TelephoneNumbersFromDB = db.TelephoneNumbers.Select(x => x.TelephoneNumber).ToList();
            var DistinctTelephoneNumbers = TelephoneNumbers.Except(TelephoneNumbersFromDB);
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
            Guid? tenantId = catalog.GetTenantID();
            try
            {
                if (tenantId == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ORGANIZATIONSETTINGS, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            var BasicDetails = db.OrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
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
                TelephoneNumbers = db.TelephoneNumbers.Where(y => y.OrganizationID == x.OrganizationID).Select(y => y.TelephoneNumber)
            });

            var DepartmentSetup = db.Department.Select(x => new
            {
                Department = db.Department.Where(y => y.TenantID == tenantId).Select(y => new
                {
                    y.DepartmentID,
                    y.DepartmentName
                }),

                Head = db.UserProfile.Where(y =>x.DepartmentID == x.DepartmentID && x.TenantID == tenantId).Select(y => new
                {
                    x.DepartmentHeadUserID,
                    FullName = y.FirstName + " " + y.LastName
                })
            });

            var UserManagement = db.UserProfile.Select(x => new
            {
                User = db.UserProfile.Where(y => y.TenantID == tenantId && x.UserID == x.UserID).Select(y => new
                {
                    y.UserID,
                    FullName = x.FirstName + " " + x.LastName,
                }),
                Department = db.UserProfile.Where(z => z.UserID == x.UserID && z.TenantID == tenantId).Select(z => new
                {
                    x.DepartmentID,
                    z.Department1.DepartmentName
                })

            });

            var SourceOfFunds = db.SourceOfFundsOrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
            {
                x.SourceOfFundID,
                x.SourceOfFunds.SourceOfFund,
                x.EnableSourceOFFund,
            });

            var ProcurementMethod = db.ProcurementMethodOrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
            {
                x.ProcurementMethodID,
                x.ProcurementMethod.Name,
                x.EnableProcurementMethod,
            });

            var ProjectCategory = db.ProjectCategoryOrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
            {
                x.ProjectCategoryID,
                x.ProjectCategory.Name,
                x.EnableProjectCategory,
            });

            var Users = db.UserProfile.Where(x => x.TenantID == tenantId).Select(x => new
            {
                x.UserID,
                FullName = x.FirstName + " " + x.LastName
            });

            var Departments = db.Department.Where(x => x.TenantID == tenantId).Select(x => new
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
    }
}
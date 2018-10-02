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

        //[Providers.Authorize]
        public ActionResult SourceOfFunds()
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                    Name = x.SourceOfFunds.SourceOfFund,
                    Enabled = x.EnableSourceOFFund
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // PUT: SetUp/UpdateSourceOfFunds
        [HttpPut]
        public ActionResult UpdateSourceOfFunds(List<SourceOfFundsOrganizationSettings> sourceOfFunds)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                DateTime dt = DateTime.Now;

                foreach (SourceOfFundsOrganizationSettings sourceOfFundsOrganizationSettings in sourceOfFunds)
                {
                    var CurrentSourceOfFundID = db.SourceOfFundsOrganizationSettings.FirstOrDefault(s => s.SourceOfFundID == sourceOfFundsOrganizationSettings.SourceOfFundID && s.TenantID == tenantId);

                    if (CurrentSourceOfFundID == null)
                    {
                        LogHelper.Log(Log.Event.UPDATE_SOURCEOFFUNDS, "SourceOfFundID not found");
                        return Json(new
                        {
                            success = false,
                            message = "SourceOfFundID not found",
                            data = new { }
                        }, JsonRequestBehavior.AllowGet);
                    }
                    CurrentSourceOfFundID.SourceOfFundID = sourceOfFundsOrganizationSettings.SourceOfFundID;
                    CurrentSourceOfFundID.EnableSourceOFFund = sourceOfFundsOrganizationSettings.EnableSourceOFFund;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_SOURCEOFFUNDS, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "Edited successfully",
                data = db.SourceOfFundsOrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
                {
                    x.SourceOfFundID,
                    Name = x.SourceOfFunds.SourceOfFund,
                    Enabled = x.EnableSourceOFFund
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [Providers.Authorize]
        public ActionResult ProcurementMethod()
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                    Enabled = x.EnableProcurementMethod,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // PUT: SetUp/UpdateProcurementMethod
        [HttpPut]
        public ActionResult UpdateProcurementMethod(List<ProcurementMethodOrganizationSettings> procurementMethod)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                DateTime dt = DateTime.Now;
                foreach (ProcurementMethodOrganizationSettings procurementMethodOrganizationSettings in procurementMethod)
                {
                    var CurrentProcurementMethod = db.ProcurementMethodOrganizationSettings.FirstOrDefault(s => s.ProcurementMethodID == procurementMethodOrganizationSettings.ProcurementMethodID && s.TenantID == tenantId);

                    if (CurrentProcurementMethod == null)
                    {
                        LogHelper.Log(Log.Event.UPDATE_SOURCEOFFUNDS, "ProcurementID not found");
                        return Json(new
                        {
                            success = false,
                            message = "ProcurementID not found",
                            data = new { }
                        }, JsonRequestBehavior.AllowGet);
                    }
                    CurrentProcurementMethod.ProcurementMethodID = procurementMethodOrganizationSettings.ProcurementMethodID;
                    CurrentProcurementMethod.EnableProcurementMethod = procurementMethodOrganizationSettings.EnableProcurementMethod;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_PROCUREMENTMETHOD, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "Editted successfully!!!",
                data = db.ProcurementMethodOrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
                {
                    x.ProcurementMethodID,
                    x.ProcurementMethod.Name,
                    Enabled = x.EnableProcurementMethod,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [Providers.Authorize]
        public ActionResult ProjectCategory()
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                    Enabled =x.EnableProjectCategory,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // PUT: SetUp/UpdateProjectCategory
        [HttpPut]
        public ActionResult UpdateProjectCategory(List<ProjectCategoryOrganizationSettings> projectCategory)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                DateTime dt = DateTime.Now;
                foreach (ProjectCategoryOrganizationSettings projectCategoryOrganizationSettings in projectCategory)
                {
                    var CurrentProjectCategory = db.ProjectCategoryOrganizationSettings.Where(s => s.ProjectCategoryID == projectCategoryOrganizationSettings.ProjectCategoryID && s.TenantID == tenantId).FirstOrDefault();

                    if (CurrentProjectCategory == null)
                    {
                        LogHelper.Log(Log.Event.UPDATE_SOURCEOFFUNDS, "ProjectCategoryID not found");
                        return Json(new
                        {
                            success = false,
                            message = "ProjectCategoryID not found",
                            data = new { }
                        }, JsonRequestBehavior.AllowGet);
                    }
                    CurrentProjectCategory.ProjectCategoryID = projectCategoryOrganizationSettings.ProjectCategoryID;
                    CurrentProjectCategory.EnableProjectCategory = projectCategoryOrganizationSettings.EnableProjectCategory;
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_PROJECTCATEGORY, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "Edited successfully",
                data = db.ProjectCategoryOrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
                {
                    x.ProjectCategoryID,
                    x.ProjectCategory.Name,
                    Enabled = x.EnableProjectCategory
                })
            }, JsonRequestBehavior.AllowGet);
        }
        [Providers.Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateBasicDetails(OrganizationSettings organizationSettings, HttpPostedFileBase image, params string[] TelephoneNumbers)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
            var TelephoneNumbersFromDB = db.TelephoneNumbers.Select(x => x.TelephoneNumber).ToList();
            var DistinctTelephoneNumbers = TelephoneNumbers.Except(TelephoneNumbersFromDB);
            foreach (var telephone in DistinctTelephoneNumbers)
            {
                DateTime dt = DateTime.Now;
                db.TelephoneNumbers.Add(new TelephoneNumbers
                {
                    TelephoneNumberID = Guid.NewGuid(),
                    TenantID = tenantId,
                    OrganizationID = organizationSettings.OrganizationID,
                    TelephoneNumber = telephone,
                    DateCreated = dt,
                    DateModified = dt,
                    CreatedBy = "Admin"
                });
                db.SaveChanges();

            }
        }

       // [Providers.Authorize]
        public ActionResult OrganizationSettings()
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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

            return Json(new
            {
                success = true,
                message = "OK",
                data =  new
                {
                    BasicDetails = db.OrganizationSettings.Where(x => x.TenantID == tenantId).Select(x => new
                    {
                        x.OrganizationID,
                        x.OrganizationEmail,
                        OrganizationName = x.OrganizationNameInFull,
                        x.OrganizationNameAbbreviation,
                        x.Address,
                        x.State,
                        x.Country,
                        x.AboutOrganization,
                        x.OrganizationLogoPath,
                        x.CreatedBy,
                        TelephoneNumbers = db.TelephoneNumbers.Where(y => y.OrganizationID == x.OrganizationID).Select(y => y.TelephoneNumber)
                    }).FirstOrDefault(),
                    DepartmentSetUp = db.Department.Where(y => y.OrganisationID == y.OrganizationSettings.OrganizationID).Select(y => new
                    {
                        Department = new
                        {
                            y.DepartmentID,
                            y.DepartmentName
                        },
                        Head = new
                        {
                            DepartmentHeadUserID = db.UserProfile.Where(z => y.DepartmentHeadUserID == z.UserID).Select(z => (true) || (false)).FirstOrDefault(),
                            FullName = db.UserProfile.Where(z => z.UserID == y.DepartmentHeadUserID).Select(z => z.FirstName + " " + z.LastName).FirstOrDefault()
                        }
                    }),
                    UserManagement = db.UserProfile.Where(y => y.OrganizationID == y.OrganizationSettings.OrganizationID && y.UserID == y.UserID).Select(y => new
                    {
                        User = new
                        {
                            y.UserID,
                            FullName = y.FirstName + " " + y.LastName
                        },
                        Department = new
                        {
                            y.DepartmentID,
                            y.Department1.DepartmentName
                        }

                    }),
                    SourceOfFunds = db.SourceOfFundsOrganizationSettings.Where(y => y.OrganizationID == y.OrganizationSettings.OrganizationID).Select(y => new
                    {
                        y.SourceOfFundID,
                        Name = y.SourceOfFunds.SourceOfFund,
                        Enabled = y.EnableSourceOFFund,
                    }),
                    ProcurementMethod = db.ProcurementMethodOrganizationSettings.Where(y => y.OrganizationID == y.OrganizationSettings.OrganizationID).Select(y => new
                    {
                        y.ProcurementMethodID,
                        y.ProcurementMethod.Name,
                        Enabled = y.EnableProcurementMethod,
                    }),
                    ProjectCategory = db.ProjectCategoryOrganizationSettings.Where(y => y.OrganizationID == y.OrganizationSettings.OrganizationID).Select(y => new
                    {
                        y.ProjectCategoryID,
                        y.ProjectCategory.Name,
                        Enabled = y.EnableProjectCategory,
                    }),
                    Users = db.UserProfile.Where(y => y.OrganizationID == y.OrganizationSettings.OrganizationID).Select(y => new
                    {
                        y.UserID,
                        FullName = y.FirstName + " " + y.LastName
                    }),
                    Departments = db.Department.Where(y => y.OrganisationID == y.OrganizationSettings.OrganizationID).Select(y => new
                    {
                        y. DepartmentID,
                        y.DepartmentName
                    })
                }
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
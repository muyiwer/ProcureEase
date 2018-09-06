using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcureEaseAPI.Models;
using System.Configuration;
using Utilities;

namespace ProcureEaseAPI.Controllers
{
    public class AdvertsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: Adverts
        public ActionResult Index()
        {
            var adverts = db.Adverts.Include(a => a.AdvertStatus).Include(a => a.BudgetYear);
            return View(adverts.ToList());
        }

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

        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult SentAdvertCategoryJson(AdvertCategory advertCategory)
        {
            return Json(new
            {
                success = true,
                message = "Ok",
                data = db.AdvertCategory.Select(x => new
                {
                    x.AdvertCategoryID,
                    x.AdvertCategory1,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // Post: Adverts/AddAdvertCategory
        [HttpPost]
        public ActionResult AddAdvertCategory(AdvertCategory advertCategory)
        {
            DateTime dt = DateTime.Now;
            try
            {
                advertCategory.AdvertCategoryID = Guid.NewGuid();
                advertCategory.CreatedBy = "MDA Administrator";
                advertCategory.DateCreated = dt;
                advertCategory.DateModified = dt;
                db.AdvertCategory.Add(advertCategory);
                db.SaveChanges();
                return SentAdvertCategoryJson(advertCategory);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_ADVERTCATEGORY, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // PUT: Adverts/EditAdvertCategory/5
        [HttpPut]
        public ActionResult EditAdvertCategory(AdvertCategory advertCategory)
        {
            try
            {
                DateTime dt = DateTime.Now;
                AdvertCategory EditAdvertCategoryData = db.AdvertCategory.SingleOrDefault(x => x.AdvertCategoryID == advertCategory.AdvertCategoryID);
                EditAdvertCategoryData.AdvertCategory1 = advertCategory.AdvertCategory1;
                EditAdvertCategoryData.DateModified = dt;
                db.SaveChanges();
                return SentAdvertCategoryJson(advertCategory);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.EDIT_ADVERTCATEGORY, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Adverts/DeleteAdvertCategory/5 
        [HttpPost, ActionName("DeleteAdvertCategory")]
        public ActionResult DeleteAdvertCategory(Guid id)
        {
            AdvertCategory advertCategory = new AdvertCategory();
            try
            {
                advertCategory = db.AdvertCategory.Find(id);
                db.AdvertCategory.Remove(advertCategory);
                db.SaveChanges();
                return SentAdvertCategoryJson(advertCategory);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.DELETE_ADVERTCATEGORY, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        private ActionResult SentAdvertJson(int BudgetYear, Guid DepartmentID)
        {
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("AttestedProcurementStatusID"), out ProcurementStatusID);

                return Json(new
                {
                    success = true,
                    Message = "All sent procurement.",
                    data = new
                    {
                        DepartmentName = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault(),
                        BudgetYearID = db.BudgetYear.Where(x => x.BudgetYearID == x.BudgetYearID).Select(x => x.BudgetYearID).FirstOrDefault(),
                        BudgetYear = db.BudgetYear.Where(x => x.BudgetYear1.Value.Year == BudgetYear).Select(x => x.BudgetYear1.Value.Year).FirstOrDefault(),
                        TotalCost = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == z.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.UnitPrice).Sum()
                                         * db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == z.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.Quantity).Sum(),
                        Projects = db.Procurements.Where(x => x.DepartmentID == DepartmentID && x.BudgetYear.BudgetYear1.Value.Year == BudgetYear && x.ProcurementStatusID == ProcurementStatusID).Select(x => new
                        {
                            x.ProcurementID,
                            x.ProjectName,
                            x.DateCreated,
                            x.ProcurementMethodID,
                            x.ProjectCategoryID,
                            x.ProcurementStatusID,
                            TotalCost = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == z.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.UnitPrice).Sum()
                                         * db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == z.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.Quantity).Sum(),
                            ProjectTotalCostStatus = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(y => y.UnitPrice != null == true || false).FirstOrDefault(),
                            Items = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => new
                            {
                                z.ItemID,
                                z.ItemName,
                                z.ItemCodeID,
                                ItemCode = z.ItemCode.ItemCode1,
                                z.Quantity,
                                z.UnitPrice,
                                EstimatedCost = z.UnitPrice * z.Quantity
                            })
                        }),
                        ProcureMentStatus = db.ProcurementStatus.Select(x => new
                        {
                            x.ProcurementStatusID,
                            x.Status
                        }),
                        ProcureMentMethod = db.ProcurementMethod.Select(x => new
                        {
                            x.ProcurementMethodID,
                            x.Name
                        }),
                        ProjectCategory = db.ProjectCategory.Select(x => new
                        {
                            x.ProjectCategoryID,
                            x.Name
                        }),
                        Advert = db.Adverts.Select(x => new
                        {
                            x.AdvertID,
                            x.Headline
                        })
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ADDED_PLAN_ON_ADVERT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PutAdvertJson()
        {
            try
            {
                AdvertCategory AdvertCategory = new AdvertCategory();
                return Json(new
                {
                    success = true,
                    message = "Advert details",
                    data = new
                    {
                        Adverts = db.Adverts.Where(x => x.AdvertID == x.AdvertID).Select(x => new
                        {
                            x.AdvertID,
                            x.AdvertStatusID,
                            x.Headline,
                            x.Introduction,
                            x.ScopeOfWork,
                            x.EligibiltyRequirement,
                            x.CollectionOfTenderDocument,
                            x.BidSubmission,
                            x.OtherImportantInformation,
                            BidOpeningDate = x.BidOpeningDate.Value.ToString(),
                            BidClosingDate = x.BidClosingDate.Value.ToString(),
                            DepartmentName = db.AdvertLotNumber.Where(y => y.AdvertID == x.AdvertID).Select(y => y.Procurements.Department.DepartmentName).FirstOrDefault(),
                            BudgetYear = db.BudgetYear.Where(y => y.BudgetYearID == x.BudgetYearID).Select(y => y.BudgetYear1.Value.Year).FirstOrDefault(),
                            AdvertCategory = db.AdvertLotNumber.Where(z => z.AdvertID == x.AdvertID).Select(z => new
                            {
                                z.ProcurementID,
                                ProjectCategoryID = db.Procurements.Where(a => a.ProcurementID == z.ProcurementID).Select(a => a.ProjectCategoryID).FirstOrDefault(),
                                CategoryName = db.Procurements.Where(a => a.ProcurementID == z.ProcurementID).Select(a => a.ProjectCategory.Name).FirstOrDefault(),
                                CategoryLotCode = db.AdvertCategoryNumber.Where(a => a.CategoryLotCode == z.LotCode).Select(a => a.CategoryLotCode).FirstOrDefault(),
                                z.Procurements.ProjectName,
                                Deleted = false
                            })
                        })
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_ADVERT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }
        // Post: Adverts/AddAdvert
        [HttpPost]
        public ActionResult AddAdvert(int BudgetYear, string Headline, string DepartmentName)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var DepartmentID = db.Department.Where(x => x.DepartmentName == DepartmentName).Select(x => x.DepartmentID).FirstOrDefault();
                Adverts adverts = new Adverts();
                var BudgetYearId = db.BudgetYear.Where(x => x.BudgetYear1.Value.Year == BudgetYear).Select(x => x.BudgetYearID).FirstOrDefault();
                int AdvertStatusID = 0;
                int.TryParse(GetConfiguration("DraftAdvertStatusID"), out AdvertStatusID);
                var checkIfBudgetYearExists = db.BudgetYear.Where(x => x.BudgetYearID == x.BudgetYearID).Select(x => x.BudgetYearID).FirstOrDefault();
                if (checkIfBudgetYearExists == null)
                {
                    LogHelper.Log(Log.Event.ADD_ADVERT_TO_DRAFT, " Budget Year does not exist.");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Budget Year does not exist.");
                }
                adverts.AdvertID = Guid.NewGuid();
                adverts.BudgetYearID = BudgetYearId;
                adverts.AdvertStatusID = AdvertStatusID;
                adverts.Headline = Headline;
                adverts.DateCreated = dt;
                adverts.CreatedBy = "Employee";

                db.Adverts.Add(adverts);
                db.SaveChanges();
                return SentAdvertJson(BudgetYear, DepartmentID);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_ADVERT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Adverts/AddPlanToAdvert
        [HttpPost]
        public ActionResult AddPlanToAdvert(int BudgetYear, string DepartmentName, List<AdvertPreparation>Adverts)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var DepartmentID = db.Department.Where(x => x.DepartmentName == DepartmentName).Select(x => x.DepartmentID).FirstOrDefault();
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("AttestedProcurementStatusID"), out ProcurementStatusID);
                foreach (AdvertPreparation Advertised in Adverts)
                {
                    var departmentalAdvert = Advertised.Projects;
                    if (Advertised.Headline == null)
                    {
                        LogHelper.Log(Log.Event.ADD_PROCUREMENT_PLAN_TO_ADVERT, "Please input Headline.");
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Error(" Please input Headline.");                      
                    }
                    Adverts UpdateAdvert = db.Adverts.Find(Advertised.AdvertID);
                    UpdateAdvert.Headline = Advertised.Headline;
                    UpdateAdvert.DateModified = dt;
                    db.Entry(UpdateAdvert).State = EntityState.Modified;

                    foreach (AdvertProject departmentAdvert in departmentalAdvert)
                    {
                        var getProjectCategoryID = db.ProjectCategory.Where(x => x.ProjectCategoryID == departmentAdvert.ProjectCategoryID).Select(x => x.ProjectCategoryID).FirstOrDefault();
                        var getProjectCategory = db.ProjectCategory.Where(x => x.ProjectCategoryID == departmentAdvert.ProjectCategoryID).Select(x => x.Name).FirstOrDefault();
                        var getAdvertCategoryID = db.AdvertCategory.Where(x => x.AdvertCategory1 == getProjectCategory).Select(x => x.AdvertCategoryID).FirstOrDefault();
                        if (Advertised.AdvertID == Guid.Empty && departmentAdvert.ProcurementID == Guid.Empty)
                        {
                            LogHelper.Log(Log.Event.ADD_PROCUREMENT_PLAN_TO_ADVERT,  "Advert OR Procurement ID could not be found.");
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Error("Advert OR Procurement ID could not be found.");
                        }
                        var checkIfProjectIsAttested = db.Procurements.Where(x => x.ProcurementID == departmentAdvert.ProcurementID).Select(x => x.ProcurementStatusID).FirstOrDefault();
                        if (checkIfProjectIsAttested != ProcurementStatusID)
                        {
                            LogHelper.Log(Log.Event.ADD_PROCUREMENT_PLAN_TO_ADVERT, "Project with ID: " + departmentAdvert.ProcurementID + "Project has not been approved by Head of Procurement.");
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Error("Project with ID: " + departmentAdvert.ProcurementID + " has not been approved by Head of Procurement.");
                        }
                        var checkIfProcurementAlreadyExistsOnAdvert = db.AdvertLotNumber.Where(x => x.ProcurementID == departmentAdvert.ProcurementID).Select(x => x.ProcurementID).FirstOrDefault();
                        if (checkIfProcurementAlreadyExistsOnAdvert == null)
                        {
                            AdvertLotNumber advertLotNumber = new AdvertLotNumber();
                            advertLotNumber.AdvertLotNumberID = Guid.NewGuid();
                            advertLotNumber.AdvertID = Advertised.AdvertID;
                            advertLotNumber.ProcurementID = departmentAdvert.ProcurementID;
                            advertLotNumber.DateCreated = dt;
                            advertLotNumber.DateModified = dt;
                            advertLotNumber.CreatedBy = "Employee";
                            db.AdvertLotNumber.Add(advertLotNumber);

                            AdvertCategoryNumber advertCategoryNumber = new AdvertCategoryNumber();
                            advertCategoryNumber.AdvertCategoryNumberID = Guid.NewGuid();
                            advertCategoryNumber.ProjectCategoryID = getProjectCategoryID;
                            advertCategoryNumber.AdvertID = Advertised.AdvertID;
                            advertCategoryNumber.AdvertCategoryID = getAdvertCategoryID;
                            advertCategoryNumber.DateCreated = dt;
                            advertCategoryNumber.DateModified = dt;
                            advertCategoryNumber.CreatedBy = "Employee";
                            db.AdvertCategoryNumber.Add(advertCategoryNumber);
                            db.SaveChanges();
                        }
                        else
                        {
                            var getAdvertLotNumberID = db.AdvertLotNumber.Where(x => x.ProcurementID == departmentAdvert.ProcurementID).Select(x => x.AdvertLotNumberID).FirstOrDefault();
                            AdvertLotNumber advertLotNumber = db.AdvertLotNumber.Find(getAdvertLotNumberID);
                            advertLotNumber.DateModified = dt;
                            db.Entry(advertLotNumber).State = EntityState.Modified;

                            var getAdvertCategoryNumberID = db.AdvertCategoryNumber.Where(x => x.ProjectCategoryID == departmentAdvert.ProjectCategoryID).Select(x => x.AdvertCategoryNumberID).FirstOrDefault();
                            AdvertCategoryNumber advertCategoryNumber = db.AdvertCategoryNumber.Find(getAdvertCategoryNumberID);
                            advertCategoryNumber.DateModified = dt;
                            db.Entry(advertCategoryNumber).State = EntityState.Modified;

                            db.SaveChanges();
                        }
                    }
                }
                return SentAdvertJson(BudgetYear, DepartmentID);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_PLAN_TO_ADVERT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Adverts/AdvertSummary
        public ActionResult AdvertSummary()
        {
            try
            {
                Adverts adverts = new Adverts();
                if (adverts == null)
                {
                    LogHelper.Log(Log.Event.GET_ADVERT_Summary, "Advert Details cannot be found");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Advert Details cannot be found");
                }
                else
                {
                    return Json(new
                    {
                        success = true,
                        message = "Advert Summary",
                        data = db.Adverts.Where(y => y.AdvertStatusID == y.AdvertStatusID).Select(x => new
                        {
                            x.AdvertID,
                            x.Headline,
                            x.BidOpeningDate,
                            x.BidClosingDate,
                            x.AdvertStatusID,
                            x.AdvertStatus.Status

                        })
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADVERT_SUMMARY, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Adverts/AdvertDetails
        public ActionResult AdvertDetails(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    LogHelper.Log(Log.Event.GET_ADVERT_DETAILS, "AdvertID cannot be found");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Error("AdvertID cannot be found");
                }
                Adverts adverts = db.Adverts.Find(id);
                if (adverts == null)
                {
                    LogHelper.Log(Log.Event.GET_ADVERT_DETAILS, "Advert Details cannot be found");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Advert Details cannot be found");
                }
                return Json(new
                {
                    success = true,
                    message = "Advert details",
                    data = new
                    {
                        Adverts = db.Adverts.Where(x => x.AdvertID == id).Select(x => new
                        {
                            x.AdvertStatusID,
                            x.Headline,
                            DepartmentName = db.AdvertLotNumber.Where(y => y.AdvertID == x.AdvertID).Select(y => y.Procurements.Department.DepartmentName).FirstOrDefault(),
                            BudgetYear = db.BudgetYear.Where(y => y.BudgetYearID == x.BudgetYearID).Select(y => y.BudgetYear1.Value.Year).FirstOrDefault(),
                            AdvertCategory = db.AdvertLotNumber.Where(z => z.AdvertID == x.AdvertID).Select(z => new
                            {
                                z.ProcurementID,
                                ProjectCategoryID = db.AdvertCategoryNumber.Where(a => a.AdvertID == x.AdvertID).Select(a => a.ProjectCategoryID).FirstOrDefault(),
                                AdvertCategoryNumberID = db.AdvertCategoryNumber.Where(a => a.AdvertID == x.AdvertID).Select(a => a.AdvertCategoryNumberID).FirstOrDefault(),
                                AdvertLotNumberID = db.AdvertLotNumber.Where(a => a.AdvertID == x.AdvertID).Select(a => a.AdvertLotNumberID).FirstOrDefault(),
                                CategoryName = db.AdvertCategoryNumber.Where(a => a.AdvertID == x.AdvertID).Select(a => a.ProjectCategory.Name).FirstOrDefault(),
                                CategoryLotCode = db.AdvertCategoryNumber.Where(a => a.AdvertID == x.AdvertID).Select(a => a.CategoryLotCode).FirstOrDefault(),
                                z.Procurements.ProjectName
                            }),
                            ProjectCategory = db.ProjectCategoryOrganizationSettings.Where(z => z.EnableProjectCategory == true).Select(z => new
                            {
                                z.ProjectCategoryID,
                                z.ProjectCategory.Name
                            })
                        })
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADVERT_DETAIL, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // PUT: Adverts/DraftAdvert
        [HttpPut]
        public ActionResult DraftAdvert(string DepartmentName, List<AdvertPreparation> Adverts)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var DepartmentID = db.Department.Where(x => x.DepartmentName == DepartmentName).Select(x => x.DepartmentID).FirstOrDefault();
                int AdvertStatusID = 0;
                int.TryParse(GetConfiguration("DraftAdvertStatusID"), out AdvertStatusID);
                foreach (AdvertPreparation Advertised in Adverts)
                {
                    var AdvertInDraft = Advertised.Category;
                    if (Adverts == null)
                    {
                        LogHelper.Log(Log.Event.Update_ADVERT, "Advert with ID: " + Advertised.AdvertID + "do not exist.");
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return Error("Project with ID: " + Advertised.AdvertID + "do not exist.");
                    }
                    Adverts UpdateAdvert = db.Adverts.Find(Advertised.AdvertID);
                    UpdateAdvert.AdvertStatusID = AdvertStatusID;
                    UpdateAdvert.Headline = Advertised.Headline;
                    UpdateAdvert.Introduction = Advertised.Introduction;
                    UpdateAdvert.ScopeOfWork = Advertised.ScopeOfWork;
                    UpdateAdvert.EligibiltyRequirement = Advertised.EligibiltyRequirement;
                    UpdateAdvert.CollectionOfTenderDocument = Advertised.CollectionOfTenderDocument;
                    UpdateAdvert.BidSubmission = Advertised.BidSubmission;
                    UpdateAdvert.OtherImportantInformation = Advertised.BidSubmission;
                    UpdateAdvert.BidOpeningDate = Advertised.BidOpeningDate;
                    UpdateAdvert.BidClosingDate = Advertised.BidClosingDate;
                    UpdateAdvert.DateModified = dt;
                    db.Entry(UpdateAdvert).State = EntityState.Modified;
                    var DeleteProject = Advertised.Category.Where(x => x.Deleted == true);
                    if (Advertised.Category != null)
                    {
                        foreach (var AdvertCategory in DeleteProject)
                        {
                            db.AdvertLotNumber.RemoveRange(db.AdvertLotNumber.Where(x => x.ProcurementID == AdvertCategory.ProcurementID));
                        }
                    }
                    foreach (AdvertProjectCategory draftAdvert in AdvertInDraft)
                    {
                        var getProjectCategoryID = db.ProjectCategory.Where(x => x.ProjectCategoryID == draftAdvert.ProjectCategoryID).Select(x => x.ProjectCategoryID).FirstOrDefault();
                        var getProjectCategory = db.ProjectCategory.Where(x => x.ProjectCategoryID == draftAdvert.ProjectCategoryID).Select(x => x.Name).FirstOrDefault();
                        var getAdvertCategoryID = db.AdvertCategory.Where(x => x.AdvertCategory1 == getProjectCategory).Select(x => x.AdvertCategoryID).FirstOrDefault();
                       
                        if (Advertised.AdvertID == Guid.Empty && draftAdvert.ProcurementID == Guid.Empty)
                        {
                            LogHelper.Log(Log.Event.Update_ADVERT,  "Advert OR Procurement ID could not be found.");
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Error("Advert OR Procurement ID could not be found.");
                        }
                        var getAdvertLotNumberID = db.AdvertLotNumber.Where(x => x.ProcurementID == draftAdvert.ProcurementID).Select(x => x.AdvertLotNumberID).FirstOrDefault();
                        var getAdvertCategoryNumberID = db.AdvertCategoryNumber.Where(x => x.ProjectCategoryID == draftAdvert.ProjectCategoryID).Select(x => x.AdvertCategoryNumberID).FirstOrDefault();

                        AdvertLotNumber advertLotNumber = db.AdvertLotNumber.Find(getAdvertLotNumberID);
                        advertLotNumber.LotCode = draftAdvert.CategoryLotCode;
                        advertLotNumber.DateModified = dt;
                        db.Entry(advertLotNumber).State = EntityState.Modified;

                        AdvertCategoryNumber advertCategoryNumber = db.AdvertCategoryNumber.Find(getAdvertCategoryNumberID);
                        advertCategoryNumber.CategoryLotCode = draftAdvert.CategoryLotCode;
                        advertCategoryNumber.ProjectCategoryID = getProjectCategoryID;
                        advertCategoryNumber.AdvertCategoryID = getAdvertCategoryID;
                        advertCategoryNumber.DateModified = dt;
                        db.Entry(advertCategoryNumber).State = EntityState.Modified;

                        Procurements procurements = db.Procurements.Find(draftAdvert.ProcurementID);
                        procurements.ProjectName = draftAdvert.ProjectName;
                        procurements.DateModified = dt;
                        db.Entry(procurements).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }
                return PutAdvertJson();
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADVERT_IN_DRAFT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }

        }

        // POST: Adverts/AddPlanToAdvert
        [HttpPut]
        public ActionResult PublishAdvert(string DepartmentName, List<AdvertPreparation> Adverts)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var DepartmentID = db.Department.Where(x => x.DepartmentName == DepartmentName).Select(x => x.DepartmentID).FirstOrDefault();
                int AdvertStatusID = 0;
                int.TryParse(GetConfiguration("PublishedAdvertStatusID"), out AdvertStatusID);
                foreach (AdvertPreparation Advertised in Adverts)
                {
                    var AdvertInDraft = Advertised.Category;
                    if (Adverts == null)
                    {
                        LogHelper.Log(Log.Event.Update_ADVERT, "Advert with ID: " + Advertised.AdvertID + "do not exist.");
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return Error("Project with ID: " + Advertised.AdvertID + "do not exist.");
                    }
                    Adverts UpdateAdvert = db.Adverts.Find(Advertised.AdvertID);
                    UpdateAdvert.AdvertStatusID = AdvertStatusID;
                    UpdateAdvert.Headline = Advertised.Headline;
                    UpdateAdvert.Introduction = Advertised.Introduction;
                    UpdateAdvert.ScopeOfWork = Advertised.ScopeOfWork;
                    UpdateAdvert.EligibiltyRequirement = Advertised.EligibiltyRequirement;
                    UpdateAdvert.CollectionOfTenderDocument = Advertised.CollectionOfTenderDocument;
                    UpdateAdvert.BidSubmission = Advertised.BidSubmission;
                    UpdateAdvert.OtherImportantInformation = Advertised.BidSubmission;
                    UpdateAdvert.BidOpeningDate = Advertised.BidOpeningDate;
                    UpdateAdvert.BidClosingDate = Advertised.BidClosingDate;
                    UpdateAdvert.DateModified = dt;
                    db.Entry(UpdateAdvert).State = EntityState.Modified;
                    var DeleteProject = Advertised.Category.Where(x => x.Deleted == true);
                    if (Advertised.Category != null)
                    {
                        foreach (var AdvertCategory in DeleteProject)
                        {
                            db.AdvertLotNumber.RemoveRange(db.AdvertLotNumber.Where(x => x.ProcurementID == AdvertCategory.ProcurementID));
                        }
                    }
                    foreach (AdvertProjectCategory draftAdvert in AdvertInDraft)
                    {
                        var getProjectCategoryID = db.ProjectCategory.Where(x => x.ProjectCategoryID == draftAdvert.ProjectCategoryID).Select(x => x.ProjectCategoryID).FirstOrDefault();
                        var getProjectCategory = db.ProjectCategory.Where(x => x.ProjectCategoryID == draftAdvert.ProjectCategoryID).Select(x => x.Name).FirstOrDefault();
                        var getAdvertCategoryID = db.AdvertCategory.Where(x => x.AdvertCategory1 == getProjectCategory).Select(x => x.AdvertCategoryID).FirstOrDefault();

                        if (Advertised.AdvertID == Guid.Empty && draftAdvert.ProcurementID == Guid.Empty)
                        {
                            LogHelper.Log(Log.Event.Update_ADVERT, "Advert OR Procurement ID could not be found.");
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            return Error("Advert OR Procurement ID could not be found.");
                        }
                        var getAdvertLotNumberID = db.AdvertLotNumber.Where(x => x.ProcurementID == draftAdvert.ProcurementID).Select(x => x.AdvertLotNumberID).FirstOrDefault();
                        var getAdvertCategoryNumberID = db.AdvertCategoryNumber.Where(x => x.ProjectCategoryID == draftAdvert.ProjectCategoryID).Select(x => x.AdvertCategoryNumberID).FirstOrDefault();

                        AdvertLotNumber advertLotNumber = db.AdvertLotNumber.Find(getAdvertLotNumberID);
                        advertLotNumber.LotCode = draftAdvert.CategoryLotCode;
                        advertLotNumber.DateModified = dt;
                        db.Entry(advertLotNumber).State = EntityState.Modified;

                        AdvertCategoryNumber advertCategoryNumber = db.AdvertCategoryNumber.Find(getAdvertCategoryNumberID);
                        advertCategoryNumber.CategoryLotCode = draftAdvert.CategoryLotCode;
                        advertCategoryNumber.ProjectCategoryID = getProjectCategoryID;
                        advertCategoryNumber.AdvertCategoryID = getAdvertCategoryID;
                        advertCategoryNumber.DateModified = dt;
                        db.Entry(advertCategoryNumber).State = EntityState.Modified;

                        Procurements procurements = db.Procurements.Find(draftAdvert.ProcurementID);
                        procurements.ProjectName = draftAdvert.ProjectName;
                        procurements.DateModified = dt;
                        db.Entry(procurements).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                }
                return PutAdvertJson();
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.PUBLISHED_ADVERT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }


        }

        // GET: Adverts/Create
        public ActionResult Create()
        {
            ViewBag.AdvertStatusID = new SelectList(db.AdvertStatus, "AdvertStatusID", "Status");
            ViewBag.BudgetYearID = new SelectList(db.BudgetYear, "BudgetYearID", "CreatedBy");
            return View();
        }

        // POST: Adverts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdvertID,BudgetYearID,AdvertStatusID,Headline,Introduction,ScopeOfWork,EligibiltyRequirement,CollectionOfTenderDocument,BidSubmission,OtherImportantInformation,BidOpeningDate,BidClosingDate,CreatedBy,DateCreated,DateModified")] Adverts adverts)
        {
            if (ModelState.IsValid)
            {
                adverts.AdvertID = Guid.NewGuid();
                db.Adverts.Add(adverts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdvertStatusID = new SelectList(db.AdvertStatus, "AdvertStatusID", "Status", adverts.AdvertStatusID);
            ViewBag.BudgetYearID = new SelectList(db.BudgetYear, "BudgetYearID", "CreatedBy", adverts.BudgetYearID);
            return View(adverts);
        }

        // GET: Adverts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adverts adverts = db.Adverts.Find(id);
            if (adverts == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdvertStatusID = new SelectList(db.AdvertStatus, "AdvertStatusID", "Status", adverts.AdvertStatusID);
            ViewBag.BudgetYearID = new SelectList(db.BudgetYear, "BudgetYearID", "CreatedBy", adverts.BudgetYearID);
            return View(adverts);
        }

        // POST: Adverts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdvertID,BudgetYearID,AdvertStatusID,Headline,Introduction,ScopeOfWork,EligibiltyRequirement,CollectionOfTenderDocument,BidSubmission,OtherImportantInformation,BidOpeningDate,BidClosingDate,CreatedBy,DateCreated,DateModified")] Adverts adverts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adverts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdvertStatusID = new SelectList(db.AdvertStatus, "AdvertStatusID", "Status", adverts.AdvertStatusID);
            ViewBag.BudgetYearID = new SelectList(db.BudgetYear, "BudgetYearID", "CreatedBy", adverts.BudgetYearID);
            return View(adverts);
        }

        // GET: Adverts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adverts adverts = db.Adverts.Find(id);
            if (adverts == null)
            {
                return HttpNotFound();
            }
            return View(adverts);
        }

        // POST: Adverts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Adverts adverts = db.Adverts.Find(id);
            db.Adverts.Remove(adverts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

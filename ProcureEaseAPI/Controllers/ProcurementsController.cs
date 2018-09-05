using ProcureEaseAPI.Models;
using ProcureEaseAPI.Providers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace ProcureEaseAPI.Controllers
{
    public class ProcurementsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: Procurments/DraftNeedsSummary
        [Providers.Authorize]
        [HttpGet]
        public ActionResult DraftNeedsSummary(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                LogHelper.Log(Log.Event.ALL_DRAFT_PROCUREMENT_NEEDS, "DepartmentID is Null");
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Error("DepartmentID is Null");
            }
            else
            {
                Guid guidID = new Guid();
                try
                {
                    guidID = Guid.Parse(id);
                }
                catch (FormatException ex)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error(ex.Message);
                }
                return Json(new
                {
                    success = true,
                    message = "Drafted procurement Summary",
                    data = new
                    {
                        DepartmentName = db.Procurements.Where(x => x.DepartmentID == guidID).Select(x => x.Department.DepartmentName).FirstOrDefault(),
                        Procurement = db.BudgetYear.Select(x => new
                        {
                            BudgetYear = x.BudgetYear1.Value.Year,
                            TotalNumberOfProjects = db.Procurements.Where(y => y.DepartmentID == guidID && y.BudgetYearID == x.BudgetYearID).Count(),
                            EstimatedCost = db.Items.Where(z => z.Procurements.DepartmentID == guidID && z.Procurements.BudgetYearID == x.BudgetYearID).Select(z => z.UnitPrice).Sum()
                                             * db.Items.Where(z => z.Procurements.DepartmentID == guidID && z.Procurements.BudgetYearID == x.BudgetYearID).Select(z => z.Quantity).Sum(),
                            DepartmentID = guidID,
                            x.BudgetYearID
                        })
                    }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //GET: Procurements/DraftNeeds
        [HttpGet]
        [Providers.Authorize]
        public ActionResult DraftNeeds(string id = "", string id2 = "")
        {
            switch (string.IsNullOrEmpty(id) && (string.IsNullOrEmpty(id2)))
            {
                case true:
                    {
                        LogHelper.Log(Log.Event.ALL_DRAFT_PROCUREMENT_NEEDS, "DepartmentID or BudgetYearID is Null");
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return Error("DepartmentID or BudgetYearID is Null");
                    }
                default:
                    {
                        Guid guidID = new Guid();
                        Guid guidID2 = new Guid();
                        try
                        {
                            guidID = Guid.Parse(id);
                            guidID2 = Guid.Parse(id2);
                        }
                        catch (FormatException ex)
                        {
                            Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            LogHelper.Log(Log.Event.ALL_DRAFT_PROCUREMENT_NEEDS, "Guid format exeception");
                            return Error(ex.Message);
                        }
                        var BudgetYear = db.BudgetYear.Where(x => x.BudgetYearID == guidID2).Select(x => x.BudgetYear1.Value.Year).FirstOrDefault();
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return DraftNeedsJson(guidID,BudgetYear); 

                    }
            }
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


        //POST: Procurement/DraftNeeds
        [HttpPost]
        [Providers.Authorize]
        public ActionResult DraftNeeds(Guid DepartmentID, int BudgetYear, List<DepartmentProject> Projects)
        {          
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("DraftProcurementStatusID"), out ProcurementStatusID);
                DateTimeSettings DateTimeSettings = new DateTimeSettings();
                var CheckIfDepartmentIsValid = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                if(CheckIfDepartmentIsValid == null)
                {
                    LogHelper.Log(Log.Event.POST_DRAFT_NEEDS, "DepartmentID is Null");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Error("DepartmentID is Null or Department is not valid.");
                }
                var BudgetyearID = db.BudgetYear.Where(x => x.BudgetYear1.Value.Year == BudgetYear).Select(x => x.BudgetYearID).FirstOrDefault();
                if (BudgetyearID == null)
                {
                    LogHelper.Log(Log.Event.POST_DRAFT_NEEDS, "BudgetYear is Null.");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Error("BudgetYear is Null.");
                }

                #region ProcessNewProjects
                ProcessNewProjects(Projects, DepartmentID,BudgetyearID);
                #endregion

                #region ProcessUpdatedProjects
                // process updated projects
                var updatedProjects = Projects.Where(x => x.ProcurementID != Guid.Empty && x.Deleted == false);
                foreach (DepartmentProject project in updatedProjects)
                {
                    // process items
                    var items = project.Items;
                    if (items == null)
                    {
                        // update project
                        Procurements dbProcurement = db.Procurements.Find(project.ProcurementID);
                        if (dbProcurement == null)
                        {
                            LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "Project with ID: " + project.ProcurementID + " could not be found.");
                            Response.StatusCode = (int)HttpStatusCode.NotFound;
                            return Error("Project with ID: " + project.ProcurementID + " could not be found.");
                        }
                        else
                        {
                            dbProcurement.ProjectName = project.ProjectName;
                            dbProcurement.ProjectCategoryID = project.ProjectCategoryID;
                            dbProcurement.ProcurementMethodID = project.ProcurementMethodID;
                            dbProcurement.DepartmentID = DepartmentID;
                            dbProcurement.BudgetYearID = BudgetyearID;
                            dbProcurement.DateModified = DateTimeSettings.CurrentDate();
                            dbProcurement.ProcurementStatusID = ProcurementStatusID;
                            db.Entry(dbProcurement).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        var UpdatedItem = items.Where(x => x.ItemID != Guid.Empty && x.Deleted == false);
                        foreach (DepartmentItems item in UpdatedItem)
                        {
                            // update items
                            Items dbItem = db.Items.Find(item.ItemID);
                            if (dbItem == null)
                            {
                                LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "Item with ID: " + item.ItemID + " could not be found.");
                                Response.StatusCode = (int)HttpStatusCode.NotFound;
                                return Error("Item with ID: " + item.ItemID + " could not be found.");
                            }
                            else
                            {
                                dbItem.UnitPrice = item.UnitPrice;
                                dbItem.Quantity = item.Quantity;
                                dbItem.ItemCodeID = item.ItemCodeID;
                                dbItem.ItemName = item.ItemName;
                                dbItem.Description = item.Description;
                                dbItem.DateModified = DateTimeSettings.CurrentDate();
                                db.Entry(dbItem).State = EntityState.Modified;
                            }
                            Procurements dbProcurement = db.Procurements.Find(project.ProcurementID);
                            if (dbProcurement == null)
                            {
                                LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "Project with ID: " + project.ProcurementID + " could not be found.");
                                Response.StatusCode = (int)HttpStatusCode.NotFound;
                                return Error("Project with ID: " + project.ProcurementID + " could not be found.");
                            }
                            else
                            {
                                dbProcurement.ProjectName = project.ProjectName;
                                dbProcurement.ProjectCategoryID = project.ProjectCategoryID;
                                dbProcurement.ProcurementMethodID = project.ProcurementMethodID;
                                dbProcurement.DepartmentID = DepartmentID;
                                dbProcurement.BudgetYearID = BudgetyearID;
                                dbProcurement.DateModified = DateTimeSettings.CurrentDate();
                                dbProcurement.ProcurementStatusID = ProcurementStatusID;
                                db.Entry(dbProcurement).State = EntityState.Modified;
                            }
                        }

                    }

                }

                #endregion

                #region ProcessDeletedProjects
                var deletedProjects = Projects.Where(x => x.Deleted == true);
                foreach (DepartmentProject project in deletedProjects)
                {
                    db.Items.RemoveRange(db.Items.Where(c => c.ProcurementID==project.ProcurementID));
                    Procurements dbProcurement = db.Procurements.Find(project.ProcurementID);
                    if (dbProcurement == null)
                    {
                        LogHelper.Log(Log.Event.POST_DRAFT_NEEDS, "Project with ID: " + project.ProcurementID + " could not be found.");
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return Error("Project with ID: " + project.ProcurementID + " could not be found.");
                    }
                    else
                    {
                        db.Procurements.Remove(dbProcurement);
                    }
                }
                #endregion

                db.SaveChanges();
                Response.StatusCode = (int)HttpStatusCode.Created;
                return DraftNeedsJson(DepartmentID, BudgetYear);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.POST_DRAFT_NEEDS, ex.Message);
                return Error("" + ex.Message + ex.StackTrace);
            }

        }

       

        private void ProcessNewProjects(List<DepartmentProject> Projects, Guid DepartmentID, Guid BudgetyearID)
        {
        #region   // process new projects
            DateTimeSettings DateTimeSettings = new DateTimeSettings();
            var newProjects = Projects.Where(x => x.ProcurementID == Guid.Empty);
            foreach (DepartmentProject project in newProjects)
            {
                // process items in the projects
                var ProcurementID = Guid.NewGuid();
                var items = project.Items;
                if (items != null)
                {
                    foreach (DepartmentItems item in items)
                    {
                        // insert items
                        db.Items.Add(new Items()
                        {
                            ItemID = Guid.NewGuid(),
                            ProcurementID = ProcurementID,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity,
                            ItemCodeID = item.ItemCodeID,
                            ItemName = item.ItemName,
                            Description = item.Description,
                            DateCreated = DateTimeSettings.CurrentDate()
                    });
                    }
                    // insert project
                    int ProcurementStatusID = 0;
                    int.TryParse(GetConfiguration("DraftProcurementStatusID"), out ProcurementStatusID);
                    db.Procurements.Add(new Procurements()
                    {
                        ProcurementID = ProcurementID,
                        ProjectName = project.ProjectName,
                        DateCreated = DateTimeSettings.CurrentDate(),
                        ProjectCategoryID = project.ProjectCategoryID,
                        ProcurementMethodID = project.ProcurementMethodID,
                        ProcurementStatusID = ProcurementStatusID,
                            DepartmentID = DepartmentID,
                            BudgetYearID = BudgetyearID
                            // TODO: ProcurementStatusID default value should be set from database
                        });
                   
                }else
                {
                    // insert project
                    db.Procurements.Add(new Procurements()
                    {
                        ProcurementID = ProcurementID,
                        ProjectName = project.ProjectName,
                        DateCreated = DateTimeSettings.CurrentDate(),
                        ProjectCategoryID = project.ProjectCategoryID,
                        ProcurementMethodID = project.ProcurementMethodID,
                        DepartmentID = DepartmentID,
                        BudgetYearID = BudgetyearID
                    });
                }
               
            }
#endregion
        }

        private void ProcessNewSentProjects(List<DepartmentProject> Projects, Guid DepartmentID, Guid BudgetyearID)
        {
            #region   // process new projects
            DateTimeSettings DateTimeSettings = new DateTimeSettings();
            var newProjects = Projects.Where(x => x.ProcurementID == Guid.Empty);
            int ProcurementStatusID = 0;
            int.TryParse(GetConfiguration("PendingProcurementStatusID"), out ProcurementStatusID);
            foreach (DepartmentProject project in newProjects)
            {
                // process items in the projects
                var ProcurementID = Guid.NewGuid();
                var items = project.Items;
                if (items != null)
                {
                    foreach (DepartmentItems item in items)
                    {
                        // insert items
                        db.Items.Add(new Items()
                        {
                            ItemID = Guid.NewGuid(),
                            ProcurementID = ProcurementID,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity,
                            ItemCodeID = item.ItemCodeID,
                            ItemName = item.ItemName,
                            Description = item.Description,
                            DateCreated = DateTimeSettings.CurrentDate()
                        });
                    }
                    // insert project
                    db.Procurements.Add(new Procurements()
                    {
                        ProcurementID = ProcurementID,
                        ProjectName = project.ProjectName,
                        DateCreated = DateTimeSettings.CurrentDate(),
                        ProjectCategoryID = project.ProjectCategoryID,
                        ProcurementMethodID = project.ProcurementMethodID,
                        DepartmentID = DepartmentID,
                        BudgetYearID = BudgetyearID,
                        ProcurementStatusID = ProcurementStatusID
                    });

                }
                else
                {
                    // insert project
                    db.Procurements.Add(new Procurements()
                    {
                        ProcurementID = ProcurementID,
                        ProjectName = project.ProjectName,
                        DateCreated = DateTimeSettings.CurrentDate(),
                        ProjectCategoryID = project.ProjectCategoryID,
                        ProcurementMethodID = project.ProcurementMethodID,
                        DepartmentID = DepartmentID,
                        BudgetYearID = BudgetyearID,
                        ProcurementStatusID = ProcurementStatusID
                    });
                }

            }
            #endregion
        }

        //POST: Procurements/SendProcurementNeeds
        [HttpPost]
        [Providers.Authorize]
        public ActionResult SendProcurementNeeds(Guid DepartmentID, int BudgetYear, List<DepartmentProject> Projects)
        {
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("PendingProcurementStatusID"), out ProcurementStatusID);
                DateTimeSettings DateTimeSettings = new DateTimeSettings();
                var CheckIfDepartmentIsValid = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                if (CheckIfDepartmentIsValid == null)
                {
                    LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "DepartmentID is Null or Department is not valid.");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Error("DepartmentID is Null or Department is not valid.");
                }
                var BudgetyearID = db.BudgetYear.Where(x => x.BudgetYear1.Value.Year == BudgetYear).Select(x => x.BudgetYearID).FirstOrDefault();
                if (BudgetyearID == null)
                {
                    LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "BudgetYear is Null.");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Error("BudgetYear is Null.");
                }
               ProcessNewSentProjects(Projects,DepartmentID,BudgetyearID);
                #region ProcessUpdatedProjects
                // process updated projects
                var updatedProjects = Projects.Where(x => x.ProcurementID != Guid.Empty && x.Deleted == false);
                foreach (DepartmentProject project in updatedProjects)
                {
                    // process items
                    var items = project.Items;
                    if (items == null)
                    {
                        // update project
                        Procurements dbProcurement = db.Procurements.Find(project.ProcurementID);
                        if (dbProcurement == null)
                        {
                            LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "Project with ID: " + project.ProcurementID + " could not be found.");
                            Response.StatusCode = (int)HttpStatusCode.NotFound;
                            return Error("Project with ID: " + project.ProcurementID + " could not be found.");
                        }
                        else
                        {
                            dbProcurement.ProjectName = project.ProjectName;
                            dbProcurement.ProjectCategoryID = project.ProjectCategoryID;
                            dbProcurement.ProcurementMethodID = project.ProcurementMethodID;
                            dbProcurement.DepartmentID = DepartmentID;
                            dbProcurement.BudgetYearID = BudgetyearID;
                            dbProcurement.DateModified = DateTimeSettings.CurrentDate();
                            dbProcurement.ProcurementStatusID = ProcurementStatusID;
                            db.Entry(dbProcurement).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        var UpdatedItem = items.Where(x => x.ItemID != Guid.Empty && x.Deleted == false);
                        foreach (DepartmentItems item in UpdatedItem)
                        {
                            // update items
                            Items dbItem = db.Items.Find(item.ItemID);
                            if (dbItem == null)
                            {
                                LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "Item with ID: " + item.ItemID + " could not be found.");
                                Response.StatusCode = (int)HttpStatusCode.NotFound;
                                return Error("Item with ID: " + item.ItemID + " could not be found.");
                            }
                            else
                            {
                                dbItem.UnitPrice = item.UnitPrice;
                                dbItem.Quantity = item.Quantity;
                                dbItem.ItemCodeID = item.ItemCodeID;
                                dbItem.ItemName = item.ItemName;
                                dbItem.Description = item.Description;
                                dbItem.DateModified = DateTimeSettings.CurrentDate();
                                db.Entry(dbItem).State = EntityState.Modified;
                            }
                            Procurements dbProcurement = db.Procurements.Find(project.ProcurementID);
                            if (dbProcurement == null)
                            {
                                LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "Project with ID: " + project.ProcurementID + " could not be found.");
                                Response.StatusCode = (int)HttpStatusCode.NotFound;
                                return Error("Project with ID: " + project.ProcurementID + " could not be found.");
                            }
                            else
                            {
                                dbProcurement.ProjectName = project.ProjectName;
                                dbProcurement.ProjectCategoryID = project.ProjectCategoryID;
                                dbProcurement.ProcurementMethodID = project.ProcurementMethodID;
                                dbProcurement.DepartmentID = DepartmentID;
                                dbProcurement.BudgetYearID = BudgetyearID;
                                dbProcurement.DateModified = DateTimeSettings.CurrentDate();
                                dbProcurement.ProcurementStatusID = ProcurementStatusID;
                                db.Entry(dbProcurement).State = EntityState.Modified;
                            }
                        }
                   
                    }
                   
                }

                #endregion

                #region ProcessDeletedProjects
                var deletedProjects = Projects.Where(x => x.Deleted == true);
                foreach (DepartmentProject project in deletedProjects)
                {
                    db.Items.RemoveRange(db.Items.Where(c => c.ProcurementID == project.ProcurementID));
                    Procurements dbProcurement = db.Procurements.Find(project.ProcurementID);
                    if (dbProcurement == null)
                    {
                        LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, "Project with ID: " + project.ProcurementID + " could not be found.");
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return Error("Project with ID: " + project.ProcurementID + " could not be found.");
                    }
                    else
                    {
                        db.Procurements.Remove(dbProcurement);
                    }
                }
                #endregion

                db.SaveChanges();
                Response.StatusCode = (int)HttpStatusCode.Created;
                return DraftNeedsJson(DepartmentID, BudgetYear);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.SEND_PROCUREMENTS_NEEDS, ex.Message);
                return Error("" + ex.Message + ex.StackTrace);
            }

        }

        public ActionResult AutoCompleteItemName(string ItemName)
        {
            try
            {                
                var RequestedItemName = db.ItemCode.Where(y => y.ItemName.StartsWith(ItemName)).Select(y=>new {
                    y.ItemName,
                    ItemCode = y.ItemCode1
                }).ToList();             
                    if (RequestedItemName != null)
                    {
                        return Json(new
                        {
                            success = true,
                            message = "Requested Item Name with Code",
                            data = new
                            {
                                ItemWithCode = RequestedItemName
                            }
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                    LogHelper.Log(Log.Event.AUTO_COMPLETE_ITEMNAME, "Item name not found.");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Error("Item name not found.");                   
                    }                              
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.AUTO_COMPLETE_ITEMNAME, ex.Message);
                return Error("" + ex.Message + ex.StackTrace);
            }
        }

        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new {}
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Procurements/SentProcurementSummary
        [Providers.Authorize]
        [HttpGet]
        public ActionResult SentProcurementSummary()
        {
            
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("PendingProcurementStatusID"), out ProcurementStatusID);
                var CheckIfAnyProjectCostIsNull = db.Items.Select(y => y.UnitPrice == null).ToList();
                var ProjectCostStatus = CheckIfAnyProjectCostIsNull == null ? true : false;
                return Json(new
                {
                    success = true,
                    message = "Department procurement needs",
                    data = new
                    {
                        Procurement = db.Department.Select(x => new
                        {
                            x.DepartmentName,
                            BudgetYear = db.Procurements.Where(y => y.DepartmentID == x.DepartmentID && y.ProcurementStatusID == ProcurementStatusID).Select(y => y.BudgetYear.BudgetYear1.Value.Year).FirstOrDefault(),
                            PendingProcurements = db.Procurements.Where(y => y.DepartmentID == x.DepartmentID && y.BudgetYear.BudgetYear1.Value.Year == y.BudgetYear.BudgetYear1.Value.Year && y.ProcurementStatusID == ProcurementStatusID).Count(),
                            TotalCost = db.Items.Where(z => z.Procurements.DepartmentID == x.DepartmentID && z.Procurements.ProcurementStatus.ProcurementStatusID==ProcurementStatusID).Select(z => z.UnitPrice).Sum()
                                             * db.Items.Where(z => z.Procurements.DepartmentID == x.DepartmentID && z.Procurements.ProcurementStatus.ProcurementStatusID == ProcurementStatusID).Select(z => z.Quantity).Sum(),
                            x.DepartmentID,            
                        })
                    }
                }, JsonRequestBehavior.AllowGet);
            }
        

        private ActionResult DraftNeedsJson(Guid DepartmentID,int BudgetYear)
        {
            int ProcurementStatusID = 0;
            int.TryParse(GetConfiguration("DraftProcurementStatusID"), out ProcurementStatusID);
            var CheckIfAnyProjectCostIsNull = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(y => y.UnitPrice == null).ToList();
            var ProjectCostStatus = CheckIfAnyProjectCostIsNull == null ? true : false;
            return Json(new
            {
                success = true,
                Message = "Drafted procurement",
                data = new
                {
                    DepartmentName = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault(),
                    BudgetYear = db.BudgetYear.Where(x => x.BudgetYear1.Value.Year == BudgetYear).Select(x => x.BudgetYear1.Value.Year).FirstOrDefault(),
                    TotalCost = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.UnitPrice).Sum()
                                            * db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.Quantity).Sum(),
                    TotalCostStatus = ProjectCostStatus,
                    Projects = db.Procurements.Where(x => x.DepartmentID == DepartmentID && x.BudgetYear.BudgetYear1.Value.Year == BudgetYear && x.ProcurementStatusID == ProcurementStatusID).Select(x => new
                    {
                        x.ProcurementID,
                        x.ProjectName,
                        x.ProcurementMethodID,
                        x.ProjectCategoryID,
                        TotalProjectCost = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.UnitPrice).Sum()
                                     * db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.Quantity).Sum(),
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
                    })
                }
            }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult SentProcurementJson(Guid DepartmentID, int BudgetYear)
        {
            int ProcurementStatusID = 0;
            int.TryParse(GetConfiguration("PendingProcurementStatusID"), out ProcurementStatusID);
            var CheckIfAnyProjectCostIsNull = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYearID == DepartmentID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(y => y.UnitPrice == null).ToList();
            var ProjectCostStatus = CheckIfAnyProjectCostIsNull == null ? true : false;           
            return Json(new
            {
                success = true,
                Message = "All sent procurement.",
                data = new
                {
                    DepartmentName = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault(),                   
                    Projects = db.Procurements.Where(x => x.DepartmentID == DepartmentID && x.BudgetYear.BudgetYear1.Value.Year == BudgetYear && x.ProcurementStatusID == ProcurementStatusID).Select(x => new
                    {
                        x.ProcurementID,
                        x.ProjectName,
                        x.DateCreated,
                        x.ProcurementMethodID,
                        x.ProjectCategoryID,
                        x.ProcurementStatusID,
                        TotalProjectCost = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.UnitPrice).Sum()
                                     * db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID == ProcurementStatusID).Select(z => z.Quantity).Sum(),
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
                    ProcureMentStatus = db.ProcurementStatus.Select(x=> new
                    {
                        x.ProcurementStatusID,
                        x.Status
                    }),
                    ProcureMentMethod = db.ProcurementMethod.Select(x=> new
                    {
                        x.ProcurementMethodID,
                        x.Name
                    }),
                    ProjectCategory = db.ProjectCategory.Select(x => new
                    {
                        x.ProjectCategoryID,
                        x.Name
                    }),
                    BudgetYear = db.BudgetYear.Select(x => new
                    {
                        x.BudgetYearID,
                        BudgetYear = x.BudgetYear1.Value.Year
                    })
                }
            }, JsonRequestBehavior.AllowGet);
        }


    }
}
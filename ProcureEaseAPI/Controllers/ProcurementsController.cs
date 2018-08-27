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
        #region Prepare procurement needs(Department Heads)
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
                        return DraftNeedsJson(guidID, BudgetYear);

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
                if (CheckIfDepartmentIsValid == null)
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
                ProcessNewProjects(Projects, DepartmentID, BudgetyearID);
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
                foreach (DepartmentProject projects in Projects)
                {
                    var items = projects.Items;
                    var DeletedItem = items.Where(x => x.ItemID != Guid.Empty && x.Deleted == true);
                    foreach (DepartmentItems Items in DeletedItem)
                    {
                        db.Items.RemoveRange(db.Items.Where(c => c.ProcurementID == projects.ProcurementID));
                    }
                }

                foreach (DepartmentProject project in deletedProjects)
                {
                    db.Items.RemoveRange(db.Items.Where(c => c.ProcurementID == project.ProcurementID));
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

                } else
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
                ProcessNewSentProjects(Projects, DepartmentID, BudgetyearID);
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
                var RequestedItemName = db.ItemCode.Where(y => y.ItemName.StartsWith(ItemName)).Select(y => new {
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
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }


        private ActionResult DraftNeedsJson(Guid DepartmentID, int BudgetYear)
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
                    ProcureMentMethod = db.ProcurementMethod.Where(x => x.EnableProcurementMethod == true).Select(x => new
                    {
                        x.ProcurementMethodID,
                        x.Name
                    }),
                    ProjectCategory = db.ProjectCategory.Where(x => x.EnableProjectCategory == true).Select(x => new
                    {
                        x.ProjectCategoryID,
                        x.Name
                    }),
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
                    ProcureMentStatus = db.ProcurementStatus.Select(x => new
                    {
                        x.ProcurementStatusID,
                        x.Status
                    }),
                    ProcureMentMethod = db.ProcurementMethod.Where(x=>x.EnableProcurementMethod == true).Select(x => new
                    {
                        x.ProcurementMethodID,
                        x.Name
                    }),
                    ProjectCategory = db.ProjectCategory.Where(x=>x.EnableProjectCategory == true).Select(x => new
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
        #endregion

        #region Setup procurement plan(procurement officer)
        // GET: Procurements/ProcurementNeedsSummary
        [Providers.Authorize]
        [HttpGet]
        public ActionResult ProcurementNeedsSummary()
        {
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("PendingProcurementStatusID"), out ProcurementStatusID);
                var DepartmentName = db.Procurements.Select(x => x.DepartmentID).Distinct();
                return Json(new
                {
                    success = true,
                    message = "Procurement needs summary.",
                    data = new
                    {
                        ProjectSummary = db.Procurements.Select(x => new
                        {
                            x.Department.DepartmentName,
                            BudgetYear = (int?)x.BudgetYear.BudgetYear1.Value.Year,
                            PendingProcurements = db.Procurements.Where(a => a.DepartmentID == x.DepartmentID && a.BudgetYear.BudgetYear1.Value.Year == x.BudgetYear.BudgetYear1.Value.Year && a.ProcurementStatusID == ProcurementStatusID).Count(),
                            ProjectTotalCost = db.Items.Where(a => a.Procurements.DepartmentID == x.DepartmentID && a.Procurements.ProcurementStatus.ProcurementStatusID == ProcurementStatusID).Select(a => a.UnitPrice).Sum()
                                         * db.Items.Where(a => a.Procurements.DepartmentID == x.DepartmentID && a.Procurements.ProcurementStatus.ProcurementStatusID == ProcurementStatusID).Select(a => a.Quantity).Sum(),
                            ProjectTotalCostStatus = db.Items.Where(z => z.Procurements.DepartmentID == x.DepartmentID && z.Procurements.BudgetYearID == x.BudgetYearID && z.Procurements.ProcurementStatusID != ProcurementStatusID).Select(y => y.UnitPrice == null == true || false).FirstOrDefault(),
                            x.DepartmentID,
                            x.BudgetYearID
                        }).Distinct()

                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.PROCUREMENT_NEEDS_SUMMARY, ex.Message);
                return Error("" + ex.Message + ex.StackTrace);
            }
        }

        //GET: Procurements/ProcurementNeeds
        [HttpGet]
        [Providers.Authorize]
        public ActionResult ProcurementNeeds(string id = "", string id2 = "")
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
                        return ProcurementNeedsJson(guidID, BudgetYear);

                    }
            }
        }

        //POST: Procurement/ProcurementNeedsToPlan
        [HttpPost]
        [Providers.Authorize]
        public ActionResult ProcurementNeedsToPlan(Guid DepartmentID, int BudgetYear, List<DepartmentProject> Projects)
        {
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("PendingProcurementStatusID"), out ProcurementStatusID);
                DateTimeSettings DateTimeSettings = new DateTimeSettings();
                var CheckIfDepartmentIsValid = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                if (CheckIfDepartmentIsValid == null)
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

                #region ProcessNewProcurementPlan
                ProcessNewProcurementPlan(Projects, DepartmentID, BudgetyearID);
                #endregion

                #region ProcessUpdatedProjects
                // process updated projects
                var updatedProjects = Projects.Where(x => x.ProcurementID != Guid.Empty && x.Deleted == false && x.Approved == false);
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

                // Add  projects to plan
                int approvedProcurementStatusID = 0;
                int.TryParse(GetConfiguration("ApprovedProcurementStatusID"), out approvedProcurementStatusID);
                var UpdateProjectToPlan = Projects.Where(x => x.ProcurementID != Guid.Empty && x.Deleted == false && x.Approved == true);
                foreach (DepartmentProject project in UpdateProjectToPlan)
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
                            dbProcurement.ProcurementStatusID = approvedProcurementStatusID;
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
                                dbProcurement.ProcurementStatusID = approvedProcurementStatusID;
                                db.Entry(dbProcurement).State = EntityState.Modified;
                            }
                        }

                    }

                }
                var AddProjectToPlan = Projects.Where(x => x.ProcurementID == Guid.Empty && x.Deleted == false && x.Approved == true);
                foreach (DepartmentProject projects in AddProjectToPlan)
                {
                    var procurementID = Guid.NewGuid();
                    db.Procurements.Add(new Procurements()
                    {
                        ProcurementID = procurementID,
                        ProjectName = projects.ProjectName,
                        DateCreated = DateTimeSettings.CurrentDate(),
                        ProjectCategoryID = projects.ProjectCategoryID,
                        ProcurementMethodID = projects.ProcurementMethodID,
                        ProcurementStatusID = approvedProcurementStatusID,
                        DepartmentID = DepartmentID,
                        BudgetYearID = BudgetyearID
                    });
                    var items = projects.Items;
                    if (items != null)
                    {
                        foreach (DepartmentItems item in items)
                        {
                            // insert items
                            db.Items.Add(new Items()
                            {
                                ItemID = Guid.NewGuid(),
                                ProcurementID = procurementID,
                                UnitPrice = item.UnitPrice,
                                Quantity = item.Quantity,
                                ItemCodeID = item.ItemCodeID,
                                ItemName = item.ItemName,
                                Description = item.Description,
                                DateCreated = DateTimeSettings.CurrentDate()
                            });
                        }

                    }
                }
                #region ProcessDeletedItems
                foreach (DepartmentProject project in Projects)
                {
                    var item = project.Items;
                    if (item != null)
                    {
                        var DeletedItem = item.Where(x => x.ItemID != Guid.Empty && x.Deleted == true);
                        foreach (DepartmentItems Item in DeletedItem)
                        {
                            db.Items.RemoveRange(db.Items.Where(c => c.ProcurementID == project.ProcurementID));
                        }
                    }
                }
                var DeletedProject = Projects.Where(x => x.Deleted == true);
                foreach (var DeleteProject in DeletedProject)
                {
                    db.Procurements.RemoveRange(db.Procurements.Where(c => c.ProcurementID == DeleteProject.ProcurementID));
                }

                #endregion
                db.SaveChanges();
             // Response.StatusCode = (int)HttpStatusCode.Created;
                return ProcurementNeedsJson(DepartmentID, BudgetYear);

            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.POST_DRAFT_NEEDS, ex.Message);
                return Error("" + ex.Message + ex.StackTrace);
            }

        }

        private void ProcessNewProcurementPlan(List<DepartmentProject> Projects, Guid DepartmentID, Guid BudgetyearID)
        {
            #region   // process new procurement plan
            DateTimeSettings DateTimeSettings = new DateTimeSettings();
            var newProjects = Projects.Where(x => x.ProcurementID == Guid.Empty && x.Approved == false);
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
                    int.TryParse(GetConfiguration("PendingProcurementStatusID"), out ProcurementStatusID);
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
                        BudgetYearID = BudgetyearID
                    });
                }
            }
        }
        #endregion

        //GET: Procurements/DraftNeeds
        [HttpGet]
        [Providers.Authorize]
        public ActionResult ProcurementPlan(string id = "", string id2 = "")
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
                        return ProcurementPlanJson(guidID, BudgetYear);

                    }
            }
        }


        private ActionResult ProcurementNeedsJson(Guid DepartmentID, int BudgetYear)
        {
            int draftProcurementStatusID = 0;
            int.TryParse(GetConfiguration("DraftProcurementStatusID"), out draftProcurementStatusID);
            int attestedProcurementStatusID = 0;
            int.TryParse(GetConfiguration("AttestedProcurementStatusID"), out attestedProcurementStatusID);
            int approvedProcurementStatusID = 0;
            int.TryParse(GetConfiguration("ApprovedProcurementStatusID"), out approvedProcurementStatusID);
            return Json(new
            {
                success = true,
                Message = "All procurement needs.",
                data = new
                {
                    DepartmentName = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault(),
                    Projects = db.Procurements.Where(x => x.DepartmentID == DepartmentID && x.BudgetYear.BudgetYear1.Value.Year == BudgetYear && x.ProcurementStatusID != attestedProcurementStatusID && x.ProcurementStatusID != draftProcurementStatusID).Select(x => new
                    {
                        x.ProcurementID,
                        x.ProjectName,
                        x.DateCreated,
                        x.ProcurementMethodID,
                        x.ProjectCategoryID,
                        x.ProcurementStatusID,
                        TotalProjectCost = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID != draftProcurementStatusID && z.Procurements.ProcurementStatusID != attestedProcurementStatusID).Select(z => z.UnitPrice).Sum()
                                     * db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID != draftProcurementStatusID && z.Procurements.ProcurementStatusID != attestedProcurementStatusID).Select(z => z.Quantity).Sum(),
                        ProjectTotalCostStatus = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID != draftProcurementStatusID && z.Procurements.ProcurementStatusID != attestedProcurementStatusID).Select(y => y.UnitPrice != null == true || false).FirstOrDefault(),
                        Deleted = false,
                        Approved = db.Procurements.Where(z=>z.ProcurementStatusID==x.ProcurementStatusID).Select(z=>z.ProcurementStatusID == approvedProcurementStatusID==true|false).FirstOrDefault(),
                        Items = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID != draftProcurementStatusID && z.Procurements.ProcurementStatusID != attestedProcurementStatusID).Select(z => new
                        {
                            z.ItemID,
                            z.ItemName,
                            z.ItemCodeID,
                            ItemCode = z.ItemCode.ItemCode1,
                            z.Quantity,
                            z.UnitPrice,
                            EstimatedCost = z.UnitPrice * z.Quantity,
                            Deleted = false
                        })
                    }),
                    ProcureMentStatus = db.ProcurementStatus.Select(x => new
                    {
                        x.ProcurementStatusID,
                        x.Status
                    }),
                    ProcureMentMethod = db.ProcurementMethod.Where(x=>x.EnableProcurementMethod==true).Select(x => new
                    {
                        x.ProcurementMethodID,
                        x.Name
                    }),
                    ProjectCategory = db.ProjectCategory.Where(x=>x.EnableProjectCategory==true).Select(x => new
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

        private ActionResult ProcurementPlanJson(Guid DepartmentID, int BudgetYear)
        {
            int approvedProcurementStatusID = 0;
            int.TryParse(GetConfiguration("ApprovedProcurementStatusID"), out approvedProcurementStatusID);
            return Json(new
            {
                success = true,
                Message = "All procurement Plans.",
                data = new
                {
                    TotalProjectCost = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.Procurements.ProcurementStatusID == approvedProcurementStatusID).Select(z => z.UnitPrice).Sum()
                                     * db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.Procurements.ProcurementStatusID == approvedProcurementStatusID).Select(z => z.Quantity).Sum(),
                    ProjectTotalCostStatus = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.Procurements.ProcurementStatusID == approvedProcurementStatusID).Select(y => y.UnitPrice != null == true || false).FirstOrDefault(),
                    DepartmentName = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault(),
                    Projects = db.Procurements.Where(x => x.DepartmentID == DepartmentID && x.BudgetYear.BudgetYear1.Value.Year == BudgetYear && x.ProcurementStatusID == approvedProcurementStatusID).Select(x => new
                    {
                        x.ProcurementID,
                        x.ProjectName,
                        DateCreated = x.DateCreated.Value.ToString(),
                        x.ProcurementMethodID,
                        x.ProjectCategoryID,
                        x.ProcurementStatusID,
                        Deleted = false,
                        Items = db.Items.Where(z => z.Procurements.DepartmentID == DepartmentID && z.Procurements.BudgetYear.BudgetYear1.Value.Year == BudgetYear && z.ProcurementID == x.ProcurementID && z.Procurements.ProcurementStatusID == approvedProcurementStatusID).Select(z => new
                        {
                            z.ItemID,
                            z.ItemName,
                            z.ItemCodeID,
                            ItemCode = z.ItemCode.ItemCode1,
                            z.Quantity,
                            z.UnitPrice,
                            EstimatedCost = z.UnitPrice * z.Quantity,
                            Deleted = false
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
                    BudgetYear = db.BudgetYear.Select(x => new
                    {
                        x.BudgetYearID,
                        BudgetYear = x.BudgetYear1.Value.Year
                    }),
                    Adverts = db.Adverts.Select(x => new
                    {
                        x.AdvertID,
                        x.Headline
                    })
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Approve procurement plan(Head of Procurement)

        // GET: Procurements/ProcurementPlanSummary
        [Providers.Authorize]
        [HttpGet]
        public ActionResult ProcurementPlanSummary()
        {
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("ApprovedProcurementStatusID"), out ProcurementStatusID);
                return Json(new
                {
                    success = true,
                    message = "Procurement plan summary.",
                    data = new
                    {
                        TotalCost = db.Items.Where(a => a.Procurements.ProcurementStatus.ProcurementStatusID == ProcurementStatusID).Select(a => a.UnitPrice).Sum()
                                         * db.Items.Where(a => a.Procurements.ProcurementStatus.ProcurementStatusID == ProcurementStatusID).Select(a => a.Quantity).Sum(),
                        ProjectSummary = db.Procurements.Select(x => new
                        {
                            x.Department.DepartmentName,
                            BudgetYear = (int?)x.BudgetYear.BudgetYear1.Value.Year,
                            TotalProject = db.Procurements.Where(a => a.DepartmentID == x.DepartmentID && a.BudgetYear.BudgetYear1.Value.Year == x.BudgetYear.BudgetYear1.Value.Year && a.ProcurementStatusID == ProcurementStatusID).Count(),
                            ProjectTotalCost = db.Items.Where(a => a.Procurements.DepartmentID == x.DepartmentID && a.Procurements.ProcurementStatus.ProcurementStatusID == ProcurementStatusID).Select(a => a.UnitPrice).Sum()
                                         * db.Items.Where(a => a.Procurements.DepartmentID == x.DepartmentID && a.Procurements.ProcurementStatus.ProcurementStatusID == ProcurementStatusID).Select(a => a.Quantity).Sum(),
                            x.DepartmentID,
                            x.BudgetYearID
                        }).Distinct()

                    }
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.PROCUREMENT_PLAN_SUMMARY, ex.Message);
                return Error("" + ex.Message + ex.StackTrace);
            }
        }

        //POST: Procurement/UpdateProcurementPlan
        [HttpPost]
        [Providers.Authorize]
        public ActionResult UpdateProcurementPlan(Guid DepartmentID, int BudgetYear, List<DepartmentProject> Projects)
        {
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("ApprovedProcurementStatusID"), out ProcurementStatusID);
                DateTimeSettings DateTimeSettings = new DateTimeSettings();
                var CheckIfDepartmentIsValid = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                if (CheckIfDepartmentIsValid == null)
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

                #region ProcessNewApprovedProcurementPlan
                ProcessNewApprovedProcurementPlan(Projects, DepartmentID, BudgetyearID);
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

               
                #region ProcessDeletedItems
                foreach (DepartmentProject project in Projects)
                {
                    var item = project.Items;
                    if (item != null)
                    {
                        var DeletedItem = item.Where(x => x.ItemID != Guid.Empty && x.Deleted == true);
                        foreach (DepartmentItems Item in DeletedItem)
                        {
                            db.Items.RemoveRange(db.Items.Where(c => c.ProcurementID == project.ProcurementID));
                        }
                    }
                }
                var DeletedProject = Projects.Where(x => x.Deleted == true);
                foreach (var DeleteProject in DeletedProject)
                {
                    db.Procurements.RemoveRange(db.Procurements.Where(c => c.ProcurementID == DeleteProject.ProcurementID));
                }

                #endregion
                db.SaveChanges();
                Response.StatusCode = (int)HttpStatusCode.Created;
                return ProcurementPlanJson(DepartmentID, BudgetYear);

            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.POST_DRAFT_NEEDS, ex.Message);
                return Error("" + ex.Message + ex.StackTrace);
            }

        }

        private void ProcessNewApprovedProcurementPlan(List<DepartmentProject> Projects, Guid DepartmentID, Guid BudgetyearID)
        {
            #region   // process new procurement plan
            DateTimeSettings DateTimeSettings = new DateTimeSettings();
            var newProjects = Projects.Where(x => x.ProcurementID == Guid.Empty && x.Approved == false);
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
                    int.TryParse(GetConfiguration("PendingProcurementStatusID"), out ProcurementStatusID);
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
                        BudgetYearID = BudgetyearID
                    });
                }
            }

            var newlyApprovedProjects = Projects.Where(x => x.ProcurementID == Guid.Empty && x.Approved == true);
            foreach (DepartmentProject project in newlyApprovedProjects)
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
                    int.TryParse(GetConfiguration("ApprovedProcurementStatusID"), out ProcurementStatusID);
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
                        BudgetYearID = BudgetyearID
                    });
                }
            }
        }

        #endregion


        //POST: Procurement/UpdateProcurementPlan
        [HttpPost]
        [Providers.Authorize]
        public ActionResult ApproveProcurementPlan(Guid DepartmentID, int BudgetYear, List<DepartmentProject> Projects)
        {
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("ApprovedProcurementStatusID"), out ProcurementStatusID);
                DateTimeSettings DateTimeSettings = new DateTimeSettings();
                var CheckIfDepartmentIsValid = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                if (CheckIfDepartmentIsValid == null)
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

                #region ProcessNewApprovedProcurementPlan
                ProcessNewAttestedProcurementPlan(Projects, DepartmentID, BudgetyearID);
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
                                db.Entry(dbProcurement).State = EntityState.Modified;
                            }
                        }

                    }

                }

                #endregion

                #region ProcessDeletedItems
                foreach (DepartmentProject project in Projects)
                {
                    var item = project.Items;
                    if (item != null)
                    {
                        var DeletedItem = item.Where(x => x.ItemID != Guid.Empty && x.Deleted == true);
                        foreach (DepartmentItems Item in DeletedItem)
                        {
                            db.Items.RemoveRange(db.Items.Where(c => c.ProcurementID == project.ProcurementID));
                        }
                    }
                }
                var DeletedProject = Projects.Where(x => x.Deleted == true);
                foreach (var DeleteProject in DeletedProject)
                {
                    db.Procurements.RemoveRange(db.Procurements.Where(c => c.ProcurementID == DeleteProject.ProcurementID));
                }

                #endregion
                db.SaveChanges();
                Response.StatusCode = (int)HttpStatusCode.Created;
                return ProcurementPlanJson(DepartmentID, BudgetYear);

            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.POST_DRAFT_NEEDS, ex.Message);
                return Error("" + ex.Message + ex.StackTrace);
            }

        }

        private void ProcessNewAttestedProcurementPlan(List<DepartmentProject> Projects, Guid DepartmentID, Guid BudgetyearID)
        {
            #region   // process new procurement plan
            DateTimeSettings DateTimeSettings = new DateTimeSettings();
            var newProjects = Projects.Where(x => x.ProcurementID == Guid.Empty && x.Attested == false);
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
                    int.TryParse(GetConfiguration("ApprovedProcurementStatusID"), out ProcurementStatusID);
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
                        BudgetYearID = BudgetyearID
                    });
                }
            }

            var newlyAttestedProjects = Projects.Where(x => x.ProcurementID == Guid.Empty && x.Attested == true);
            foreach (DepartmentProject project in newlyAttestedProjects)
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
                    int.TryParse(GetConfiguration("AttestedProcurementStatusID"), out ProcurementStatusID);
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
                        BudgetYearID = BudgetyearID
                    });
                }
            }
            #endregion
        }

        #endregion

    }
}
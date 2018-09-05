using System;
using System.Linq;
using System.Web.Mvc;
using ProcureEaseAPI.Models;
using System.Net;
using System.Threading.Tasks;
using Utilities;
using System.Configuration;

namespace ProcureEaseAPI.Controllers
{
    public class ProcureController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        //GET: Procure/SentProcurement

        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SentProcurement(string id = "", string id2 = "")
        {
            switch (string.IsNullOrEmpty(id) && (string.IsNullOrEmpty(id2)))
            {
                case true:
                    {
                        LogHelper.Log(Log.Event.SENT_PROCUREMENTS, "DepartmentID or BudgetYearID is Null");
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
                            LogHelper.Log(Log.Event.ALL_DRAFT_PROCUREMENT_NEEDS, "Guid format exeception");
                            return Error(ex.Message);
                        }
                        var BudgetYear = db.BudgetYear.Where(x => x.BudgetYearID == guidID2).Select(x => x.BudgetYear1.Value.Year).FirstOrDefault();
                        return SentProcurementJson(guidID, BudgetYear);

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
                    })
                }
            }, JsonRequestBehavior.AllowGet);
        }


    }
}

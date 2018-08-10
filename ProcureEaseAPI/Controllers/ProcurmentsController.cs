using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace ProcureEaseAPI.Controllers
{
    public class ProcurmentsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: Procurments/DraftNeedsSummary
        [Authorize]
        [HttpGet]
        public ActionResult DraftNeedsSummary(string id = "")
        {           
            if (string.IsNullOrEmpty(id))
            {
                LogHelper.Log(Log.Event.ALL_DRAFT_PROCUREMENT_NEEDS, "DepartmentID is Null");
                return Json(new
                {
                    success = true,
                    message = "DepartmentID is Null",
                    data = db.Procurements.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
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
                    return Json(new
                    {
                        success = false,
                        message = "" + ex.Message,
                        data = db.Procurements.Select(x => new
                        { })
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    success = true,
                    message = "Drafted procurement",
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
    }
}
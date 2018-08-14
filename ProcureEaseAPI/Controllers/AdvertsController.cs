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
                return  SentAdvertCategoryJson(advertCategory);
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
        }

        // GET: Adverts/Details/5
        public ActionResult AddPlanToAdvert(Guid DepartmentID, Guid AdvertID, int BudgetYear, Guid ProcurementID, Guid ProcurementStatusID)
        {
            try
            {
                Adverts adverts = new Adverts();
                var departmentID = db.Department.Where(x => x.DepartmentID == DepartmentID).Select(x => x.DepartmentID).FirstOrDefault();
                var advertID = db.Adverts.Where(x => x.AdvertID == AdvertID).Select(x => x.AdvertID).FirstOrDefault();
                var procurementID = db.Procurements.Where(x => x.ProcurementID == ProcurementID).Select(x => x.ProcurementID).FirstOrDefault();
                
                adverts.AdvertID = Guid.NewGuid();
                db.Adverts.Add(adverts);
                db.SaveChanges();
                //var BudgetyearID = db.BudgetYear.Where(x => x.BudgetYear1.Value.Year == BudgetYear).Select(x => x.BudgetYearID).FirstOrDefault();
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
                              //  z.ItemCodeID,
                               // ItemCode = z.ItemCode.ItemCode1,
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
            catch (Exception)
            {

                throw;
            }
        }


        // GET: Adverts/Details/5
        public ActionResult Details(Guid? id)
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

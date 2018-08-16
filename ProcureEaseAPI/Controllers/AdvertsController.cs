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

        [HttpPost]
        public ActionResult SentAdvertJson(int BudgetYear, Guid DepartmentID)
        {
            try
            {
                int ProcurementStatusID = 0;
                int.TryParse(GetConfiguration("AttestedProcurementStatusID"), out ProcurementStatusID);

                //TODO:check if budget year and departmentID is equal to null
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
            catch (Exception)
            {

                throw;
            }
        }

        // Post: Adverts/AddAdvert
        [HttpPost]
        public ActionResult AddAdvert(int BudgetYear, string Headline, string DepartmentName)
        {
            try
            {
                var DepartmentID = db.Department.Where(x => x.DepartmentName == DepartmentName).Select(x => x.DepartmentID).FirstOrDefault();
                Adverts adverts = new Adverts();
                var BudgetYearId = db.BudgetYear.Where(x => x.BudgetYear1.Value.Year == BudgetYear).Select(x => x.BudgetYearID).FirstOrDefault();
                int AdvertStatusID = 0;
                int.TryParse(GetConfiguration("DraftProcurementStatusID"), out AdvertStatusID);

                adverts.AdvertID = Guid.NewGuid();
                adverts.BudgetYearID = BudgetYearId;
                adverts.AdvertStatusID = AdvertStatusID;
                adverts.Headline = Headline;

                db.Adverts.Add(adverts);
                db.SaveChanges();
                return SentAdvertJson(BudgetYear, DepartmentID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: Adverts/AddPlanToAdvert
        [HttpPost]
        public ActionResult AddPlanToAdvert(int BudgetYear, string DepartmentName, List<AdvertPreparation>Adverts)
        {
            try
            {
                var DepartmentID = db.Department.Where(x => x.DepartmentName == DepartmentName).Select(x => x.DepartmentID).FirstOrDefault();
                int AttestedProcurementStatusID = 0;
                int.TryParse(GetConfiguration("AttestedProcurementStatusID"), out AttestedProcurementStatusID);
                foreach (AdvertPreparation Advertised in Adverts)
                {
                    if (Advertised.AdvertID == Guid.Empty && Advertised.ProcurementID == Guid.Empty)
                    {

                    }
                    var checkIfProjectIsAttested = db.Procurements.Where(x => x.ProcurementID == Advertised.ProcurementID && x.ProcurementStatusID == AttestedProcurementStatusID).Select(x => x.ProcurementStatusID);
                    if (checkIfProjectIsAttested == null)
                    {

                    }
                    var checkIfProcurementIsAddedToAdvert = db.AdvertLotNumber.Where(x => x.ProcurementID == Advertised.ProcurementID).Select(x => x.ProcurementID).FirstOrDefault();
                    if(checkIfProcurementIsAddedToAdvert==null)
                    {

                    }
                    var c1 = 'A' + 0;
                    c1++;
                    var getProjectCategory = db.ProjectCategory.Where(x => x.ProjectCategoryID == Advertised.ProjectCategoryID).Select(x => x.Name).FirstOrDefault();
                    var getAdvertCategoryID = db.AdvertCategory.Where(x => x.AdvertCategory1 == getProjectCategory).Select(x => x.AdvertCategoryID).FirstOrDefault();
                    Adverts UpdateAdvert = db.Adverts.Find(Advertised.AdvertID);
                    UpdateAdvert.Headline = Advertised.Headline;
                    db.Entry(UpdateAdvert).State = EntityState.Modified;
                    AdvertCategoryNumber advertCategoryNumber = new AdvertCategoryNumber();
                    advertCategoryNumber.AdvertCategoryNumberID = Guid.NewGuid();
                    advertCategoryNumber.ProjectCategoryID = Advertised.ProjectCategoryID;
                    advertCategoryNumber.AdvertID = Advertised.AdvertID;
                    advertCategoryNumber.CategoryLotCode = c1.ToString();
                    advertCategoryNumber.AdvertCategoryID = Advertised.ProjectCategoryID;
                    db.AdvertCategoryNumber.Add(advertCategoryNumber);
                    AdvertLotNumber advertLotNumber = new AdvertLotNumber();
                    advertLotNumber.AdvertID = Advertised.AdvertID;
                    advertLotNumber.LotCode = c1.ToString();
                    advertLotNumber.ProcurementID = Advertised.ProcurementID;
                    db.AdvertLotNumber.Add(advertLotNumber); 
                    db.SaveChanges();
                }
                return SentAdvertJson(BudgetYear, DepartmentID);
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

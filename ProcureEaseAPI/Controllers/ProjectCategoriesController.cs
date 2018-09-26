using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcureEaseAPI.Models;
using Utilities;

namespace ProcureEaseAPI.Controllers
{
    public class ProjectCategoriesController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        private CatalogsController catalog = new CatalogsController();

        // GET: ProjectCategories
        public ActionResult Index()
        {
            return View(db.ProjectCategory.ToList());
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

        // POST: ProjectCategories/AddProjectCategory
        [HttpPost]
        public ActionResult AddProjectCategory(ProjectCategory projectCategory)
        {
            try
            {
                DateTime dt = DateTime.Now;
                projectCategory.ProjectCategoryID = Guid.NewGuid();
                projectCategory.DateCreated = dt;
                projectCategory.DateModified = dt;
                projectCategory.CreatedBy = "MDA Administrator";
                db.ProjectCategory.Add(projectCategory);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_PROJECTCATEGORY, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            return Json(new
            {
                success = true,
                message = "Project Category added successfully!!!",
                data = db.ProjectCategoryOrganizationSettings.Select(x => new
                {
                    x.ProjectCategoryID,
                    x.ProjectCategory.Name,
                    x.EnableProjectCategory,
                })
            }, JsonRequestBehavior.AllowGet);
        }


        // GET: ProjectCategories/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategory projectCategory = db.ProjectCategory.Find(id);
            if (projectCategory == null)
            {
                return HttpNotFound();
            }
            return View(projectCategory);
        }

        // GET: ProjectCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectCategoryID,ProjectCategory1,EnableProjectCategory,DateModified,CreatedBy,DateCreated")] ProjectCategory projectCategory)
        {
            if (ModelState.IsValid)
            {
                projectCategory.ProjectCategoryID = Guid.NewGuid();
                db.ProjectCategory.Add(projectCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectCategory);
        }

        // GET: ProjectCategories/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategory projectCategory = db.ProjectCategory.Find(id);
            if (projectCategory == null)
            {
                return HttpNotFound();
            }
            return View(projectCategory);
        }

        // POST: ProjectCategories/Edit
        [HttpPost]
        public ActionResult Edit(ProjectCategoryOrganizationSettings projectCategoryOrganizationSettings, bool EnableProjectCategory)
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
                var currentProjectCategoryID = db.ProjectCategoryOrganizationSettings.FirstOrDefault(p => p.ProjectCategoryID == projectCategoryOrganizationSettings.ProjectCategoryID);

                if (currentProjectCategoryID == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_PROJECTCATEGORY, "ProjectCategoryID not found");
                    return Json(new
                    {
                        success = false,
                        message = "ProjectCategoryID not found",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }

                currentProjectCategoryID.EnableProjectCategory = EnableProjectCategory;
                currentProjectCategoryID.DateModified = dt;

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
                    x.EnableProjectCategory
                })
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: ProjectCategories/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCategory projectCategory = db.ProjectCategory.Find(id);
            if (projectCategory == null)
            {
                return HttpNotFound();
            }
            return View(projectCategory);
        }

        // POST: ProjectCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ProjectCategory projectCategory = db.ProjectCategory.Find(id);
            db.ProjectCategory.Remove(projectCategory);
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

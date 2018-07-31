using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcureEaseAPI.Models;

namespace ProcureEaseAPI.Controllers
{
    public class ProjectCategoriesController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: ProjectCategories
        public ActionResult Index()
        {
            return View(db.ProjectCategory.ToList());
        }

        //http://localhost:85/ProjectCategories/AddProjectCategory
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddProjectCategory(ProjectCategory projectCategory)
        {
            DateTime dt = DateTime.Now;
            projectCategory.ProjectCategoryID = Guid.NewGuid();
            projectCategory.DateCreated = dt;
            projectCategory.DateModified = dt;
            projectCategory.CreatedBy = "MDA Administrator";
            db.ProjectCategory.Add(projectCategory);
            db.SaveChanges();
            var ProjectCategory = db.ProjectCategory.Select(x => new
            {
                x.ProjectCategoryID,
                x.Name,
                x.EnableProjectCategory,
                x.CreatedBy,
            });
            var AdminDashboard = new
            {
                success = true,
                message = "Project Category added successfully!!!",
                data = new
                {
                    ProjectCategory = ProjectCategory
                }
            };
            return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
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

        // POST: ProjectCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ProjectCategoryID,ProjectCategory1,EnableProjectCategory,DateModified,CreatedBy,DateCreated")] ProjectCategory projectCategory)
        {
            if (ModelState.IsValid)
            {
                DateTime dt = DateTime.Now;
                var currentProjectCategory = db.ProjectCategory.FirstOrDefault(p => p.ProjectCategoryID == p.ProjectCategoryID);

                if (currentProjectCategory == null)
                    return HttpNotFound();

                currentProjectCategory.DateModified = dt;
                currentProjectCategory.EnableProjectCategory = projectCategory.EnableProjectCategory;
                db.SaveChanges();

                var ProjectCategory = db.ProjectCategory.Select(x => new
                {
                    x.ProjectCategoryID,
                    x.Name,
                    x.EnableProjectCategory
                });
                var AdminDashboard = new
                {
                    success = true,
                    message = "Edited successfully",
                    data = new
                    {
                        ProjectCategory = ProjectCategory
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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

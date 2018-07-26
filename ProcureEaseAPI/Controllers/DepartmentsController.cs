using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcureEaseAPI.Models;
using System.Threading.Tasks;

namespace ProcureEaseAPI.Controllers
{
    public class DepartmentsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        // GET: Departments http://localhost:85/Departments
        public ActionResult Index()
        {
            //var department = db.Department.Include(d => d.AspNetUsers);
            if (ModelState.IsValid)
            {
                Department department = new Department();
                var Departments = db.Department.Select(x => new
                {
                    x.DepartmentID,
                    x.DepartmentName,
                    x.DepartmentHeadUserID,
                    x.CreatedBy
                });

                var AdminDashboard = new
                {
                    success = true,
                    message = "Ok",
                    data = new
                    {
                        Departments = Departments
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Add Departments http://localhost:85/Departments/AddDepartment
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddDepartment(AspNetUsers aspNetUsers, string DepartmentName)
        {
            if (ModelState.IsValid)
            {
                DateTime dt = DateTime.Now;
                Department department = new Department();
                try
                {
                    {
                        department.DepartmentID = Guid.NewGuid();
                        department.DepartmentName = DepartmentName;
                        department.DepartmentHeadUserID = aspNetUsers.Id;
                        department.DateCreated = dt;
                        department.DateModified = dt;
                        department.CreatedBy = aspNetUsers.UserName;
                    };
                    db.Department.Add(department);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);

                }
                var Departments = db.Department.Select(x => new
                {
                    x.DepartmentID,
                    x.DepartmentName,
                    x.DepartmentHeadUserID,
                    x.CreatedBy
                });
                var AdminDashboard = new
                {
                    success = true,
                    message = "Department added successfully",
                    data = new
                    {
                        Departments = Departments
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        // PUT: Departments/Edit/5 http://localhost:85/Departments/Edit
        [HttpPut]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentHeadUserID,DepartmentName,DateModified,CreatedBy,DateCreated")] Department department)
        {
            if (ModelState.IsValid)
            {
                AspNetUsers aspNetUsers = new AspNetUsers();
                DateTime dt = DateTime.Now;
                department.DateCreated = dt;
                department.DateModified = dt;
                department.CreatedBy = aspNetUsers.UserName;

                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                var DepartmentSetup = db.Department.Select(x => new
                {
                    Department = db.Department.Where(y => y.DepartmentID == x.DepartmentID).Select(y => new
                    {
                        y.DepartmentID,
                        y.DepartmentName
                    }),

                    Head = db.UserProfile.Where(y => y.DepartmentID == x.DepartmentID).Select(y => new
                    {
                        x.DepartmentHeadUserID,
                        FullName = y.FirstName + " " + y.LastName
                    })
                });
                var AdminDashboard = new
                {
                    success = true,
                    message = "Edited successfully",
                    data = new
                    {
                        DepartmentSetup = DepartmentSetup
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: Departments/Delete/5 http://localhost:85/Departments/Delete
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                Department department = db.Department.Find(id);
                db.Department.Remove(department);
                db.SaveChanges();
                var Departments = db.Department.Select(x => new
                {
                    x.DepartmentID,
                    x.DepartmentName,
                    x.DepartmentHeadUserID,
                    x.CreatedBy
                });

                var AdminDashboard = new
                {
                    success = true,
                    message = "Deleted successfully",
                    data = new
                    {
                        Departments = Departments
                    }
                };
                return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound("User not found");
        }

        // GET: Departments/Edit/5 http://localhost:85/Departments/Edit
        public ActionResult getEditedDetail(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            var Departments = db.Department.Select(x => new
            {
                x.DepartmentID,
                x.DepartmentName,
                x.DepartmentHeadUserID,
                x.CreatedBy,
                DateCreated = x.DateCreated.Value.ToString(),
                DateModified = x.DateModified.Value.ToString()
            });
            var AdminDashboard = new
            {
                success = true,
                message = "Ok",
                data = new
                {
                    departments = Departments
                }
            };
            return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
        }


        // GET: Departments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentHeadUserID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,DepartmentHeadUserID,DepartmentName,DateModified,CreatedBy,DateCreated")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.DepartmentID = Guid.NewGuid();
                db.Department.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentHeadUserID = new SelectList(db.AspNetUsers, "Id", "Email", department.DepartmentHeadUserID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
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

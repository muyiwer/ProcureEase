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
using Utilities;

namespace ProcureEaseAPI.Controllers
{
    public class DepartmentsController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        

        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
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

        // GET: Departments
        public ActionResult Index()
        {
            Guid? tenantId = catalog.GetTenantID();
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
                Department department = new Department();
                var Departments = db.Department.Select(x => new
                {
                    x.DepartmentID,
                    x.DepartmentName,
                    x.DepartmentHeadUserID,
                    x.CreatedBy
                });

                return Json(new
                {
                    success = true,
                    message = "Ok",
                    data = db.Department.Select(x => new
                    {
                        Department = new
                        {
                            x.DepartmentID,
                            x.DepartmentName
                        },
                        Head = new
                        {
                            DepartmentHeadUserID = db.UserProfile.Where(y => x.DepartmentHeadUserID == y.UserID).Select(y => (true) || (false)).FirstOrDefault(),
                            FullName = db.UserProfile.Where(z => z.UserID == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName)
                            //FullName = db.UserProfile.Where(z => new Guid(z.Id) == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName)
                        }

                    }),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_DEPARTMENT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Departments/AddDepartment
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddDepartment(string DepartmentName)
        {
            try
            {
                DateTime dt = DateTime.Now;
                Department department = new Department();
                {
                    department.DepartmentID = Guid.NewGuid();
                    department.DepartmentName = DepartmentName;
                    department.DateCreated = dt;
                    department.DateModified = dt;
                    department.CreatedBy = "MDA Administrator";
                };
                    db.Department.Add(department);
                    db.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Department Added Successfully",
                    data = db.Department.Select(x => new
                    {
                        Department = new
                        {
                            x.DepartmentID,
                            x.DepartmentName
                        },
                        Head = new
                        {
                            DepartmentHeadUserID = db.UserProfile.Where(y => x.DepartmentHeadUserID == y.UserID).Select(y => (true) || (false)).FirstOrDefault(),
                            FullName = db.UserProfile.Where(z => z.UserID == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName)
                        }

                    }),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_DEPARTMENT, ex.Message + ex.StackTrace);
                return Json(new
                {
                    success = false,
                    message = ex.Message,
                    data = new { }
                });

            }
        }

        // PUT: Departments/Edit
        [HttpPut]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentHeadUserID,DepartmentName,DateModified,CreatedBy,DateCreated")] Department department)
        {
            try
            {
                UserProfile userProfile = new UserProfile();

                DateTime dt = DateTime.Now;
                var currentDepartmentDetail = db.Department.FirstOrDefault(d => d.DepartmentID == department.DepartmentID);

                if (currentDepartmentDetail == null)
                {
                    LogHelper.Log(Log.Event.EDIT_DEPARTMENT, "DepartmentID not found");
                    return Json(new
                    {
                        success = false,
                        message = "DepartmentID not found",
                        data = new { }
                    });
                }
                currentDepartmentDetail.DateModified = dt;
                currentDepartmentDetail.DepartmentName = department.DepartmentName;
                currentDepartmentDetail.DepartmentHeadUserID = department.DepartmentHeadUserID;
                db.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Editted successfully",
                    data = db.Department.Select(x => new
                    {
                        User = new
                        {
                            x.UserProfile.UserID,
                            FullName = db.UserProfile.Where(z => z.UserID == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName)
                        },
                        Head = new
                        {
                            x.DepartmentID,
                            x.DepartmentName                        
                        }

                    }),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.EDIT_DEPARTMENT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Departments/Delete
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
                try
                {
                    Department department = db.Department.Find(id);
                    db.Department.Remove(department);
                    db.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Deleted Successfully",
                    data = db.Department.Select(x => new
                    {
                        Department = new
                        {
                            x.DepartmentID,
                            x.DepartmentName
                        },
                        Head = new
                        {
                            DepartmentHeadUserID = db.UserProfile.Where(y => x.DepartmentHeadUserID == y.UserID).Select(y => (true) || (false)).FirstOrDefault(),
                            FullName = db.UserProfile.Where(z => z.UserID == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName)
                        }

                    }),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_DEPARTMENT, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Departments/Edit
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
                message = "Deleted Successfully",
                data = new
                {
                    departments = Departments
                }
            };
            return Json(AdminDashboard, JsonRequestBehavior.AllowGet);
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

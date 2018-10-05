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
        CatalogsController catalog = new CatalogsController();

        private ActionResult Error(string message)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }

        private ActionResult ExceptionError(string message, string StackTrace)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(new
            {
                success = false,
                message = message,
                data = new { InternalError = StackTrace }
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Departments
        [Providers.Authorize]
        public ActionResult Index()
        {

            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
            try
            {
                if (tenantId == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new
                {
                    success = true,
                    message = "Ok",
                    data = db.Department.Where(x => x.TenantID == tenantId).Select(x => new
                    {
                        Department = new
                        {
                            x.DepartmentID,
                            x.DepartmentName
                        },
                        Head = new
                        {
                            DepartmentHeadStatus = db.UserProfile.Where(y => x.DepartmentHeadUserID == y.UserID).Select(y => (true) || (false)).FirstOrDefault(),
                            FullName = db.UserProfile.Where(z => z.UserID == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName)
                        }
                    }),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_DEPARTMENT, ex.Message);
                return ExceptionError(ex.Message, ex.StackTrace);
            }
        }

        // POST: Departments/AddDepartment
        [HttpPost]
        [Providers.Authorize]
        public ActionResult AddDepartment(string DepartmentName, Guid? UserID)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);

            try
            {
                if (tenantId == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                DateTime dt = DateTime.Now;
                var GetRequestID = db.Catalog.Where(x => x.TenantID == tenantId).Select(x => x.RequestID).ToList().FirstOrDefault();
                var GetAdministratorFirstName = db.RequestForDemo.Where(x => x.RequestID == GetRequestID).Select(x => x.AdministratorFirstName).FirstOrDefault();
                Department department = new Department();
                {
                    department.DepartmentID = Guid.NewGuid();
                    department.TenantID = tenantId;
                    department.OrganisationID = catalog.GetOrganizationID(email);
                    department.DepartmentName = DepartmentName;
                    department.DateCreated = dt;
                    department.DateModified = dt;
                    department.CreatedBy = GetAdministratorFirstName;
                };
                    db.Department.Add(department);
                    db.SaveChanges();

                var currentDepartmentDetail = db.Department.FirstOrDefault(d => d.DepartmentID == department.DepartmentID);
                var departmentID = db.UserProfile.Where(x => x.UserID == UserID).Select(x => x.UserID).FirstOrDefault();
                currentDepartmentDetail.DepartmentHeadUserID = departmentID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_DEPARTMENT, ex.Message + ex.StackTrace);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new
            {
                success = true,
                message = "Department Added Successfully",
                data = db.Department.Where(x=> x.TenantID == tenantId).Select(x => new
                {
                    Department = new
                    {
                        x.DepartmentID,
                        x.DepartmentName
                    },
                    Head = new
                    {
                        DepartmentHeadStatus = db.UserProfile.Where(y => x.DepartmentHeadUserID == y.UserID).Select(y => (true) || (false)).FirstOrDefault(),
                        FullName = db.UserProfile.Where(z => z.UserID == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault()
                    }
                }),
            }, JsonRequestBehavior.AllowGet);
        }

        // PUT: Departments/Edit
        [HttpPut]
        [Providers.Authorize]
        public ActionResult Edit (string DepartmentID, Guid UserID, string DepartmentName)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
            AuthRepository authRepository = new AuthRepository();
            try
            {
                DateTime dt = DateTime.Now;
                var DepartmentHeadUserID = db.UserProfile.Where(x => x.UserID == UserID).Select(x => x.UserID).FirstOrDefault();
                if (tenantId == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(DepartmentID))
                {
                    LogHelper.Log(Log.Event.EDIT_DEPARTMENT, "DepartmentID is null");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "DepartmentID can not be null.",
                        data = new { }
                    });
                }
                else
                {
                    Guid guidID = new Guid();
                    guidID = Guid.Parse(DepartmentID);

                    var FindDepartment = db.Department.FirstOrDefault(x => x.DepartmentID == guidID && x.TenantID == tenantId);
                    var Id = db.UserProfile.Where(x => x.UserID == UserID).Select(x => x.Id).FirstOrDefault();
                    if (FindDepartment == null)
                    {
                        LogHelper.Log(Log.Event.EDIT_DEPARTMENT, "Department not found");
                        Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return Json(new
                        {
                            success = false,
                            message = "Department not found",
                            data = new { }
                        });
                    }
                    if (Id == null)
                    {
                        FindDepartment.DepartmentHeadUserID = DepartmentHeadUserID;
                        FindDepartment.DepartmentName = DepartmentName;
                        FindDepartment.DateModified = dt;
                        db.SaveChanges();
                    }
                    else
                    {
                        FindDepartment.DepartmentHeadUserID = DepartmentHeadUserID;
                        FindDepartment.DepartmentName = DepartmentName;
                        FindDepartment.DateModified = dt;
                        db.SaveChanges();
                        UserProfile userProfile = new UserProfile();
                        if (FindDepartment.DepartmentName == "Procurement" && FindDepartment.DepartmentHeadUserID != UserID)
                        {
                            authRepository.EditToProcurementOfficerRole(userProfile, Id);
                        }
                        if (FindDepartment.DepartmentName == "Procurement" && FindDepartment.DepartmentHeadUserID == UserID)
                        {
                            authRepository.EditToHeadOfProcumentRole(userProfile, Id);
                        }
                        if (FindDepartment.DepartmentName != "Procurement" && FindDepartment.DepartmentHeadUserID != UserID)
                        {
                            authRepository.EditToEmployeeRole(userProfile, Id);
                        }
                        if (FindDepartment.DepartmentName != "Procurement" && FindDepartment.DepartmentHeadUserID == UserID)
                        {
                            authRepository.EditToHeadOfDepartmentRole(userProfile, Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.EDIT_DEPARTMENT, ex.Message);
                ExceptionError(ex.Message, ex.StackTrace);
            }
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new
            {
                success = true,
                message = "Edited successfully",
                data = db.Department.Where(x => x.TenantID == tenantId).Select(x => new
                {
                    Department = new
                    {
                        x.DepartmentID,
                        x.DepartmentName
                    },
                    Head = new
                    {
                        DepartmentHeadStatus = db.UserProfile.Where(y => x.DepartmentHeadUserID == y.UserID).Select(y => (true) || (false)).FirstOrDefault(),
                        FullName = db.UserProfile.Where(z => z.UserID == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault()
                    }

                }),
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: Departments/Delete
        [HttpPost, ActionName("Delete")]
        [Providers.Authorize]
        public ActionResult Delete(Guid id)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
            try
                {
                    if (tenantId == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                        {
                            success = false,
                            message = "TenantId is null",
                            data = new { }
                        }, JsonRequestBehavior.AllowGet);
                    }

                    Department department = db.Department.Find(id);
                if(department == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "Department not found",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                db.Department.Remove(department);
                db.SaveChanges();
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new
                {
                    success = true,
                    message = "Deleted Successfully",
                    data = db.Department.Where(x=> x.TenantID == tenantId).Select(x => new
                    {
                        Department = new
                        {
                            x.DepartmentID,
                            x.DepartmentName
                        },
                        Head = new
                        {
                            DepartmentHeadStatus = db.UserProfile.Where(y => x.DepartmentHeadUserID == y.UserID).Select(y => (true) || (false)).FirstOrDefault(),
                            FullName = db.UserProfile.Where(z => z.UserID == x.DepartmentHeadUserID).Select(y => y.FirstName + " " + y.LastName).FirstOrDefault()
                        }

                    }),
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.DELETE_DEPARTMENT, ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }

        }

        // GET: Departments/Details/5
        [HttpPost]
        [Providers.Authorize]
        public ActionResult Details(Guid? id)
        {
            string email = Request.Headers["Email"];
            var tenantId = catalog.GetTenantIDFromClientURL(email);
            try
            {
                if (tenantId == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }

            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.GET_DEPARTMENT, ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new
            {
                success = true,
                message = "Ok",
                data = db.Department.Where(x => x.TenantID == tenantId).Select(x => new
                {
                    x.DepartmentID,
                    x.DepartmentName,
                    x.DepartmentHeadUserID,
                    x.CreatedBy,
                    DateCreated = x.DateCreated.Value.ToString(),
                    DateModified = x.DateModified.Value.ToString()
                }).FirstOrDefault()
            }, JsonRequestBehavior.AllowGet);
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

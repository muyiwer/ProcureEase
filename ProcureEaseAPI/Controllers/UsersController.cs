using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProcureEaseAPI.Models;
using Utilities;
using System.Net;
using System.Threading.Tasks;
using static Utilities.EmailHelper;

namespace ProcureEaseAPI.Controllers
{
    public class UsersController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        // POST: Users/Add
       [HttpPost]
       public ActionResult Add(UserProfile UserProfile)
        {
            try
            {                 
                if(UserProfile.UserEmail == null)
                {
                    LogHelper.Log(LogHelper.LogEvent.ADD_USER, "User Email is null");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Please Input User Email");                  
                }
                var CheckIfEmailExist = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.UserEmail).FirstOrDefault();
                if(CheckIfEmailExist != null)
                {
                    LogHelper.Log(LogHelper.LogEvent.ADD_USER, "User Email already Exist");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email already exist! Please check and try again");
                }
                UserProfile.UserId = Guid.NewGuid();
                db.UserProfile.Add(UserProfile);
                db.SaveChanges();
                //  await EmailHelper.AddEmailToQueue();
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogHelper.LogEvent.ADD_USER, ex.Message);
                return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);
            }
            return Json(db.UserProfile.Where(x => x.UserId == UserProfile.UserId).Select(x => new
            {
                success = true,
                message = "User details added successfully",
                data = new
                {
                x.UserId,
                x.UserEmail,
                x.DepartmentID,
                x.Department.DepartmentName
                }
            }), JsonRequestBehavior.AllowGet);

        }
    }
}
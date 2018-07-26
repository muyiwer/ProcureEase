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
using Newtonsoft.Json;

namespace ProcureEaseAPI.Controllers
{
    public class UsersController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        // POST: Users/Add
       [HttpPost]
       public async Task<ActionResult> Add(UserProfile UserProfile)
        {
            try
            {                 
                if(UserProfile.UserEmail == null)
                {
                    LogHelper.Log(Log.Event.INITIATE_PASSWORD_RESET, "User Email is null");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Please Input User Email");                  
                }
                var CheckIfEmailExist = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.UserEmail).FirstOrDefault();
                if(CheckIfEmailExist != null)
                {
                    LogHelper.Log(Log.Event.ADD_USER, "User Email already Exist");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email already exist! Please check and try again");
                }
                UserProfile.UserId = Guid.NewGuid();
                db.UserProfile.Add(UserProfile);
                db.SaveChanges();             
                string RecipientEmail = UserProfile.UserEmail;
                string Subject = "ProcureEase SignUp Invitation";
                string Body = new EmailTemplateHelper().GetTemplateContent("SignUpTemplate");
                var url = System.Web.HttpContext.Current.Request.Url.Host;
                string newTemplateContent = string.Format(Body," " + "http://" + url + "/#/signup/" + UserProfile.UserEmail);
                Message message = new Message( RecipientEmail,Subject, newTemplateContent);
                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.AddEmailToQueue(message);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_USER, ex.Message);
                return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);
            }
            return Json(db.UserProfile.Select(x => new
            {
                success = true,
                message = "User details added successfully",
                data = new
                {
                   User= new
                   {
                       x.UserId,
                       FullName = x.FirstName + " " + x.LastName
                   },
                   Department = new
                   {
                       x.DepartmentID,
                       x.Department.DepartmentName
                   }
                   
                }
            }), JsonRequestBehavior.AllowGet);

        }

          // Users/InitiatePasswordReset
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> InitiatePasswordReset(string UserEmail)
        {
            try
            {
                AuthRepository Repository = new AuthRepository();
                ApplicationUser user = await Repository.FindEmail(UserEmail);
                if (user == null)
                {
                    LogHelper.Log(Log.Event.INITIATE_PASSWORD_RESET, "UserEmail does not Exist");
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Email does not Exist! Please check and try again");
                }
                var PasswordToken = await Repository.GeneratePasswordToken(user.Id);
                string RecipientEmail = UserEmail;
                string Subject = "Password Reset";
                string Body = new EmailTemplateHelper().GetTemplateContent("NMRC-Template");
                var url = System.Web.HttpContext.Current.Request.Url.Host;
                string newTemplateContent = string.Format(Body,url + "/#/email/resetpassword/token" + PasswordToken);
                Message message = new Message(RecipientEmail, Subject, newTemplateContent);
                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.AddEmailToQueue(message);
            }
            catch(Exception ex)
            {
                LogHelper.Log(Log.Event.INITIATE_PASSWORD_RESET, ex.Message);
                return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);
            }
            return Json(db.UserProfile.Select(x => new
            {
                success = true,
                message = "Please check your email to reset password",
                data = new
                {
                    User = new
                    {
                        x.UserId,
                        FullName = x.FirstName + " " + x.LastName
                    },
                    Department = new
                    {
                        x.DepartmentID,
                        x.Department.DepartmentName
                    }

                }
            }), JsonRequestBehavior.AllowGet);

        }

        // Users/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel ResetPassword)
        {
            try
            {
                AuthRepository Repository = new AuthRepository();
                ApplicationUser user = await Repository.FindEmail(ResetPassword.UserEmail);
                if (user == null)
                {
                    LogHelper.Log(Log.Event.RESET_PASSWORD, "UserEmail does not Exist");
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Email does not Exist! Please check and try again");
                }
                var result = await Repository.ResetPassword(ResetPassword, user.Id);
            }
            catch (Exception ex)
            {
              LogHelper.Log(Log.Event.RESET_PASSWORD, ex.Message);
                return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);
            }
            return Json(db.UserProfile.Select(x => new
            {
                success = true,
                message = "Password reset successfully!!!",
                data = new
                {
                    User = new
                    {
                        x.UserId,
                        FullName = x.FirstName + " " + x.LastName
                    },
                    Department = new
                    {
                        x.DepartmentID,
                        x.Department.DepartmentName
                    }

                }
            }), JsonRequestBehavior.AllowGet);

        }


    }
}
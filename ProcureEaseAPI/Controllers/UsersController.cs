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
using System.Data.Entity;

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
                UserProfile.UserID = Guid.NewGuid();
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
                       x.UserID,
                       FullName = x.FirstName + " " + x.LastName
                   },
                   Department = new
                   {
                       x.DepartmentID,
                       x.Department1.DepartmentName
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
                        x.UserID,
                        FullName = x.FirstName + " " + x.LastName
                    },
                    Department = new
                    {
                        x.DepartmentID,
                        x.Department1.DepartmentName
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
                        x.UserID,
                        FullName = x.FirstName + " " + x.LastName
                    },
                    Department = new
                    {
                        x.DepartmentID,
                        x.Department1.DepartmentName
                    }

                }
            }), JsonRequestBehavior.AllowGet);

        }

        //POST: Users/SignUp
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(UserProfile UserProfile, string Password)
        {
            try
            {
                var CheckIfUserHasSignedUp = db.AspNetUsers.Where(x => x.Email == UserProfile.UserEmail).Select(x => x.Email).FirstOrDefault();
                if (CheckIfUserHasSignedUp != null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "UserEmail already exist on AspNetUser");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email has already signed up! Please check and try again");
                }
                if (UserProfile.UserEmail == null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "UserEmail is null");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Please Input your email");
                }
                AddUserModel UserModel = new AddUserModel
                {
                    Email = UserProfile.UserEmail,
                    Password = Password,
                    UserName = UserProfile.UserEmail
                };
                var CheckUserDepartmentName = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.Department1.DepartmentName).FirstOrDefault();
                var UserDepartmentName = CheckUserDepartmentName;
                AuthRepository Repository = new AuthRepository();
                ApplicationUser User =  await Repository.RegisterUser(UserModel,UserDepartmentName);
                var Id = db.AspNetUsers.Where(x => x.Email == UserProfile.UserEmail).Select(x => x.Id).FirstOrDefault();
                var CheckIfUserIsAddedByAdmin = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.UserID).FirstOrDefault();
                if (CheckIfUserIsAddedByAdmin == null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "UserEmail has not yet been added to UserProfile table");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Admin has not yet sent Invitation Emmail to this Email!!");
                }
                var UserID = CheckIfUserIsAddedByAdmin;
                UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserID).FirstOrDefault();
                EditProfile.Id = User.Id;
                EditProfile.FirstName = UserProfile.FirstName;
                EditProfile.LastName = UserProfile.LastName;           
                db.SaveChanges();
            }catch(Exception ex)
            {
                LogHelper.Log(Log.Event.SIGN_UP, ex.Message);
                return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);
            }
            return Json(db.UserProfile.Select(x => new
            {
                success = true,
                message = "SignUp successfull!!",
                data = new
                {
                    User = new
                    {
                        x.UserID,
                        FullName = x.FirstName + " " + x.LastName
                    },
                    Department = new
                    {
                        x.DepartmentID,
                        x.Department1.DepartmentName
                    }

                }
            }), JsonRequestBehavior.AllowGet);
        }



    }
}
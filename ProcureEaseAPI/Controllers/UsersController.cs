﻿using System;
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
                   // return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Please Input User Email");
                    return Json(new
                    {
                        success = false,
                        message = "Please Input User Email",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
                }
                var CheckIfEmailExist = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.UserEmail).FirstOrDefault();
                if(CheckIfEmailExist != null)
                {
                    LogHelper.Log(Log.Event.ADD_USER, "User Email already Exist");
                   // return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email already exist! Please check and try again");
                    return Json(new
                    {
                        success = false,
                        message = "Email already exist! Please check and try again",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
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
               // return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    success = false,
                    message = "" +ex.Message,
                    data = db.UserProfile.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = true,
                message = "SignUp successfull!!",
                data = db.UserProfile.Select(x => new
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

                }),
            }, JsonRequestBehavior.AllowGet);

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
                //    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Email does not Exist! Please check and try again");
                    return Json(new
                    {
                        success = false,
                        message = "Email does not Exist! Please check and try again",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
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
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = db.UserProfile.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = true,
                message = "SignUp successfull!!",
                data = db.UserProfile.Select(x => new
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

                }),
            }, JsonRequestBehavior.AllowGet);

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
                   // return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Email does not Exist! Please check and try again");
                    return Json(new
                    {
                        success = false,
                        message = "Email does not Exist! Please check and try again",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
                }
                var result = await Repository.ResetPassword(ResetPassword, user.Id);
            }
            catch (Exception ex)
            {
              LogHelper.Log(Log.Event.RESET_PASSWORD, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = db.UserProfile.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = true,
                message = "SignUp successfull!!",
                data = db.UserProfile.Select(x => new
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

                }),
            }, JsonRequestBehavior.AllowGet);

        }

        //POST: Users/SignUp
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(UserProfile UserProfile, string Password)
        {
            try
            {
                var CheckIfUserHasSignedUp = db.AspNetUsers.Where(x => x.UserName == UserProfile.UserEmail).Select(x => x.UserName).FirstOrDefault();
                if (CheckIfUserHasSignedUp != null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "UserEmail already exist on AspNetUser");
                  //  return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email has already signed up! Please check and try again");
                    return Json(new
                    {
                        success = false,
                        message = "Email has already signed up! Please check and try again",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
                }
                if (UserProfile.UserEmail == null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "UserEmail is null");
                   // return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Please Input your email");
                    return Json(new
                    {
                        success = false,
                        message = "Please Input your email",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
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
                    LogHelper.Log(Log.Event.SIGN_UP, "UserEmail does not exist on UserProfile table");
                  // return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Admin has not send Invitation Email to this Email!!");
                    return Json(new
                    {
                        success = false,
                        message = "Admin has not send Invitation Email to this Email!!",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
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
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = db.UserProfile.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = true,
                message = "SignUp successfull!!",
                data = db.UserProfile.Select(x => new
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

                }),
            }, JsonRequestBehavior.AllowGet);
        }

 
        //PUT: Users/EditUser
        [HttpPut]
        public ActionResult EditUser(UserProfile UserProfile)
        {
            try
            {               
                if (UserProfile.DepartmentID == null)
                {
                    LogHelper.Log(Log.Event.EDIT_USER, "DepartmentID is Null");
                    return Json(new
                    {
                        success = false,
                        message = "No Department is Selected",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
                }
                if (UserProfile.UserID == null)
                {
                    LogHelper.Log(Log.Event.EDIT_USER, "UserID is Null");
                    return Json(new
                    {
                        success = false,
                        message = "UserID is Null",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
                }

                var Id= db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                if (Id == null)
                {
                    EditUserWithoutID(UserProfile);
                    return Json(new
                    {
                        success = true,
                        message = "",
                        data = db.UserProfile.Select(x => new
                        {
                            x.UserID,
                            FullName = x.FirstName + " " + x.LastName,
                            x.Department1.DepartmentName,
                            x.DepartmentID,
                            x.UserEmail,
                            DepartmentHeadUserID = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                        })
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var CheckUserDepartmentName = db.Department.Where(x => x.DepartmentID == UserProfile.DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                    var CheckIfUserIsHeadOfDepartment = db.Department.Where(x => x.DepartmentHeadUserID == UserProfile.UserID).Select(x => x.DepartmentHeadUserID).FirstOrDefault();
                    if (CheckUserDepartmentName == "Procurement" && CheckIfUserIsHeadOfDepartment == null)
                    {
                        EditToProcurementOfficerRole(UserProfile, Id);
                    }
                    if (CheckUserDepartmentName == "Procurement" && CheckIfUserIsHeadOfDepartment != null)
                    {
                        EditToHeadOfProcumentRole(UserProfile, Id);
                    }
                    if (CheckUserDepartmentName != "Procurement" && CheckIfUserIsHeadOfDepartment == null)
                    {
                        EditToEmployeeRole(UserProfile, Id);
                    }
                    if (CheckUserDepartmentName != "Procurement" && CheckIfUserIsHeadOfDepartment != null)
                    {
                        EditToHeadOfDepartmentRole(UserProfile, Id);
                    }

                    return Json(new
                    {
                        success = true,
                        message = "",
                        data = db.UserProfile.Select(x => new
                        {
                            x.UserID,
                            FullName = x.FirstName + " " + x.LastName,
                            x.Department1.DepartmentName,
                            x.DepartmentID,
                            x.UserEmail,
                            DepartmentHeadUserID = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                        })
                    }, JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
            {
                LogHelper.Log(Log.Event.SIGN_UP, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = db.UserProfile.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        protected void EditUserWithoutID(UserProfile UserProfile)
        {
            UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).FirstOrDefault();          
            EditProfile.UserEmail = UserProfile.UserEmail;
            EditProfile.DepartmentID = UserProfile.DepartmentID;
            db.SaveChanges();
        }
        
        protected void EditToProcurementOfficerRole(UserProfile UserProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Procurement Officer").Select(x => x.Id).FirstOrDefault();
            UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).FirstOrDefault();
            EditProfile.UserEmail = UserProfile.UserEmail;
            EditProfile.DepartmentID = UserProfile.DepartmentID;
            AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
            db.AspNetUserRoles.Remove(role);
            db.SaveChanges();
            AspNetUserRoles userRole = new AspNetUserRoles();
            userRole.UserId = Id;
            userRole.RoleId = RoleId;
            db.AspNetUserRoles.Add(userRole);
            AspNetUsers EditUser = db.AspNetUsers.Where(x => x.Id == Id).FirstOrDefault();
            EditUser.UserName = UserProfile.UserEmail;
            db.SaveChanges();
        }

        protected void EditToHeadOfProcumentRole(UserProfile UserProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Procurement Head").Select(x => x.Id).FirstOrDefault();
            UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).FirstOrDefault();
            EditProfile.UserEmail = UserProfile.UserEmail;
            EditProfile.DepartmentID = UserProfile.DepartmentID;
            AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
            db.AspNetUserRoles.Remove(role);
            db.SaveChanges();
            AspNetUserRoles userRole = new AspNetUserRoles();
            userRole.UserId = Id;
            userRole.RoleId = RoleId;
            AspNetUsers EditUser = db.AspNetUsers.Where(x => x.Id == Id).FirstOrDefault();
            EditUser.UserName = UserProfile.UserEmail;            
            db.SaveChanges();
        }

        protected void EditToEmployeeRole(UserProfile UserProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Employee").Select(x => x.Id).FirstOrDefault();
            UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).FirstOrDefault();
            EditProfile.UserEmail = UserProfile.UserEmail;
            EditProfile.DepartmentID = UserProfile.DepartmentID;
            AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
            db.AspNetUserRoles.Remove(role);
            db.SaveChanges();
            AspNetUserRoles userRole = new AspNetUserRoles();
            userRole.UserId = Id;
            userRole.RoleId = RoleId;
            AspNetUsers EditUser = db.AspNetUsers.Where(x => x.Id == Id).FirstOrDefault();
            EditUser.UserName = UserProfile.UserEmail;
            db.SaveChanges();
        }

        protected void EditToHeadOfDepartmentRole(UserProfile UserProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Head of Department").Select(x => x.Id).FirstOrDefault();
            UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).FirstOrDefault();
            EditProfile.UserEmail = UserProfile.UserEmail;
            EditProfile.DepartmentID = UserProfile.DepartmentID;
            AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
            db.AspNetUserRoles.Remove(role);
            db.SaveChanges();
            AspNetUserRoles userRole = new AspNetUserRoles();
            userRole.UserId = Id;
            userRole.RoleId = RoleId;
            AspNetUsers EditUser = db.AspNetUsers.Where(x => x.Id == Id).FirstOrDefault();
            EditUser.UserName = UserProfile.UserEmail;
            db.SaveChanges();
        }

        //PUT: Users/UpdateUserProfile
        [HttpPut]
        public ActionResult UpdateUserProfile(UserProfile UserProfile)
        {
            try
            {
                var Id = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                if(Id == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, "Id is Null(Not yet signed up)");
                    return Json(new
                    {
                        success = false,
                        message = "Not yet signed up",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
                }
                if(UserProfile.UserID == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, "UserID is Null");
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "UserID is null");
                }
                UserProfile UpdateProfile = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).FirstOrDefault();
                UpdateProfile.UserEmail = UserProfile.UserEmail;
                UpdateProfile.FirstName = UserProfile.FirstName;
                UpdateProfile.LastName = UserProfile.LastName;
                AspNetUsers EditUser = db.AspNetUsers.Where(x => x.Id == Id).FirstOrDefault();
                EditUser.UserName = UserProfile.UserEmail;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = db.UserProfile.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = true,
                message = "",
                data = db.UserProfile.Select(x => new
                {
                    x.UserID,
                    FullName = x.FirstName + " " + x.LastName,
                    x.Department1.DepartmentName,
                    x.DepartmentID,
                    x.UserEmail,
                    DepartmentHeadUserID = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                })
            }, JsonRequestBehavior.AllowGet);
        }

        //PUT: Users/UpdateDepartmentHead
        public ActionResult UpdateDepartmentHead(UserProfile UserProfile)
        {
            try
            {
                if (UserProfile.UserID == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_DEPARTMENT_HEAD, "UserID is Null");
                    return Json(new
                    {
                        success = false,
                        message = "Input UserID",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
                }
                var CheckDepartmentHead = db.Department.Where(x => x.DepartmentHeadUserID == UserProfile.UserID).Select(x => x.DepartmentHeadUserID).FirstOrDefault();
                if (CheckDepartmentHead != null)
                {          
                    return new HttpStatusCodeResult(HttpStatusCode.OK, "User already head of department");
                }
                var CheckUserDepartmentName = db.Department.Where(x => x.DepartmentID == UserProfile.DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();
                var Id = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                if (Id != null)
                {
                    if (CheckUserDepartmentName == "Procurement")
                    {
                        var RoleId = db.AspNetRoles.Where(x => x.Name == "Procurement Head").Select(x => x.Id).FirstOrDefault();
                        Department UpdateDepartmentHead = db.Department.Where(x => x.DepartmentID == UserProfile.DepartmentID).FirstOrDefault();
                        UpdateDepartmentHead.DepartmentHeadUserID = UserProfile.UserID;
                        AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId== Id);
                        db.AspNetUserRoles.Remove(role);
                        db.SaveChanges();
                        AspNetUserRoles userRole = new AspNetUserRoles();
                        userRole.UserId = Id;
                        userRole.RoleId = RoleId;
                        db.SaveChanges();
                    }
                    else
                    {
                        var RoleId = db.AspNetRoles.Where(x => x.Name == "Head of Department").Select(x => x.Id).FirstOrDefault();
                        Department UpdateDepartmentHead = db.Department.Where(x => x.DepartmentID == UserProfile.DepartmentID).FirstOrDefault();
                        UpdateDepartmentHead.DepartmentHeadUserID = UserProfile.UserID;
                        AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
                        db.AspNetUserRoles.Remove(role);
                        db.SaveChanges();
                        AspNetUserRoles userRole = new AspNetUserRoles();
                        userRole.UserId = Id;
                        userRole.RoleId = RoleId;
                        db.SaveChanges();
                    }
                }
                if (Id == null)
                {
                        Department UpdateDepartmentHead = db.Department.Where(x => x.DepartmentID == UserProfile.DepartmentID).FirstOrDefault();
                        UpdateDepartmentHead.DepartmentHeadUserID = UserProfile.UserID;
                        db.SaveChanges();
                    
                }
                return Json(new
                {
                    success = true,
                    message = "",
                    data = db.UserProfile.Select(x => new
                    {
                        x.UserID,
                        FullName = x.FirstName + " " + x.LastName,
                        x.Department1.DepartmentName,
                        x.DepartmentID,
                        x.UserEmail,
                        DepartmentHeadUserID = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                    })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_DEPARTMENT_HEAD, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = db.UserProfile.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //GET: Users/GetAllUsers
        // [ActionName("ByDepartment")]
        //[HttpGet]
        //public ActionResult GetAllUsers()
        //{
        //    return GetAllUsers(Guid.Empty);
        //}

        [HttpGet]
        public ActionResult GetAllUsers(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new
                {
                    success = true,
                    message = "",
                    data = db.UserProfile.Select(x => new
                    {
                        x.UserID,
                        FullName = x.FirstName + " " + x.LastName,
                        x.Department1.DepartmentName,
                        x.DepartmentID,
                        x.UserEmail,
                        DepartmentHeadUserID = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                    })
                }, JsonRequestBehavior.AllowGet);
            } else
            {
                Guid guidID = new Guid();
                try
                {
                    guidID = Guid.Parse(id);
                }
                catch (FormatException ex)
                {
                    return Json(new
                    {
                        success = false,
                        message = "" + ex.Message,
                        data = db.UserProfile.Select(x => new
                        { })
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    success = true,
                    message = "",
                    data = db.UserProfile.Where(x => x.DepartmentID == guidID).Select(x => new
                    {
                        x.UserID,
                        FullName = x.FirstName + " " + x.LastName,
                        x.Department1.DepartmentName,
                        x.DepartmentID,
                        x.UserEmail,
                        DepartmentHeadUserID = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                    })
                }, JsonRequestBehavior.AllowGet);
            }
        }

      
        [HttpDelete]
        public ActionResult Delete(UserProfile UserProfile)
        {
            try
            { 
            if(UserProfile.UserID==null)
            {
                LogHelper.Log(Log.Event.DELETE_USER, "UserID is Null");
                    // return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    return Json(new
                    {
                        success = false,
                        message = "UserID is Null",
                        data = db.UserProfile.Select(x => new
                        { }).FirstOrDefault()
                    }, JsonRequestBehavior.AllowGet);
                }
            var Id = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                if (Id == null)
                {
                    var checkIfUserIsHeadOfDepartment = db.Department.Where(x => x.DepartmentHeadUserID == UserProfile.UserID).FirstOrDefault();
                    if (checkIfUserIsHeadOfDepartment != null)
                    {
                        Department RemoveUserFromHeadOfDepartment = db.Department.SingleOrDefault(x => x.DepartmentHeadUserID == UserProfile.UserID);
                        RemoveUserFromHeadOfDepartment.DepartmentHeadUserID = null;
                        db.SaveChanges();

                    }
                    UserProfile profile = db.UserProfile.SingleOrDefault(x => x.UserID == UserProfile.UserID);
                    db.UserProfile.Remove(profile);
                    db.SaveChanges();
                    return Json(new
                    {
                        success = true,
                        message = "",
                        data = db.UserProfile.Select(x => new
                        {
                            x.UserID,
                            FullName = x.FirstName + " " + x.LastName,
                            x.Department1.DepartmentName,
                            x.DepartmentID,
                            x.UserEmail,
                            DepartmentHeadUserID = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                        })
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var checkIfUserIsHeadOfDepartment = db.Department.Where(x => x.DepartmentHeadUserID == UserProfile.UserID).FirstOrDefault();
                    if (checkIfUserIsHeadOfDepartment != null)
                    {
                        Department RemoveUserFromHeadOfDepartment = db.Department.SingleOrDefault(x => x.DepartmentHeadUserID == UserProfile.UserID);
                        RemoveUserFromHeadOfDepartment.DepartmentHeadUserID = null;
                        db.SaveChanges();

                    }
                    UserProfile profile = db.UserProfile.SingleOrDefault(x => x.UserID == UserProfile.UserID);
                    db.UserProfile.Remove(profile);
                    AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
                    db.AspNetUserRoles.Remove(role);
                    AspNetUsers users = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                    db.AspNetUsers.Remove(users);
                    db.SaveChanges();
                    return Json(new
                    {
                        success = true,
                        message = "",
                        data = db.UserProfile.Select(x => new
                        {
                            x.UserID,
                            FullName = x.FirstName + " " + x.LastName,
                            x.Department1.DepartmentName,
                            x.DepartmentID,
                            x.UserEmail,
                            DepartmentHeadUserID = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                        })
                    }, JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_DEPARTMENT_HEAD, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = db.UserProfile.Select(x => new
                    { }).FirstOrDefault()
                }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}
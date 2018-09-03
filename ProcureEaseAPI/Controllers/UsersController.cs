using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProcureEaseAPI.Models;
using Utilities;
using System.Net;
using System.Threading.Tasks;
using static Utilities.EmailHelper;
using System.Net.Http;
using System.Web.Script.Serialization;
using ProcureEaseAPI.Providers;

namespace ProcureEaseAPI.Controllers
{
    public class UsersController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();

        //POST: Users/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string UserName, string Password)
        {
            DateTimeSettings DateTimeSettings = new DateTimeSettings();
            /*This will depend totally on how you will get access to the identity provider and get your token, this is just a sample of how it would be done*/
            /*Get Access Token Start*/
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = System.Web.HttpContext.Current.Request.Url;
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("UserName", UserName));
            postData.Add(new KeyValuePair<string, string>("Password", Password));
            postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
            HttpContent content = new FormUrlEncodedContent(postData);
            HttpResponseMessage response = await httpClient.PostAsync("/token", content);
            //  var error = response.EnsureSuccessStatusCode();
            string ResponseContent = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Token token = serializer.Deserialize<Token>(ResponseContent);
 
            if (response.StatusCode != HttpStatusCode.OK)
            {
                LogHelper.Log(Log.Event.LOGIN, ResponseContent);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new
                {
                    success = false,
                    message = "Invalid username or password",
                    data = new {}
                }, JsonRequestBehavior.AllowGet);
            } else {
                var User = db.AspNetUsers.Where(x => x.UserName == UserName).FirstOrDefault();
                return Json(new
                {
                    success = true,
                    message = "Login successful",
                    data = new
                    {
                       User.Id,
                       Email = User.UserName,
                       DepartmentID = db.UserProfile.Where(x=>x.UserEmail == UserName).Select(x=>x.DepartmentID).FirstOrDefault(),
                       Role = db.AspNetUserRoles.Where(x=>x.UserId == User.Id).Select(x=>x.AspNetRoles.Name).FirstOrDefault(),
                       BudgetYear = DateTimeSettings.CurrentYear(),
                       OrganizationName= db.UserProfile.Where(x=>x.UserEmail == UserName).Select(x=>x.OrganizationSettings.OrganizationNameAbbreviation).FirstOrDefault(),
                        OrganizationID = db.UserProfile.Where(x => x.UserEmail == UserName).Select(x => x.OrganizationID).FirstOrDefault(),
                        token = token
                    }
                }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Users/Add
       [HttpPost]
     //  [Providers.Authorize]
       public async Task<ActionResult> Add(UserProfile UserProfile)
        {
            try
            {
                var url = System.Web.HttpContext.Current.Request.Url.Host;
                GetTenantID(url);
                if (UserProfile.OrganizationID != null)
                {
                    Guid guidID = new Guid();
                    var OrganizationID = Convert.ToString(UserProfile.OrganizationID);
                    try
                    {
                        guidID = Guid.Parse(OrganizationID);
                    }
                    catch (FormatException ex)
                    {
                        LogHelper.Log(Log.Event.ADD_USER, "Invalid GUID format");
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Error(ex.Message);
                    }                 
                }
                else
                {
                    LogHelper.Log(Log.Event.ADD_USER, "OrganizationID is null.");
                    return Json(new
                    {
                        success = false,
                        message = "OrganizationID is null.",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                if (UserProfile.UserEmail == null)
                {
                    LogHelper.Log(Log.Event.ADD_USER, "User email is null");
                    return Json(new
                    {
                        success = false,
                        message = "Please input user email",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var CheckIfEmailExist = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.UserEmail).FirstOrDefault();
                if(CheckIfEmailExist != null)
                {
                    LogHelper.Log(Log.Event.ADD_USER, "User email already exists");
                   // return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Email already exist! Please check and try again");
                    return Json(new
                    {
                        success = false,
                        message = "Email already exists! Please check and try again.",
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                UserProfile.UserID = Guid.NewGuid();
                db.UserProfile.Add(UserProfile);
                db.SaveChanges();             
                string RecipientEmail = UserProfile.UserEmail;
                string Subject = "ProcureEase SignUp Invitation";
                string Body = new EmailTemplateHelper().GetTemplateContent("SignUpTemplate");              
                string newTemplateContent = string.Format(Body," " + "http://" + url + "/#/signup/" + UserProfile.UserEmail);
                Message message = new Message( RecipientEmail,Subject, newTemplateContent);
                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.AddEmailToQueue(message);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_USER, ex.Message);
                LogHelper.Log(Log.Event.ADD_USER, ex.StackTrace);
                // return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    success = false,
                    message = "" +ex.Message,
                    data =new
                    { }
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = true,
                message = "User added successfully.",
                data = db.UserProfile.Where(x=>x.OrganizationID==UserProfile.OrganizationID).Select(x => new
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
                    LogHelper.Log(Log.Event.INITIATE_PASSWORD_RESET, "Email does not exist");
                    return Json(new
                    {
                        success = false,
                        message = "Email does not exist! Please check and try again.",
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var PasswordToken =  await Repository.GeneratePasswordToken(user.Id);
                string RecipientEmail = UserEmail;
                string Subject = "Password Reset";
                string Body = new EmailTemplateHelper().GetTemplateContent("PasswordResetTemplate");
                var url = System.Web.HttpContext.Current.Request.Url.Host;
                string newTemplateContent = string.Format(Body,"http://"+ url + "/#/resetpassword/"+UserEmail +"/"+ PasswordToken);
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
                    message = "" + ex.Message+ex.StackTrace,
                    data =new
                    { }
                }, JsonRequestBehavior.AllowGet);
            }
            var getUserOrganizationID = db.UserProfile.Where(x => x.UserEmail == UserEmail).Select(x => x.OrganizationID).FirstOrDefault();
            return Json(new
            {
                success = true,
                message = "Please check your email to reset password.",
                data = db.UserProfile.Where(x=>x.OrganizationID==getUserOrganizationID).Select(x => new
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
                    LogHelper.Log(Log.Event.RESET_PASSWORD, "Email does not exist");
                    return Json(new
                    {
                        success = false,
                        message = "Email does not exist! Please check and try again.",
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var result = await Repository.ResetPassword(ResetPassword.ResetToken,ResetPassword.NewPassword, user.Id);
            }
            catch (Exception ex)
            {
              LogHelper.Log(Log.Event.RESET_PASSWORD, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data =  new
                    { }
                }, JsonRequestBehavior.AllowGet);
            }
            var getUserOrganizationID = db.UserProfile.Where(x => x.UserEmail == ResetPassword.UserEmail).Select(x => x.OrganizationID).FirstOrDefault();
            return Json(new
            {
                success = true,
                message = "Password reset successful.",
                data = db.UserProfile.Where(x => x.OrganizationID == getUserOrganizationID).Select(x => new
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
                if (UserProfile.OrganizationID != null)
                {
                    Guid guidID = new Guid();
                    var OrganizationID = Convert.ToString(UserProfile.OrganizationID);
                    try
                    {
                        guidID = Guid.Parse(OrganizationID);
                    }
                    catch (FormatException ex)
                    {
                        LogHelper.Log(Log.Event.SIGN_UP, "Invalid GUID format(OrganizationID)");
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Error(ex.Message);
                    }
                }
                else
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "OrganizationID is null.");
                    return Json(new
                    {
                        success = false,
                        message = "OrganizationID is null.",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var CheckIfUserHasSignedUp = db.AspNetUsers.Where(x => x.UserName == UserProfile.UserEmail).Select(x => x.UserName).FirstOrDefault();
                if (CheckIfUserHasSignedUp != null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "Email already exists on AspNetUser table.");
                    return Json(new
                    {
                        success = false,
                        message = "Email has already signed up! Please use a different email address.",
                        data =new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                if (UserProfile.UserEmail == null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "Email can not be null");
                   // return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Please Input your email");
                    return Json(new
                    {
                        success = false,
                        message = "Please input your email",
                        data =  new
                        { }
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
                    LogHelper.Log(Log.Event.SIGN_UP, "Email does not exist on UserProfile table");
                    return Json(new
                    {
                        success = false,
                        message = "Admin has not sent an invitation to this email.",
                        data = new
                        { }
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
                    data = new
                    { }
                }, JsonRequestBehavior.AllowGet);
            }
            var getUserOrganizationID = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.OrganizationID).FirstOrDefault();
            return Json(new
            {
                success = true,
                message = "Sign up successful.",
                data = db.UserProfile.Where(x => x.OrganizationID == UserProfile.OrganizationID).Select(x => new
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
        [Providers.Authorize]
        public ActionResult EditUser(UserProfile UserProfile)
        {
            try
            {
                if (UserProfile.OrganizationID != null)
                {
                    Guid guidID = new Guid();
                    var OrganizationID = Convert.ToString(UserProfile.OrganizationID);
                    try
                    {
                        guidID = Guid.Parse(OrganizationID);
                    }
                    catch (FormatException ex)
                    {
                        LogHelper.Log(Log.Event.EDIT_USER, "Invalid GUID format(OrganizationID)");
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Error(ex.Message);
                    }
                }
                else
                {
                    LogHelper.Log(Log.Event.EDIT_USER, "OrganizationID is null.");
                    return Json(new
                    {
                        success = false,
                        message = "OrganizationID is null.",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                if (UserProfile.DepartmentID == null)
                {
                    LogHelper.Log(Log.Event.EDIT_USER, "Department ID is null");
                    return Json(new
                    {
                        success = false,
                        message = "No Department is Selected",
                        data =  new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                if (UserProfile.UserID == null)
                {
                    LogHelper.Log(Log.Event.EDIT_USER, "UserID is Null");
                    return Json(new
                    {
                        success = false,
                        message = "UserID is Null",
                        data =  new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }

                var Id= db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                if (Id == null)
                {
                    EditUserWithoutID(UserProfile);

                    var getUserOrganizationID = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.OrganizationID).FirstOrDefault();
                    return Json(new
                    {
                        success = true,
                        message = "Edited successfully.",
                        data = db.UserProfile.Where(x => x.OrganizationID == UserProfile.OrganizationID).Select(x => new
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
                        message = "Edited Successfully.",
                        data = db.UserProfile.Where(x => x.OrganizationID == UserProfile.OrganizationID).Select(x => new
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
                    message = "" + ex.Message + ex.StackTrace,
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
        [Providers.Authorize]
        public ActionResult UpdateUserProfile(UserProfile UserProfile)
        {
            try
            {
                Guid guidID = new Guid();
                if (UserProfile.OrganizationID != null)
                {                    
                    var OrganizationID = Convert.ToString(UserProfile.OrganizationID);
                    try
                    {
                        guidID = Guid.Parse(OrganizationID);
                    }
                    catch (FormatException ex)
                    {
                        LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, "Invalid GUID format(OrganizationID)");
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Error(ex.Message);
                    }
                }
                else
                {
                    LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, "OrganizationID is null.");
                    return Json(new
                    {
                        success = false,
                        message = "OrganizationID is null.",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }

                var Id = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                if(Id == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, "Id is Null(Not yet signed up)");
                    return Json(new
                    {
                        success = false,
                        message = "Not yet signed up",
                        data = new
                        { }
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
                return Json(new
                {
                    success = true,
                    message = "Profile update successful.",
                    data = db.UserProfile.Where(x => x.OrganizationID == guidID).Select(x => new
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
            catch(Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, ex.Message);
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data =  new
                    { }
                }, JsonRequestBehavior.AllowGet);
            }

          
        }

        //PUT: Users/UpdateDepartmentHead
        [Providers.Authorize]
        [HttpPut]
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
                        data = new
                        { }
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
                Guid guidID = new Guid();
                if (UserProfile.OrganizationID != null)
                {
                    var OrganizationID = Convert.ToString(UserProfile.OrganizationID);
                    try
                    {
                        guidID = Guid.Parse(OrganizationID);
                    }
                    catch (FormatException ex)
                    {
                        LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, "Invalid GUID format(OrganizationID)");
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Error(ex.Message);
                    }
                }
                else
                {
                    LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, "OrganizationID is null.");
                    return Json(new
                    {
                        success = false,
                        message = "OrganizationID is null.",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    success = true,
                    message = "User added as department head successful.",
                    data = db.UserProfile.Where(x => x.OrganizationID == guidID).Select(x => new
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

        [HttpGet]
        [Providers.Authorize]
        public ActionResult GetAllUsers(string id = "", string id2="")
        {
            Guid guidID2 = new Guid();
            try
            {
                guidID2 = Guid.Parse(id2);
            }
            catch (FormatException ex)
            {
                return Json(new
                {
                    success = false,
                    message = "" + ex.Message,
                    data = new
                    { }
                }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(id))
            {
                return Json(new
                {
                    success = true,
                    message = "",
                    data = db.UserProfile.Where(x => x.OrganizationID == guidID2).Select(x => new
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
                        data =new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    success = true,
                    message = "All Users",
                    data = db.UserProfile.Where(x => x.DepartmentID == guidID &&  x.OrganizationID == guidID2).Select(x => new
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

        [HttpGet]
        public ActionResult GetUrl()
        {
            var url = System.Web.HttpContext.Current.Request.Url.Host;
            return Json(url, JsonRequestBehavior.AllowGet);
        }
      
        [HttpDelete]
        [Providers.Authorize]
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
                        data = new
                        {}
                    }, JsonRequestBehavior.AllowGet);
                }
            var UserAspNetID = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                if (UserAspNetID == null)
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
                    Guid guidID = new Guid();
                    var OrganizationID = Convert.ToString(UserProfile.OrganizationID);
                    try
                    {
                        guidID = Guid.Parse(OrganizationID);
                    }
                    catch (FormatException ex)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Error(ex.Message);
                    }

                    return Json(new
                    {
                        success = true,
                        message = "User is deleted suessfully",
                        data = db.UserProfile.Where(x => x.OrganizationID == guidID).Select(x => new
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
                    UserProfile profile = db.UserProfile.SingleOrDefault(x => x.UserID == UserProfile.UserID);
                    db.UserProfile.Remove(profile);
                    AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == UserAspNetID);
                    db.AspNetUserRoles.Remove(role);
                    AspNetUsers users = db.AspNetUsers.SingleOrDefault(x => x.Id == UserAspNetID);
                    db.AspNetUsers.Remove(users);
                    db.SaveChanges();
                    Guid guidID = new Guid();
                    var OrganizationID = Convert.ToString(UserProfile.OrganizationID);
                    try
                    {
                        guidID = Guid.Parse(OrganizationID);
                    }
                    catch (FormatException ex)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Error(ex.Message);
                    }

                    return Json(new
                    {
                        success = true,
                        message = "User is deleted suessfully",
                        data = db.UserProfile.Where(x => x.OrganizationID == guidID).Select(x => new
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

        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
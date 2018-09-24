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
using ProcureEaseAPI.Controllers;
using System.Data.Entity;

namespace ProcureEaseAPI.Controllers
{
    public class UsersController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        private CatalogsController catalog = new CatalogsController();

        //POST: Users/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(string UserName, string Password)
        {
            try
            {
                var subDomain = GetSubDomainForUser(UserName);
                var tenantId = GetTenantIDForUser(UserName);
                if (tenantId == null || subDomain == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = ((tenantId==null) ? "TenantID" : "SubDomain") + " is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
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
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var User = db.AspNetUsers.Where(x => x.UserName == UserName).FirstOrDefault();
                    return Json(new
                    {
                        success = true,
                        message = "Login successful",
                        data = new
                        {
                            User.Id,
                            Email = User.UserName,
                            DepartmentID = db.UserProfile.Where(x => x.UserEmail == UserName).Select(x => x.DepartmentID).FirstOrDefault(),
                            Role = db.AspNetUserRoles.Where(x => x.UserId == User.Id).Select(x => x.AspNetRoles.Name).FirstOrDefault(),
                            BudgetYear = DateTimeSettings.CurrentYear(),
                            SubDomain = subDomain,
                            OrganizationName = db.UserProfile.Where(x => x.UserEmail == UserName).Select(x => x.OrganizationSettings.OrganizationNameAbbreviation).FirstOrDefault(),
                            OrganizationID = db.UserProfile.Where(x => x.UserEmail == UserName).Select(x => x.OrganizationID).FirstOrDefault(),
                            token = token
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
            {
                LogHelper.Log(Log.Event.LOGIN, ex.Message);
                return ExceptionError(ex.Message, ex.StackTrace);
            }
        }

        public Guid? GetTenantIDForUser(string email)
        {
            List<UserProfile> users = db.UserProfile.Where(x => x.UserEmail == email).ToList();
            if (users != null && users.Count != 0)
            {
                return users[0].TenantID;
            }
            return null;
        }

        public string GetSubDomainForUser(string email)
        {
            List<UserProfile> users = db.UserProfile.Where(x => x.UserEmail == email).ToList();
            if (users != null && users.Count != 0 && users[0].Catalog != null)
            {
                return users[0].Catalog.SubDomain;
            }
            return null;
        }

        // POST: Users/Add
        [HttpPost]
       [Providers.Authorize]
       public async Task<ActionResult> Add(UserProfile UserProfile)
        {
            // TODO: Muyiwa, add implementation to get the authorized user's email from the request header using Request.Headers["email"]
            // TODO: Muyiwa, change the GetTenantIDFromClientURL() implementation to GetTenantIDForUser(email)
            try
            {
                string email = Request.Headers["Email"];
                var tenantId = catalog.GetTenantIDFromClientURL(email); 
                if (tenantId == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var organizationId = catalog.GetOrganizationID(email);
                if (organizationId == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "OrganizationId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                UserProfile.TenantID = tenantId;
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
                if (UserProfile.DepartmentID == new Guid())
                {
                    LogHelper.Log(Log.Event.ADD_USER, "DepartmentID is null");
                    return Json(new
                    {
                        success = false,
                        message = "DepartmentID is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var CheckIfEmailExist = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.UserEmail).FirstOrDefault();
                if(CheckIfEmailExist != null)
                {
                    LogHelper.Log(Log.Event.ADD_USER, "User email already exists");
                    Response.StatusCode = (int)HttpStatusCode.Conflict;
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
                var url = System.Web.HttpContext.Current.Request.Url.Host;
                string newTemplateContent = string.Format(Body," " + "http://" + url + "/#/signup/" + UserProfile.UserEmail);
                Message message = new Message( RecipientEmail,Subject, newTemplateContent);
                EmailHelper emailHelper = new EmailHelper();
                await emailHelper.AddEmailToQueue(message);
                return Json(new
                {
                    success = true,
                    message = "User added successfully.",
                    data = db.UserProfile.Where(x => x.TenantID == tenantId).Select(x => new
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
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_USER, ex.Message);
                LogHelper.Log(Log.Event.ADD_USER, ex.StackTrace);
                // return Json(ex.Message + ex.StackTrace, JsonRequestBehavior.AllowGet);
                return ExceptionError(ex.Message, ex.StackTrace);
            }
          
        }

          // Users/InitiatePasswordReset
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> InitiatePasswordReset(string UserEmail)
        {
            try
            {
                string email = Request.Headers["Email"];
                var SubDomain = catalog.GetSubDomainFromClientURL(email);
                var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                AuthRepository Repository = new AuthRepository();
                ApplicationUser user = await Repository.FindEmail(UserEmail);
                if (user == null)
                {
                    LogHelper.Log(Log.Event.INITIATE_PASSWORD_RESET, "Email does not exist");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
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
                return Json(new
                {
                    success = true,
                    message = "Please check your email to reset password.",
                    data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
            catch(Exception ex)
            {
                LogHelper.Log(Log.Event.INITIATE_PASSWORD_RESET, ex.Message);
                return ExceptionError(ex.Message, ex.StackTrace);
            } 

        }

        // Users/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel ResetPassword)
        {
            try
            {
                string email = Request.Headers["Email"];
                var SubDomain = catalog.GetSubDomainFromClientURL(email);
                var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                AuthRepository Repository = new AuthRepository();
                ApplicationUser user = await Repository.FindEmail(ResetPassword.UserEmail);
                if (user == null)
                {
                    LogHelper.Log(Log.Event.RESET_PASSWORD, "Email does not exist");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "Email does not exist! Please check and try again.",
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var result = await Repository.ResetPassword(ResetPassword.ResetToken,ResetPassword.NewPassword, user.Id);
                return Json(new
                {
                    success = true,
                    message = "Password reset successful.",
                    data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
            catch (Exception ex)
            {
              LogHelper.Log(Log.Event.RESET_PASSWORD, ex.Message);
                return ExceptionError(ex.Message, ex.StackTrace);
            }                    

        }

        //POST: Users/SignUp
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> SignUp(UserProfile UserProfile, string Password)
        {
            try
            {
                string email = Request.Headers["Email"];
                var SubDomain = catalog.GetSubDomainFromClientURL(email);
                var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                var organizationId = catalog.GetOrganizationID(email);                
                var CheckIfUserHasSignedUp = db.AspNetUsers.Where(x => x.UserName == UserProfile.UserEmail).Select(x => x.UserName).FirstOrDefault();
                if (CheckIfUserHasSignedUp != null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "Email already exists on AspNetUser table.");
                    Response.StatusCode = (int)HttpStatusCode.Conflict;
                    return Json(new
                    {
                        success = false,
                        message = "Email has already signed up! Please use a different email address.",
                        data =new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var CheckIfUserHasBeenAddedByAdmin = db.UserProfile.Where(x => x.UserEmail == UserProfile.UserEmail).Select(x => x.UserEmail).FirstOrDefault();
                if (UserProfile.UserEmail == null || CheckIfUserHasBeenAddedByAdmin == null)
                {
                    LogHelper.Log(Log.Event.SIGN_UP, "Email is null or UserEmail does not exist UserProfile table.");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "User not yet added by admin.",
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
                var UserID = CheckIfUserIsAddedByAdmin;
                UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserID).FirstOrDefault();
                EditProfile.Id = User.Id;
                EditProfile.FirstName = UserProfile.FirstName;
                EditProfile.LastName = UserProfile.LastName;           
                db.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "Sign up successful.",
                    data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
            catch(Exception ex)
            {
                LogHelper.Log(Log.Event.SIGN_UP, ex.Message);
                return ExceptionError(ex.Message, ex.StackTrace);
            }
           
        }

 
        //PUT: Users/EditUser
        [HttpPut]
        [Providers.Authorize]
        public ActionResult EditUser(UserProfile userProfile)
        {
            try
            {
                string email = Request.Headers["Email"];
                var SubDomain = catalog.GetSubDomainFromClientURL(email);
                var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                if (userProfile.DepartmentID == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    LogHelper.Log(Log.Event.EDIT_USER, "Department ID is null");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        success = false,
                        message = "No Department is Selected",
                        data =  new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }             
                if (userProfile.UserID == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    LogHelper.Log(Log.Event.EDIT_USER, "UserID is Null");
                    return Json(new
                    {
                        success = false,
                        message = "UserID is Null",
                        data =  new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }

                var Id= db.UserProfile.Where(x => x.UserID == userProfile.UserID).Select(x => x.Id).FirstOrDefault();
                var CheckIfIdIsNull = db.UserProfile.Where(x => x.Id == Id).ToList();
                if (CheckIfIdIsNull == null && CheckIfIdIsNull.Count < 0)
                {
                    EditUserWithoutID(userProfile);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    return Json(new
                    {
                        success = true,
                        message = "Edited successfully.",
                        data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
                    var findDepartment = db.Department.Find(userProfile.DepartmentID);
                    if(findDepartment == null)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        LogHelper.Log(Log.Event.EDIT_USER, "Invalid Department with ID:" + " " + userProfile.DepartmentID);
                        return Json(new
                        {
                            success = false,
                            message = "UserID is Null",
                            data = new
                            { }
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (findDepartment.DepartmentName == "Procurement" && findDepartment.DepartmentHeadUserID != userProfile.UserID)
                    {
                        EditToProcurementOfficerRole(userProfile, Id);
                    }
                    if (findDepartment.DepartmentName == "Procurement" && findDepartment.DepartmentHeadUserID == userProfile.UserID)
                    {
                        EditToHeadOfProcumentRole(userProfile, Id);
                    }
                    if (findDepartment.DepartmentName != "Procurement" && findDepartment.DepartmentHeadUserID != userProfile.UserID)
                    {
                        EditToEmployeeRole(userProfile, Id);                    
                    }
                    if (findDepartment.DepartmentName != "Procurement" && findDepartment.DepartmentHeadUserID == userProfile.UserID)
                    {
                        EditToHeadOfDepartmentRole(userProfile, Id);
                    }
                    Response.StatusCode = (int)HttpStatusCode.OK;                  
                }
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new
                {
                    success = true,
                    message = "Edited Successfully.",
                    data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
                LogHelper.Log(Log.Event.SIGN_UP, ex.Message);
                return ExceptionError(ex.Message, ex.StackTrace);
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
            var getUserRoleId = db.AspNetUserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId);
            AspNetUserRoles role = db.AspNetUserRoles.Find(Id, getUserRoleId);
            if (role == null)
            {
                AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                EditUser.UserName = UserProfile.UserEmail;
                EditUser.Email = UserProfile.UserEmail;
                AspNetUserRoles userRole = new AspNetUserRoles();
                userRole.UserId = Id;
                userRole.RoleId = RoleId;
                db.AspNetUserRoles.Add(userRole);
                db.SaveChanges();
            }
            else
            {
                AspNetUserRoles roles = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
                db.AspNetUserRoles.Remove(role);
                AspNetUserRoles userRole = new AspNetUserRoles();
                userRole.UserId = Id;
                userRole.RoleId = RoleId;
                db.AspNetUserRoles.Add(userRole);
                AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                EditUser.UserName = UserProfile.UserEmail;
                EditUser.Email = UserProfile.UserEmail;
                db.SaveChanges();
            }
        }

        protected void EditToHeadOfProcumentRole(UserProfile UserProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Procurement Head").Select(x => x.Id).FirstOrDefault();
            UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).FirstOrDefault();
            EditProfile.UserEmail = UserProfile.UserEmail;
            EditProfile.DepartmentID = UserProfile.DepartmentID;
            var getUserRoleId = db.AspNetUserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId);
            AspNetUserRoles role = db.AspNetUserRoles.Find(Id, getUserRoleId);
            if (role == null)
            {
                AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                EditUser.UserName = UserProfile.UserEmail;
                EditUser.Email = UserProfile.UserEmail;
                AspNetUserRoles userRole = new AspNetUserRoles();
                userRole.UserId = Id;
                userRole.RoleId = RoleId;
                db.AspNetUserRoles.Add(userRole);
                db.SaveChanges();
            }
            else
            {
                AspNetUserRoles roles = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
                db.AspNetUserRoles.Remove(role);
                AspNetUserRoles userRole = new AspNetUserRoles();
                userRole.UserId = Id;
                userRole.RoleId = RoleId;
                db.AspNetUserRoles.Add(userRole);
                AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                EditUser.UserName = UserProfile.UserEmail;
                EditUser.Email = UserProfile.UserEmail;
                db.SaveChanges();
            }
        }

        protected void EditToEmployeeRole(UserProfile UserProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Employee").Select(x => x.Id).FirstOrDefault();
            UserProfile EditProfile = db.UserProfile.SingleOrDefault(x => x.UserID == UserProfile.UserID);
            EditProfile.UserEmail = UserProfile.UserEmail;
            EditProfile.DepartmentID = UserProfile.DepartmentID;
            var getUserRoleId = db.AspNetUserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId);
            AspNetUserRoles role = db.AspNetUserRoles.Find(Id,getUserRoleId);
            if(role==null)
            {
                AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                EditUser.UserName = UserProfile.UserEmail;
                EditUser.Email = UserProfile.UserEmail;
                AspNetUserRoles userRole = new AspNetUserRoles();
                userRole.UserId = Id;
                userRole.RoleId = RoleId;
                db.AspNetUserRoles.Add(userRole);
                db.SaveChanges();
            }
            else
            {
                AspNetUserRoles roles = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
                db.AspNetUserRoles.Remove(role);
                AspNetUserRoles userRole = new AspNetUserRoles();
                userRole.UserId = Id;
                userRole.RoleId = RoleId;
                db.AspNetUserRoles.Add(userRole);
                AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                EditUser.UserName = UserProfile.UserEmail;
                EditUser.Email = UserProfile.UserEmail;
                db.SaveChanges();
            }
            db.SaveChanges();
        
        }

        protected void EditToHeadOfDepartmentRole(UserProfile UserProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Head of Department").Select(x => x.Id).FirstOrDefault();
            UserProfile EditProfile = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).FirstOrDefault();
            EditProfile.UserEmail = UserProfile.UserEmail;
            EditProfile.DepartmentID = UserProfile.DepartmentID;
            var getUserRoleId = db.AspNetUserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId);
            AspNetUserRoles role = db.AspNetUserRoles.Find(Id, getUserRoleId);
            if (role == null)
            {
                AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                EditUser.UserName = UserProfile.UserEmail;
                EditUser.Email = UserProfile.UserEmail;
                AspNetUserRoles userRole = new AspNetUserRoles();
                userRole.UserId = Id;
                userRole.RoleId = RoleId;
                db.AspNetUserRoles.Add(userRole);
                db.SaveChanges();
            }
            else
            {
                AspNetUserRoles roles = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
                db.AspNetUserRoles.Remove(role);
                AspNetUserRoles userRole = new AspNetUserRoles();
                userRole.UserId = Id;
                userRole.RoleId = RoleId;
                db.AspNetUserRoles.Add(userRole);
                AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
                EditUser.UserName = UserProfile.UserEmail;
                EditUser.Email = UserProfile.UserEmail;
                db.SaveChanges();
            }
        }

        //PUT: Users/UpdateUserProfile
        [HttpPut]
        [Providers.Authorize]
        public ActionResult UpdateUserProfile(UserProfile UserProfile)
        {
            try
            {
                string email = Request.Headers["Email"];
                var SubDomain = catalog.GetSubDomainFromClientURL(email);
                var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                var Id = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                var GetId = db.UserProfile.Where(x => x.Id == Id).ToList();
                if (Id == null || GetId.Count < 0)
                {
                    LogHelper.Log(Log.Event.UPDATE_USER_PROFILE, "Id is Null(Not yet signed up)");
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new
                    {
                        success = false,
                        message = "Not yet signed up",
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                if (UserProfile.UserID == null)
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
                    data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
                return ExceptionError(ex.Message, ex.StackTrace);
            }          
        }

        //PUT: Users/UpdateDepartmentHead
        [Providers.Authorize]
        [HttpPut]
        public ActionResult UpdateDepartmentHead(UserProfile UserProfile)
        {
            try
            {
                string email = Request.Headers["Email"];
                var SubDomain = catalog.GetSubDomainFromClientURL(email);
                var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                UserProfile users = db.UserProfile.Find(UserProfile.UserID);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                if (users.UserID == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_DEPARTMENT_HEAD, "UserID is Null");
                    return Json(new
                    {
                        success = false,
                        message = "Invalid user with UserID:" + " " + UserProfile.UserID,
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                Department getUserDepartment = db.Department.Find(UserProfile.DepartmentID);
                if (getUserDepartment == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Invalid department with DepartmentID:" + " " + UserProfile.DepartmentID,
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }               
                if (getUserDepartment.DepartmentHeadUserID == UserProfile.UserID)
                {
                    Response.StatusCode = (int)HttpStatusCode.Conflict;
                    return Json(new
                    {
                        success = false,
                        message = "User already head of department" ,
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                var CheckUserDepartmentName = db.Department.Where(x => x.DepartmentID == UserProfile.DepartmentID).Select(x => x.DepartmentName).FirstOrDefault();               
                var Id = db.UserProfile.Where(x => x.UserID == UserProfile.UserID).Select(x => x.Id).FirstOrDefault();
                if (users.Id != null)
                {
                    if (CheckUserDepartmentName == "Procurement")
                    {
                       
                        var getFormerHeadOfDepartmentId = db.UserProfile.Where(x => x.UserID== getUserDepartment.DepartmentHeadUserID).Select(x => x.Id).FirstOrDefault();                                           
                        //change former head of department to procurement officer role
                        var procurementOfficerRoleId = db.AspNetRoles.Where(x => x.Name == "Procurement Officer").Select(x => x.Id).FirstOrDefault();
                        var getFormerHeadOfProcurementId = db.UserProfile.Where(x => x.UserID == getUserDepartment.DepartmentHeadUserID).Select(x => x.Id).FirstOrDefault();
                        var getFormerHeadOfProcurementRoleId = db.AspNetUserRoles.Where(x => x.UserId == getFormerHeadOfDepartmentId).Select(x => x.RoleId).FirstOrDefault();
                        AspNetUserRoles deleteFormerHeadOfProcurementRole = db.AspNetUserRoles.FirstOrDefault(x => x.RoleId == getFormerHeadOfProcurementRoleId);
                        db.AspNetUserRoles.Remove(deleteFormerHeadOfProcurementRole);
                        AspNetUserRoles createRole = new AspNetUserRoles();
                        createRole.RoleId = procurementOfficerRoleId;
                        createRole.UserId = getFormerHeadOfDepartmentId;
                        db.AspNetUserRoles.Add(createRole);
                        //set user to head of procurement
                        var roleId = db.AspNetRoles.Where(x => x.Name == "Procurement Head").Select(x => x.Id).FirstOrDefault();
                        getUserDepartment.DepartmentHeadUserID = UserProfile.UserID;
                        db.Entry(getUserDepartment).State = EntityState.Modified;
                        AspNetUserRoles userRoles = db.AspNetUserRoles.Find(Id, procurementOfficerRoleId);
                        if (userRoles == null)
                        {
                            AspNetUserRoles userRole = new AspNetUserRoles();
                            userRole.UserId = Id;
                            userRole.RoleId = roleId;
                            db.AspNetUserRoles.Add(userRole);
                            db.SaveChanges();
                        }
                        else
                        {
                            AspNetUserRoles removeRole = db.AspNetUserRoles.FirstOrDefault(x => x.UserId == Id);
                            db.AspNetUserRoles.Remove(removeRole);
                            AspNetUserRoles userRole = new AspNetUserRoles();
                            userRole.UserId = Id;
                            userRole.RoleId = roleId;
                            db.AspNetUserRoles.Add(userRole);
                            db.SaveChanges();                           
                        }
                        db.SaveChanges();
                    }
                    else
                    {                      
                        //change former head of department to employee role
                        var getFormerHeadOfDepartmentUserId = db.UserProfile.Where(x => x.UserID == getUserDepartment.DepartmentHeadUserID).Select(x => x.Id).FirstOrDefault();
                        var employeeRoleId = db.AspNetRoles.Where(x => x.Name == "Employee").Select(x => x.Id).FirstOrDefault();
                        AspNetUserRoles removeFormerHeadOfDepartmentRole = db.AspNetUserRoles.FirstOrDefault(x=>x.UserId== getFormerHeadOfDepartmentUserId);
                        db.AspNetUserRoles.Remove(removeFormerHeadOfDepartmentRole);
                        AspNetUserRoles createRole = new AspNetUserRoles();
                        createRole.RoleId = employeeRoleId;
                        createRole.UserId = getFormerHeadOfDepartmentUserId;
                        db.AspNetUserRoles.Add(createRole);

                        //set user to head of department role
                        getUserDepartment.DepartmentHeadUserID = UserProfile.UserID;
                        db.Entry(getUserDepartment).State = EntityState.Modified;
                        var roleId = db.AspNetRoles.Where(x => x.Name == "Head of Department").Select(x => x.Id).FirstOrDefault();
                        AspNetUserRoles userRoles = db.AspNetUserRoles.Find(Id,employeeRoleId);
                        if(userRoles == null)
                        {
                            AspNetUserRoles userRole = new AspNetUserRoles();
                            userRole.UserId = Id;
                            userRole.RoleId = roleId;
                            db.AspNetUserRoles.Add(userRole);
                            db.SaveChanges();
                        }
                        else
                        {
                            AspNetUserRoles removeHeadOfDepartmentRole = db.AspNetUserRoles.FirstOrDefault(x => x.UserId == Id);
                            db.AspNetUserRoles.Remove(removeHeadOfDepartmentRole);
                            db.SaveChanges();
                            AspNetUserRoles userRole = new AspNetUserRoles();
                            userRole.UserId = Id;
                            userRole.RoleId = roleId;
                            db.AspNetUserRoles.Add(userRole);
                            db.SaveChanges();
                        }
                        db.SaveChanges();
                    }
                }
                else
                {
                        Department UpdateDepartmentHead = db.Department.Where(x => x.DepartmentID == UserProfile.DepartmentID).FirstOrDefault();
                        UpdateDepartmentHead.DepartmentHeadUserID = UserProfile.UserID;
                        db.SaveChanges();
                    
                }
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new
                {
                    success = true,
                    message = "User added as department head successful.",
                    data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return ExceptionError(ex.Message, ex.StackTrace);
            }
        }

        [HttpGet]
         [Providers.Authorize]
        public ActionResult GetAllUsers(string departmentId = "")
        {
            string email = Request.Headers["Email"];
            var SubDomain = catalog.GetSubDomainFromClientURL(email);
            var tenantId = catalog.GetTenantIDFromClientURL(email);
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
            if (string.IsNullOrEmpty(departmentId))
            {
                return Json(new
                {
                    success = true,
                    message = "Users in: " + SubDomain,
                    data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
                    {
                        x.UserID,
                        FullName = x.FirstName + " " + x.LastName,
                        x.Department1.DepartmentName,
                        x.DepartmentID,
                        x.UserEmail,
                        DepartmentHeadStatus = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                    })
                }, JsonRequestBehavior.AllowGet);
            } else
            {
                Guid departmentGuidId = new Guid();
                try
                {
                    departmentGuidId = Guid.Parse(departmentId);
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
                    message = "Users in: " + SubDomain + ", in department with ID: " + departmentGuidId,
                    data = db.UserProfile.Where(x => x.DepartmentID == departmentGuidId &&  x.Catalog.SubDomain == SubDomain).Select(x => new
                    {
                        x.UserID,
                        FullName = x.FirstName + " " + x.LastName,
                        x.Department1.DepartmentName,
                        x.DepartmentID,
                        x.UserEmail,
                        DepartmentHeadStatus = db.UserProfile.Where(y => y.Department1.DepartmentHeadUserID == x.UserID).Select(y => (true) || (false)).FirstOrDefault()
                    })
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpDelete]
        [Providers.Authorize]
        public ActionResult Delete(UserProfile userProfile)
        {
            try
            {
                string email = Request.Headers["Email"];
                var SubDomain = catalog.GetSubDomainFromClientURL(email);
                var tenantId = catalog.GetTenantIDFromClientURL(email);
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
                if (userProfile.UserID==null)
            {
                LogHelper.Log(Log.Event.DELETE_USER, "UserID is Null");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        success = false,
                        message = "UserID is Null",
                        data = new
                        {}
                    }, JsonRequestBehavior.AllowGet);
                }
                var findUser = db.UserProfile.Find(userProfile.UserID);
                if(findUser == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new
                    {
                        success = false,
                        message = "Invalid User with Id:" + " " + userProfile.UserID,
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }           
                if (findUser.Id == null)
                {
                    var checkIfUserIsHeadOfDepartment = db.Department.Where(x => x.DepartmentHeadUserID == userProfile.UserID).ToList();
                    if (checkIfUserIsHeadOfDepartment != null && checkIfUserIsHeadOfDepartment.Count > 0) 
                    {
                        Department RemoveUserFromHeadOfDepartment = db.Department.SingleOrDefault(x => x.DepartmentHeadUserID == userProfile.UserID);
                        RemoveUserFromHeadOfDepartment.DepartmentHeadUserID = null;
                        db.SaveChanges();
                    }
                    UserProfile profile = db.UserProfile.SingleOrDefault(x => x.UserID == userProfile.UserID);
                    db.UserProfile.Remove(profile);
                    db.SaveChanges();                   
                    return Json(new
                    {
                        success = true,
                        message = "User is deleted successfully",
                        data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
                    UserProfile profile = db.UserProfile.SingleOrDefault(x => x.UserID == findUser.UserID);
                    db.UserProfile.Remove(profile);
                    AspNetUserRoles role = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == findUser.Id);
                    if(role == null)
                    {
                        AspNetUsers user = db.AspNetUsers.SingleOrDefault(x => x.Id == findUser.Id);
                        db.AspNetUsers.Remove(user);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.AspNetUserRoles.Remove(role);
                        AspNetUsers users = db.AspNetUsers.SingleOrDefault(x => x.Id == findUser.Id);
                        db.AspNetUsers.Remove(users);
                        db.SaveChanges();
                    }                                
                    return Json(new
                    {
                        success = true,
                        message = "User is deleted suessfully",
                        data = db.UserProfile.Where(x => x.Catalog.SubDomain == SubDomain).Select(x => new
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
                return ExceptionError(ex.Message, ex.StackTrace);
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

        private ActionResult ExceptionError(string message, string StackTrace)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new {InternalError = StackTrace }
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
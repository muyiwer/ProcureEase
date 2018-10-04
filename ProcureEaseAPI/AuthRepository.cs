using ProcureEaseAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace ProcureEaseAPI
{
    public class AuthRepository
    {
        //private ApplicationDbContext _ctx;

        //private UserManager<ApplicationUser> _userManager; 

        //public AuthRepository()
        //{
        //    _ctx = new ApplicationDbContext();
        //    _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));           
        //}
        private ProcureEaseEntities db = new ProcureEaseEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext _ctx;
        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
        }

        public AuthRepository(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public async Task<ApplicationUser> RegisterAdmin(AddUserModel userModel)
        {
            var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            return user;
        }
            public async Task<ApplicationUser> RegisterUser(AddUserModel userModel, string UserDepartment,bool IsHeadOfdepartment )
        {
            var _userManager =  new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if(UserDepartment == "Procurement" && IsHeadOfdepartment ==false)
            {
                var AddUserToProcurement = _userManager.AddToRole(user.Id, "Procurement Officer");
            }
            if(UserDepartment != "Procurement" && IsHeadOfdepartment == false)
            {
                var AddUserToEmployee = _userManager.AddToRole(user.Id, "Employee");
            }
            if (UserDepartment == "Procurement" && IsHeadOfdepartment == true)
            {
                var AddUserToProcurement = _userManager.AddToRole(user.Id, "Procurement Head");
            }
            if(UserDepartment != "Procurement" && IsHeadOfdepartment == false)
            {
                var AddUserToEmployee = _userManager.AddToRole(user.Id, "Head of Department");
            }
            return user;
        }

        public async Task<ApplicationUser> FindEmail(string UserEmail)
        {

            ApplicationUser result = await UserManager.FindByEmailAsync(UserEmail);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
            ApplicationUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public async Task<string> GeneratePasswordToken(string UserID)
        {          
            var PasswordToken = await UserManager.GeneratePasswordResetTokenAsync(UserID);
            return PasswordToken;
        }

        public async Task<IdentityResult> ResetPassword(string ResetToken,string NewPassword, string UserID)
        {           
            var PasswordToken = await UserManager.ResetPasswordAsync(UserID, ResetToken,NewPassword);
            return PasswordToken;
        }

        public void EditUserIdentity(UserProfile userProfile, string Id)
        {
            AspNetUsers EditUser = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
            EditUser.UserName = userProfile.UserEmail;
            EditUser.Email = userProfile.UserEmail;
        }

        public void RemoveUserIdentity(string Id)
        {
            AspNetUsers user = db.AspNetUsers.SingleOrDefault(x => x.Id == Id);
            db.AspNetUsers.Remove(user);
        }

        public void CreateRole(string Id, string RoleId)
        {
            AspNetUserRoles userRole = new AspNetUserRoles();
            userRole.UserId = Id;
            userRole.RoleId = RoleId;
            db.AspNetUserRoles.Add(userRole);
        }

        public void RemoveUserFromCurrentRole(string Id)
        {
            AspNetUserRoles roles = db.AspNetUserRoles.SingleOrDefault(x => x.UserId == Id);
            db.AspNetUserRoles.Remove(roles);
        }

        public void EditToProcurementOfficerRole(UserProfile userProfile,string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Procurement Officer").Select(x => x.Id).FirstOrDefault();
            var getUserRoleId = db.AspNetUserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).FirstOrDefault();
            AspNetUserRoles findUserRole = db.AspNetUserRoles.Find(Id, getUserRoleId);
            if (findUserRole == null)
            {
                EditUserIdentity(userProfile, Id);
                CreateRole(Id, RoleId);
                db.SaveChanges();
            }
            else
            {
                RemoveUserFromCurrentRole(Id);
                CreateRole(Id,RoleId);
                EditUserIdentity(userProfile, Id);
                db.SaveChanges();
            }
            db.SaveChanges();
        }

        public void EditToHeadOfProcumentRole(UserProfile userProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Procurement Head").Select(x => x.Id).FirstOrDefault();
            var getUserRoleId = db.AspNetUserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).FirstOrDefault();
            AspNetUserRoles findUserRole = db.AspNetUserRoles.Find(Id, getUserRoleId);
            if (findUserRole == null)
            {
                EditUserIdentity(userProfile, Id);
                CreateRole(Id, RoleId);
                db.SaveChanges();
            }
            else
            {
                RemoveUserFromCurrentRole(Id);
                CreateRole(Id, RoleId);
                EditUserIdentity(userProfile, Id);
                db.SaveChanges();
            }
        }


        public void EditToEmployeeRole(UserProfile userProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Employee").Select(x => x.Id).FirstOrDefault();
            var getUserRoleId = db.AspNetUserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).FirstOrDefault();
            AspNetUserRoles findUserRole = db.AspNetUserRoles.Find(Id, getUserRoleId);
            if (findUserRole == null)
            {
                EditUserIdentity(userProfile, Id);
                CreateRole(Id, RoleId);
                db.SaveChanges();
            }
            else
            {
                RemoveUserFromCurrentRole(Id);
                CreateRole(Id, RoleId);
                EditUserIdentity(userProfile, Id);
                db.SaveChanges();
            }
            db.SaveChanges();

        }

        public void EditToHeadOfDepartmentRole(UserProfile userProfile, string Id)
        {
            var RoleId = db.AspNetRoles.Where(x => x.Name == "Head of Department").Select(x => x.Id).FirstOrDefault();
            var getUserRoleId = db.AspNetUserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).FirstOrDefault();
            AspNetUserRoles findUserRole = db.AspNetUserRoles.Find(Id, getUserRoleId);
            if (findUserRole == null)
            {
                EditUserIdentity(userProfile, Id);
                CreateRole(Id, RoleId);
                db.SaveChanges();
            }
            else
            {
                RemoveUserFromCurrentRole(Id);
                CreateRole(Id, RoleId);
                EditUserIdentity(userProfile, Id);
                db.SaveChanges();
            }
            db.SaveChanges();
        }


        public void Dispose()
        {
            _ctx.Dispose();
            UserManager.Dispose();

        }
    }
}
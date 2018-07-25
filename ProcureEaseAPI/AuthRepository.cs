using ProcureEaseAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProcureEaseAPI
{
    public class AuthRepository
    {
        private ApplicationDbContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(AddUserModel userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<ApplicationUser> FindEmail(string UserEmail)
        {

            ApplicationUser result = await _userManager.FindByEmailAsync(UserEmail);

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public async Task<string> GeneratePasswordToken(string UserID)
        {
            var PasswordToken = await _userManager.GeneratePasswordResetTokenAsync(UserID);

            return PasswordToken;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordModel ResetPassword, string UserId)
        {
            var PasswordToken = await _userManager.ResetPasswordAsync(UserId, ResetPassword.ResetToken, ResetPassword.NewPassword);

            return PasswordToken;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}
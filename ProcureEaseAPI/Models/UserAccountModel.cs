using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProcureEaseAPI.Models
{
    public class ListUserModel
    {
        public IList<ApplicationUser> users { get; set; }
        public IList<string> roles { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberBrowser { get; set; }
        public bool RememberMe { get; set; }
    }

    public class PasswordModel
    {
        [Required]
        public string Email { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class Token
    {
      public string  access_token { get; set; }
      public string token_type { get; set; }
      public string expires_in{ get; set; }
    }

    public class AddUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        //   [DataType(DataType.Password)]
        //  [Display(Name = "Confirm password")]
        //  [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //   public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        [EmailAddress]
        public string UserEmail { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        public string ResetToken { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
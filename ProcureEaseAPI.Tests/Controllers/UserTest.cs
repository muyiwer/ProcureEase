using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcureEaseAPI.Controllers;
using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
   public  class UserTest
    {
        [TestMethod]
        public void TestAddUser()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com"
            };      
            var testAddUser = new UsersController();
            var result = testAddUser.Add(UserProfile);
            Assert.IsNull(result);
            
        }

        [TestMethod]
        public void TestInitiatePasswordReset()
        {
            string UserEmail = "muyiweraro@gmail.com";           
            var testAddUser = new UsersController();
            var result = testAddUser.InitiatePasswordReset(UserEmail);
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void TestPasswordReset()
        //{
        //    ResetPasswordModel ResetPassword = new ResetPasswordModel
        //    {
        //        UserEmail = "muyiweraro@gmail.com",
        //        NewPassword ="",
        //        ResetToken ="",
        //    };
        //    var testAddUser = new UsersController();
        //    var result = testAddUser.ResetPassword(ResetPassword);
        //    Assert.IsNotNull(result);

        //}

        [TestMethod]
        public void TestSignUp()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com",
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            string Password = "Muyiwer87";
            var testSignUp = new UsersController();
            var result = testSignUp.SignUp(UserProfile, Password);
            Assert.IsNotNull(result);
        }
    }
}

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

        [TestMethod]
        public void TestDelete()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("5C99B26F-CBA8-493E-ABD4-E049BB548DB5"),
               
            };
            var testDelete = new UsersController();
            var result = testDelete.Delete(UserProfile);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
           
            string id= "5C99B26F-CBA8-493E-ABD4-E049BB548DB5";
            var GetAllUsers = new UsersController();
            var result = GetAllUsers.GetAllUsers(id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestUpdateDepartmentHead()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("5C99B26F-CBA8-493E-ABD4-E049BB548DB5"),
                DepartmentID = new Guid("86CAF117-37ED-4370-AC31-0D86EECAD8ED"),
                LastName = "Aro"
            };
            var testUpdateDepartmentHead = new UsersController();
            var result = testUpdateDepartmentHead.UpdateDepartmentHead(UserProfile);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestUpdateUserProfile()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("5C99B26F-CBA8-493E-ABD4-E049BB548DB5"),
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            var testUpdateUserProfile = new UsersController();
            var result = testUpdateUserProfile.UpdateUserProfile(UserProfile);
            Assert.IsNotNull(result);
        }
    }
}

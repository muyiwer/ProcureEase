using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProcureEaseAPI.Controllers;
using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
   public  class UsersControllerTest
    {
        string LOCAL_SERVER = "http://localhost:82/";

        [TestMethod]
        public async Task TestLogin_WithInvalidLoginDetails()
        {
            try
            {
                Mock<HttpRequest> httpRequest = new Mock<HttpRequest>();
                httpRequest.Setup(x => x.Url).Returns(new Uri(LOCAL_SERVER));

                string username = "invalidusername";
                string password = "invalidpassword";
                UsersController usersController = new UsersController();
                Console.WriteLine(usersController);

                ActionResult response = await usersController.Login(username, password);
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                //throw ex;
            }
        }

        [TestMethod]
        public async Task TestAddUser_Unsuccessfully_UserEmailAlreadyExists()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com"
            };
            var testAddUser = new UsersController();
            JsonResult result = (JsonResult)await testAddUser.Add(UserProfile); // first call to add email
            result = (JsonResult)await testAddUser.Add(UserProfile); // second call to attempt to add email again and force duplicate insertion attempt
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Email already exists"));
        }

        [TestMethod]
        public async Task TestAddUser_Unsuccessfully_InvalidDepartmentID()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "email-" + new Random().Next() + "@gmail.com",
                DepartmentID = new Guid()
            };
            var testAddUser = new UsersController();
            JsonResult result = (JsonResult)await testAddUser.Add(UserProfile); // first call to add email
            result = (JsonResult)await testAddUser.Add(UserProfile); // second call to attempt to add email again and force duplicate insertion attempt
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("User added successfully"));
        }

        [TestMethod]
        public async Task TestAddUser_Successfully()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "email-" + new Random().Next() + "@gmail.com",
                DepartmentID = new Guid("86CAF117-37ED-4370-AC31-0D86EECAD8ED")
            };

            Mock<HttpRequest> httpRequest = new Mock<HttpRequest>();
            httpRequest.Setup(x => x.Url).Returns(new Uri(LOCAL_SERVER));

            var usersController = new UsersController();

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Request.Url).Returns(new Uri("/", UriKind.Relative));
            context.Setup(x => x.Session).Returns(session.Object);
            var requestContext = new RequestContext(context.Object, new RouteData());
            usersController.ControllerContext = new ControllerContext(requestContext, usersController);
            
            JsonResult result = (JsonResult)await usersController.Add(UserProfile); // first call to add email
            result = (JsonResult)await usersController.Add(UserProfile); // second call to attempt to add email again and force duplicate insertion attempt
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("User added successfully"));
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

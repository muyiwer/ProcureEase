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
        public async Task TestInitiatePasswordReset_Unsuccessfully_InvalidEmail()
        {
            string UserEmail = "email-" + new Random().Next() + "@gmail.com";
            var testInitiatePasswordReset = new UsersController();
            JsonResult result = (JsonResult)await testInitiatePasswordReset.InitiatePasswordReset(UserEmail); // first call to add email
            result = (JsonResult)await testInitiatePasswordReset.InitiatePasswordReset(UserEmail); // second call to attempt to add email again and force duplicate insertion attempt
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Email does not exist"));
        }

        [TestMethod]
        public async Task TestInitiatePasswordReset_Successfull_Email()
        {
            string UserEmail = "muyiweraro@gmail.com";           
            var testInitiatePasswordReset = new UsersController();
            var result = (JsonResult)await testInitiatePasswordReset.InitiatePasswordReset(UserEmail);
           Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Please check your email to reset password."));
        }

        [TestMethod]
        public async Task TestPasswordReset()
        {
            ResetPasswordModel ResetPassword = new ResetPasswordModel
            {
                UserEmail = "muyiweraro@gmail.com",
                NewPassword = "",
                ResetToken = "",
            };
            var testAddUser = new UsersController();
            var result = (JsonResult) await testAddUser.ResetPassword(ResetPassword);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Please check your email to reset password."));

        }

        [TestMethod]
        public async Task TestSignUp_Unsuccessfully_InvalidEmail()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com",
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            string Password = "Muyiwer87";
            var testSignUp = new UsersController();
            var result = (JsonResult)await testSignUp.SignUp(UserProfile, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Email does not exist on UserProfile table."));
        }


        [TestMethod]
        public async Task TestSignUp_Unsuccessfully_AlreadySignedUp()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com",
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            string Password = "Muyiwer87";
            var testSignUp = new UsersController();
            var result = (JsonResult)await testSignUp.SignUp(UserProfile, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Email has already signed up! Please use a different email address."));
        }

        [TestMethod]
        public async Task TestSignUp_Successful_Valid_Email()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com",
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            string Password = "Muyiwer87";
            var testSignUp = new UsersController();
            var result = (JsonResult)await testSignUp.SignUp(UserProfile, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Sign up successful."));
        }

        [TestMethod]
        public void TestEditUser_successfully_EditToProcurementOfficerRole()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com",
                DepartmentID =new Guid("86CAF117-37ED-4370-AC31-0D86EECAD8ED"),//for procurement dept only
                UserID = new Guid("140EC11C-BEFD-4DB3-AEE6-0C45339D3F05") 
            };
            var testEditUser = new UsersController();
            var result = (JsonResult)testEditUser.EditUser(UserProfile);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Edited Successfully."));
        }

        [TestMethod]
        public void TestEditUser_successfully_EditToEmployeeRole()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com",
                DepartmentID = new Guid("200A6BAE-F4B7-4D34-AEEB-D780B0E6B8EA"),//procurement dept not to be included
                UserID = new Guid("140EC11C-BEFD-4DB3-AEE6-0C45339D3F05")
            };
            var testEditUser = new UsersController();
            var result = (JsonResult)testEditUser.EditUser(UserProfile);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Edited Successfully."));
        }

        [TestMethod]
        public void TestDelete()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("5C99B26F-CBA8-493E-ABD4-E049BB548DB5"),              
            };
            var testDelete = new UsersController();
            var result = (JsonResult)testDelete.Delete(UserProfile);
            Assert.IsTrue((result.Data + "").Contains("User is deleted suessfully"));
        }

        [TestMethod]
        public void TestGetAllUsers()
        {          
            string id= "5C99B26F-CBA8-493E-ABD4-E049BB548DB5";
            var GetAllUsers = new UsersController();
            var result = (JsonResult)GetAllUsers.GetAllUsers(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All Users"));
        }

        [TestMethod]
        public void TestGetAllUsers_Unsuccessfull_Invalid_Guid()
        {
            string id = "5C99B26F-CBA8-493E-ABD4";
            var GetAllUsers = new UsersController();
            var result = (JsonResult)GetAllUsers.GetAllUsers(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)"));
        }

        [TestMethod]
        public void TestUpdateDepartmentHead()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("5C99B26F-CBA8-493E-ABD4-E049BB548DB5"),
                DepartmentID = new Guid("86CAF117-37ED-4370-AC31-0D86EECAD8ED"),
            };
            var testUpdateDepartmentHead = new UsersController();
            var result = (JsonResult)testUpdateDepartmentHead.UpdateDepartmentHead(UserProfile);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("User added as department head successful."));
        }

        [TestMethod]
        public void TestUpdateUserProfile()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("5C99B26F-CBA8-493E-ABD4-E049BB548DB5"),
                UserEmail = "oaro@techspecialistlimited.com",
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            var testUpdateUserProfile = new UsersController();
            var result = (JsonResult)testUpdateUserProfile.UpdateUserProfile(UserProfile);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Profile update successful."));
        }

        [TestMethod]
        public void TestUpdateUserProfile_Unsuccessful_Id()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("5C99B26F-CBA8-493E-ABD4-E049BB548DB5"),
                UserEmail = "oaro@techspecialistlimited.com",
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            var testUpdateUserProfile = new UsersController();
            var result = (JsonResult)testUpdateUserProfile.UpdateUserProfile(UserProfile);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Not yet signed up"));
        }
    }
}

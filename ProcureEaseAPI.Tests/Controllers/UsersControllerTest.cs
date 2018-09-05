using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProcureEaseAPI.Controllers;
using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing; 

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
   public class UsersControllerTest
    {
      public  string LOCAL_SERVER = "http://localhost.procureease.ng/";

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

        //[TestMethod]
        //public virtual void TestGetTenantID_Successfully()
        //{
        //    var controller = new CatalogsController();
        //    MockController(controller, LOCAL_SERVER);

        //    var host = controller.Request.Url.Host;
        //    Console.WriteLine(host);
        //    var result = controller.GetTenantID();
        //    Console.WriteLine(result);

        //    string url = System.Web.HttpContext.Current.Request.Url.Host; // expecting format nitda.procureease.ng
        //    string[] hostUrlParts = url.Split('.');// extract sub domain from URL
        //    string subDomain = hostUrlParts[0];
        //    Console.WriteLine(subDomain);

        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public virtual void TestGetTenantID_TenantIDNotFound()
        //{
        //    string LOCAL_SERVER2 = "http://ncc.procureease.ng/";

        //    var controller = new CatalogsController();
        //    MockController(controller, LOCAL_SERVER2);

        //    var host = controller.Request.Url.Host;
        //    Console.WriteLine(host);
        //    var result = controller.GetTenantID();
        //    Console.WriteLine(result);

        //    string url = System.Web.HttpContext.Current.Request.Url.Host; // expecting format nitda.procureease.ng
        //    string[] hostUrlParts = url.Split('.');// extract sub domain from URL
        //    string subDomain = hostUrlParts[0];
        //    Console.WriteLine(subDomain);

        //    Assert.IsNull(result);
        //}

        private void MockController(UsersController controller, string server)
        {
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Request.Url).Returns(new Uri(server, UriKind.Absolute));
            context.Setup(x => x.Session).Returns(session.Object);
            var requestContext = new RequestContext(context.Object, new RouteData());
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            HttpContext.Current = new HttpContext(new HttpRequest("", server, ""), new HttpResponse(new StringWriter()));
        }

        [TestMethod]
        public async Task TestAddUser_Unsuccessfully_UserEmailAlreadyExists()
        {
            var testAddUser = new UsersController();
            MockController(testAddUser, LOCAL_SERVER);
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "oaro@techspecialistlimited.com",
            };
           
            JsonResult result = (JsonResult)await testAddUser.Add(UserProfile); // first call to add email
            result = (JsonResult)await testAddUser.Add(UserProfile); // second call to attempt to add email again and force duplicate insertion attempt
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Email already exists! Please check and try again."));
        }

        [TestMethod]
        public async Task TestAddUser_Unsuccessfully_InvalidDepartmentID()
        {
            var testAddUser = new UsersController();
            MockController(testAddUser, LOCAL_SERVER);
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "email-" + new Random().Next() + "@gmail.com",
                DepartmentID = new Guid()
            };          
            JsonResult result = (JsonResult)await testAddUser.Add(UserProfile); // first call to add email
            result = (JsonResult)await testAddUser.Add(UserProfile); // second call to attempt to add email again and force duplicate insertion attempt
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("DepartmentID is null"));
        }

        [TestMethod]
        public async Task TestAddUser_Successfully()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "email-" + new Random().Next() + "@gmail.com",
                DepartmentID = new Guid("8DEFC11D-5595-41DD-87A9-2A0EF5FE04B8")
            };


            var usersController = new UsersController();
            MockController(usersController, LOCAL_SERVER);
            JsonResult result = (JsonResult)await usersController.Add(UserProfile); // first call to add email
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("User added successfully"));
        }

        [TestMethod]
        public async Task TestInitiatePasswordReset_Unsuccessfully_InvalidEmail()
        {
            string UserEmail = "email-" + new Random().Next() + "@gmail.com";
            var testInitiatePasswordReset = new UsersController();
            MockController(testInitiatePasswordReset, LOCAL_SERVER);
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
            MockController(testInitiatePasswordReset, LOCAL_SERVER);
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
            MockController(testAddUser, LOCAL_SERVER);
            var result = (JsonResult) await testAddUser.ResetPassword(ResetPassword);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Please check your email to reset password."));

        }

        [TestMethod]
        public async Task TestSignUp_Unsuccessfully_InvalidEmail()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "ro@gmail.com",
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            string Password = "Muyiwer87";
            var testSignUp = new UsersController();
            MockController(testSignUp, LOCAL_SERVER);
            var result = (JsonResult)await testSignUp.SignUp(UserProfile, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("User not yet added by admin."));
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
            MockController(testSignUp, LOCAL_SERVER);
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
            MockController(testSignUp, LOCAL_SERVER);
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
                DepartmentID =new Guid("09C9DE6E-5D1B-4E77-BF80-A44AF73D7E7A"),//for procurement dept only
                UserID = new Guid("472BAD81-FF85-4BE6-9238-BC8306493CB9") 
            };
            var testEditUser = new UsersController();
            MockController(testEditUser, LOCAL_SERVER);
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
                DepartmentID = new Guid("0B6A0615-F453-4E8C-AF68-B799117A8B1A"),//procurement dept not to be included
                UserID = new Guid("472BAD81-FF85-4BE6-9238-BC8306493CB9")
            };
            var testEditUser = new UsersController();
            MockController(testEditUser, LOCAL_SERVER);
            var result = (JsonResult)testEditUser.EditUser(UserProfile);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Edited Successfully."));
        }

        [TestMethod]
        public void TestDelete()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("6F9B517C-4F97-44BA-AB90-81046E1398FD"),              
            };
            var testDelete = new UsersController();
            MockController(testDelete, LOCAL_SERVER);
            var result = (JsonResult)testDelete.Delete(UserProfile);
            Assert.IsTrue((result.Data + "").Contains("User is deleted successfully"));
        }

        [TestMethod]
        public void TestGetAllUsers()
        {          
            string id= "0B6A0615-F453-4E8C-AF68-B799117A8B1A";
            var GetAllUsers = new UsersController();
            MockController(GetAllUsers, LOCAL_SERVER);
            var result = (JsonResult)GetAllUsers.GetAllUsers(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All Users"));
        }

        [TestMethod]
        public void TestGetAllUsers_Unsuccessfull_Invalid_Guid()
        {
            string id = "5C99B26F-CBA8-493E-ABD4";
            var GetAllUsers = new UsersController();
            MockController(GetAllUsers, LOCAL_SERVER);
            var result = (JsonResult)GetAllUsers.GetAllUsers(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)"));
        }

        [TestMethod]
        public void TestUpdateDepartmentHead()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("472BAD81-FF85-4BE6-9238-BC8306493CB9"),
                DepartmentID = new Guid("0B6A0615-F453-4E8C-AF68-B799117A8B1A"),
            };
            var testUpdateDepartmentHead = new UsersController();
            MockController(testUpdateDepartmentHead, LOCAL_SERVER);
            var result = (JsonResult)testUpdateDepartmentHead.UpdateDepartmentHead(UserProfile);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("User added as department head successful."));
        }

        [TestMethod]
        public void TestUpdateUserProfile()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserID = new Guid("472BAD81-FF85-4BE6-9238-BC8306493CB9"),
                UserEmail = "muyiweraro@gmail.com",
                FirstName = "Femi",
                LastName = "Aro"
            };
            var testUpdateUserProfile = new UsersController();
            MockController(testUpdateUserProfile, LOCAL_SERVER);
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
                UserEmail = "muyiweraro@gmail.com",
                FirstName = "Muyiwa",
                LastName = "Aro"
            };
            var testUpdateUserProfile = new UsersController();
            MockController(testUpdateUserProfile, LOCAL_SERVER);
            var result = (JsonResult)testUpdateUserProfile.UpdateUserProfile(UserProfile);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Not yet signed up"));
        }
    }
}

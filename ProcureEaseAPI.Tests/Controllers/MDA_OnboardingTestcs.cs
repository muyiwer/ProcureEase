using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcureEaseAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
    public class MDA_OnboardingTestcs
    {
        [TestMethod]
        public void TestGetRequests()
        {
            var TestGetRequests = new HomeController();
            var result = (JsonResult)TestGetRequests.OnboardingRequests();
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Ok"));
        }

        [TestMethod]
        public async Task TestOnboarding_Null_ID()
        {
            string AdministatorEmail = "";
            string Password = "";
            var TestOnboarding_For_Null_ID = new HomeController();
            JsonResult result = (JsonResult) await TestOnboarding_For_Null_ID.Onboarding(AdministatorEmail, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("RequestID is Null"));
        }

        [TestMethod]
        public async Task TestOnboarding_For_RequestForDemo()
        {
            string AdministatorEmail = "";
            string Password = "";
            var TestOnboarding_If_RequestExistsOn_RequestForDemo = new HomeController();
            JsonResult result = (JsonResult) await TestOnboarding_If_RequestExistsOn_RequestForDemo.Onboarding(AdministatorEmail, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("RequestID does not exist on RequestForDemo"));
        }

        [TestMethod]
        public async Task TestPostOnboarding_Successful()
        {
            string AdministatorEmail = "annieajek@gmail.com";
            string Password = "Password";
            var testOnboarding_Successful = new HomeController();
            JsonResult result = (JsonResult)await testOnboarding_Successful.Onboarding(AdministatorEmail, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Organization Onboarded Successfully"));
        }

        [TestMethod]
        public async Task TestOnboarding_If_OrganizationExists()
        {
            string AdministatorEmail = "annieajek@gmail.com";
            string Password = "Password";
            var TestOnboarding_If_OrganizationAlreadyExists = new HomeController();
            JsonResult result = (JsonResult)await TestOnboarding_If_OrganizationAlreadyExists.Onboarding(AdministatorEmail, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Duplicate insertion attempt, OrganizationID already exist"));
        }
    }
}

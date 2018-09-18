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
            Guid RequestID = new Guid();
            string Password = "";
            var TestOnboarding_For_Null_ID = new HomeController();
            JsonResult result = (JsonResult) await TestOnboarding_For_Null_ID.Onboarding(RequestID, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("RequestID is Null"));
        }

        [TestMethod]
        public async Task TestOnboarding_For_RequestForDemo()
        {
            Guid RequestID = new Guid();
            string Password = "";
            var TestOnboarding_If_RequestExistsOn_RequestForDemo = new HomeController();
            JsonResult result = (JsonResult) await TestOnboarding_If_RequestExistsOn_RequestForDemo.Onboarding(RequestID, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("RequestID does not exist on RequestForDemo"));
        }

        [TestMethod]
        public async Task TestPostOnboarding_Successful()
        {
            Guid RequestID = new Guid("3C0EBB6F-D4BB-46C0-A3AE-1D8FD592D459");
            string Password = "Anita";
            var testOnboarding_Successful = new HomeController();
            JsonResult result = (JsonResult)await testOnboarding_Successful.Onboarding(RequestID, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Organization Onboarded Successfully"));
        }

        [TestMethod]
        public async Task TestOnboarding_If_OrganizationExists()
        {
            Guid RequestID = new Guid("3C0EBB6F-D4BB-46C0-A3AE-1D8FD592D459");
            string Password = "Another Password";
            var TestOnboarding_If_OrganizationAlreadyExists = new HomeController();
            JsonResult result = (JsonResult)await TestOnboarding_If_OrganizationAlreadyExists.Onboarding(RequestID, Password);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Duplicate insertion attempt, OrganizationID already exist"));
        }
    }
}

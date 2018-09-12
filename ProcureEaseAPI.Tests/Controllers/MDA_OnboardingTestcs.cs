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
        public void TestOnboarding_Null_ID()
        {
            Guid? RequestID = null;
            var TestOnboarding_For_Null_ID = new HomeController();
            JsonResult result = (JsonResult)TestOnboarding_For_Null_ID.Onboarding(RequestID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("RequestID is Null"));
        }

        [TestMethod]
        public void TestOnboarding_For_RequestForDemo()
        {
            Guid RequestID = new Guid();
            var TestOnboarding_If_RequestExistsOn_RequestForDemo = new HomeController();
            JsonResult result = (JsonResult)TestOnboarding_If_RequestExistsOn_RequestForDemo.Onboarding(RequestID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("RequestID does not exist on RequestForDemo"));
        }

        [TestMethod]
        public void TestPostOnboarding_Successful()
        {
            Guid RequestID = new Guid("3C0EBB6F-D4BB-46C0-A3AE-1D8FD592D459");
            var testOnboarding_Successful = new HomeController();
            JsonResult result = (JsonResult)testOnboarding_Successful.Onboarding(RequestID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Organization Onboarded Successfully"));
        }

        [TestMethod]
        public void TestOnboarding_If_OrganizationExists()
        {
            Guid RequestID = new Guid("3C0EBB6F-D4BB-46C0-A3AE-1D8FD592D459");
            var TestOnboarding_If_OrganizationAlreadyExists = new HomeController();
            JsonResult result = (JsonResult)TestOnboarding_If_OrganizationAlreadyExists.Onboarding(RequestID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Duplicate insertion attempt, OrganizationID already exist"));
        }
    }
}

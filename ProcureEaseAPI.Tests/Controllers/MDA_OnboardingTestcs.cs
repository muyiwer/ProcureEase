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
        public void TestOnboarding_Successful()
        {

            Guid RequestID = new Guid("3C0EBB6F-D4BB-46C0-A3AE-1D8FD592D459");

            var testOnboarding_Successful = new HomeController();
            JsonResult result = (JsonResult)testOnboarding_Successful.Onboarding(RequestID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Organization Onboarded Successfully"));
        }

        [TestMethod]
        public void TestOnboarding_UnSuccessful()
        {
            Guid RequestID = new Guid("3C0EBB6F-D4BB-46C0-A3AE-1D8FD592D459");

            var testOnboarding_Successful = new HomeController();
            JsonResult result = (JsonResult)testOnboarding_Successful.Onboarding(RequestID);//Duplicate insertion attempt
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Organization Onboarded Successfully"));
        }
    }
}

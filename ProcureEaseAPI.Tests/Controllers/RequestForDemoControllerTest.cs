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

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
    public class RequestForDemoControllerTest
    {
        [TestMethod]
        public void TestGetAllRequestsForDemo()
        {
            RequestForDemo requestForDemo = new RequestForDemo
            {

            };
            var testRequestForDemo = new HomeController();
            var result = (JsonResult)testRequestForDemo.Index();
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Ok"));
        }

        [TestMethod]
        public async Task TestPostRequestForDemo()
        {
            RequestForDemo requestForDemo = new RequestForDemo
            {
                OrganizationFullName = "Central Bank of Nigeria",
                OrganizationShortName = "CBN",
                AdministratorEmail = "annieajeks@outlook.com",
                AdministratorFirstName = "Muyiwer",
                AdministratorLastName = "Aro",
                AdministratorPhoneNumber = "+2348165389524",
            };

            var testRequestForDemo = new HomeController();
            JsonResult result = (JsonResult)await testRequestForDemo.RequestForDemo(requestForDemo);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Request Sent Successfully"));
        }
    }
}

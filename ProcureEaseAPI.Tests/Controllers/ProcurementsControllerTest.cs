using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcureEaseAPI.Controllers;
using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
    public class ProcurementsControllerTest
    {

        [TestMethod]
        public void TestDraftNeedsSummary()
        {
            string id = "3A380F88-6B8B-40AF-AA14-DF546CEC1AA6";
            var testDraftNeedsSummary = new ProcurementsController();
            JsonResult result = (JsonResult) testDraftNeedsSummary.DraftNeedsSummary(id);            
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Drafted procurement Summary"));   
        }

     
        [TestMethod]
        public void TestDraftNeedsSummary_Invalid_Guid()
        {
            string id = "3A380F88-6B8B-40AF-AA14";
            var testDraftNeedsSummary = new ProcurementsController();
            JsonResult result = (JsonResult)testDraftNeedsSummary.DraftNeedsSummary(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)"));
        }

        [TestMethod]
        public void TestDraftNeedsSummary_Null_ID()
        {
            string id = "";
            var testDraftNeedsSummary = new ProcurementsController();
            JsonResult result = (JsonResult)testDraftNeedsSummary.DraftNeedsSummary(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("DepartmentID is Null"));
        }

        [TestMethod]
        public void TestGetDraftNeeds()
        {
            string id = "3A380F88-6B8B-40AF-AA14-DF546CEC1AA6";
            string id2 = "452BE391-2A89-4212-A5C8-B0A0BB37DFE3";
            var testDraftNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testDraftNeeds.DraftNeeds(id, id2);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Drafted procurement"));
        }

        [TestMethod]
        public void TestSendProcurementNeeds()
        {
            Guid DepartmentID = new Guid("5C99B26F-CBA8-493E-ABD4-E049BB548DB5");
            int BudgetYear = 2018;
            var Items = new
            {
                ItemID = new Guid()
            };
            List<DepartmentProject> Projects = new List<DepartmentProject>
            {
               new DepartmentProject
               {
                   ProjectCategoryID = new Guid(),
                   ProjectName = "",
                   ProcurementMethodID = new Guid(),
                   ProcurementID = new Guid(),
                  
               },
               new DepartmentProject
               {
                   ProjectCategoryID = new Guid(),
                   ProjectName = "",
                   ProcurementMethodID = new Guid(),
                   ProcurementID = new Guid(),
                   
               }

            };
            var testDraftNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testDraftNeeds.SendProcurementNeeds(DepartmentID, BudgetYear, Projects);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Drafted procurement"));
        }

       
   
    }
}

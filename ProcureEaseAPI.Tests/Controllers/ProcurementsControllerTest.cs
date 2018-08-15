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
            Guid DepartmentID = new Guid("066AF5C4-3F41-419E-ADD5-77E8EC0E1E93");
            int BudgetYear = 2018;
            DepartmentItems Items = new DepartmentItems();
            Items.Quantity = 2000;
            Items.UnitPrice = 5000;
            Items.ItemName = "Office stationaries";
            Items.ItemCodeID = new Guid("93E6FFA5-E700-45B0-BDE7-6B3570DAB27B");
            DepartmentProject project = new DepartmentProject();
            project.ProcurementMethodID = new Guid("3D30C690-F37B-43D9-A17C-D47A4F5DA127");
            project.ProjectName = "Procurement of Office stationary";
            project.ProjectCategoryID = new Guid();
            project.Items.Add(Items);
            List<DepartmentProject> Projects = new List<DepartmentProject>();
            Projects.Add(project);
            var testDraftNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testDraftNeeds.SendProcurementNeeds(DepartmentID, BudgetYear, Projects);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Drafted procurement"));
        }

        [TestMethod]
        public void TestProcurementNeedSummary()
        {
            var testProcurementNeedSummary = new ProcurementsController();
            JsonResult result = (JsonResult)testProcurementNeedSummary.ProcurementNeedsSummary();
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Procurement needs summary."));
        }

        [TestMethod]
        public void TestGetProcurementNeeds()
        {
            string id = "3A380F88-6B8B-40AF-AA14-DF546CEC1AA6";
            string id2 = "452BE391-2A89-4212-A5C8-B0A0BB37DFE3";
            var testGetProcurementNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testGetProcurementNeeds.ProcurementNeeds(id, id2);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All procurement needs."));
        }

        [TestMethod]
        public void TestGetProcurementNeeds_Null_ID()
        {
            string id = "";
            var testGetProcurementNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testGetProcurementNeeds.ProcurementNeeds(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("DepartmentID or BudgetYearID is Null"));
        }

        [TestMethod]
        public void TestGetProcurementNeeds_Invalid_Guid()
        {
            string id = "3A380F88-6B8B-40AF-AA14";
            var testGetProcurementNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testGetProcurementNeeds.ProcurementNeeds(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)"));
        }
    }
}

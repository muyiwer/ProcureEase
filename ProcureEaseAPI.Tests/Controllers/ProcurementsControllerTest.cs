﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public string LOCAL_SERVER = "http://localhost.procureease.ng/";
        [TestMethod]
        public void TestDraftNeedsSummary()
        {
            var testDraftNeedsSummary = new ProcurementsController();
           Mocker.MockControllerContext(testDraftNeedsSummary, LOCAL_SERVER);
            string id = "8DEFC11D-5595-41DD-87A9-2A0EF5FE04B8";         
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

            Guid DepartmentID = new Guid("8DEFC11D-5595-41DD-87A9-2A0EF5FE04B8");
            int BudgetYear = 2018;
            List<DepartmentItems> Items = new List<DepartmentItems>();
            Items.Add(new DepartmentItems
            {
                ItemID = new Guid("FD1597B6-E857-4530-8B32-16FD6592CFC8"),
                ItemCodeID = new Guid("05B5775D-6400-4FAA-8AC3-BD1EDCAB1CC0"),
                ItemCode = "IT0100",
                ItemName = "Keyboards",
                UnitPrice = "70000",
                Quantity = "20"
            });
            Items.Add(new DepartmentItems
            {
                ItemID = new Guid("848B0648-22E8-489C-8E60-9976325861BA"),
                ItemCodeID = new Guid("05B5775D-6400-4FAA-8AC3-BD1EDCAB1CC0"),
                ItemCode = "IT0100",
                ItemName = "Monitors",
                UnitPrice = "70000",
                Quantity = "70000"
            });
            List<DepartmentProject> Projects = new List<DepartmentProject>();
            Projects.Add(new DepartmentProject
            {
                ProcurementID = new Guid("D4C85763-A4E7-43D4-AA1A-4D1C1E1E1EAF"),
                ProcurementMethodID = new Guid("4AAF793E-965B-4C0A-8CC3-9957AE52EACC"),
                ProjectCategoryID = new Guid("C4A8C27A-E46E-4B59-AD0F-AA44761248F0"),
                ProjectName = "Procurement of furnitures",
              //  Items = Items
            });           
            var testDraftNeeds = new ProcurementsController();
            Mocker.MockControllerContext(testDraftNeeds, LOCAL_SERVER);
            JsonResult result = (JsonResult)testDraftNeeds.SendProcurementNeeds(DepartmentID, BudgetYear, Projects);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Drafted procurement"));
        }

        [TestMethod]
        public void TestProcurementNeedSummary()
        {
            string budgetYearId = "";
            string departmentId = "";
            var testProcurementNeedSummary = new ProcurementsController();
            JsonResult result = (JsonResult)testProcurementNeedSummary.ProcurementNeedsSummary(budgetYearId,departmentId);
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
            string budgetYearId = "";
            string departmentId = "";
            var testGetProcurementNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testGetProcurementNeeds.ProcurementNeeds(budgetYearId,departmentId);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("DepartmentID or BudgetYearID is Null"));
        }

        [TestMethod]
        public void TestGetProcurementNeeds_Invalid_Guid()
        {
            string budgetYearId = "3A380F88-6B8B-40AF-AA14";
            string departmentId = "3A380F88-6B8B-40AF-AA14";
            var testGetProcurementNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testGetProcurementNeeds.ProcurementNeeds(budgetYearId, departmentId);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)"));
        }

        [TestMethod]
        public void TestGetProcurementPlan_Null_ID()
        {
            string id = "";
            string id2 = "";
            var testGetProcurementPlan = new ProcurementsController();
            JsonResult result = (JsonResult)testGetProcurementPlan.ProcurementPlan(id,id2);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("DepartmentID or BudgetYearID is Null"));
        }

        [TestMethod]
        public void TestGetProcurementPlan_Invalid_Guid()
        {
            string id = "3A380F88-6B8B-40AF-AA14";
            string id2 = "3A380F88-6";
            var testGetProcurementNeeds = new ProcurementsController();
            JsonResult result = (JsonResult)testGetProcurementNeeds.ProcurementPlan(id, id2);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)"));
        }

        [TestMethod]
        public void TestGetProcurementPlan()
        {
            string id = "3A380F88-6B8B-40AF-AA14-DF546CEC1AA6";
            string id2 = "452BE391-2A89-4212-A5C8-B0A0BB37DFE3";
            var testGetProcurementPlan = new ProcurementsController();
            JsonResult result = (JsonResult)testGetProcurementPlan.ProcurementPlan(id, id2);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All procurement Plans."));
        }

        [TestMethod]
        public void TestProcurementPlanSummary()
        {
            string budgetYearId = "3A380F88-6B8B-40AF-AA14-DF546CEC1AA6";
            string departmentId = "452BE391-2A89-4212-A5C8-B0A0BB37DFE3";
            var testProcurementPlanSummary = new ProcurementsController();
            JsonResult result = (JsonResult)testProcurementPlanSummary.ProcurementPlanSummary(budgetYearId,departmentId);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Procurement plan summary."));
        }


        [TestMethod]
        public void TestProcurementNeedsToPlan()
        {
            Guid DepartmentID = new Guid("3A380F88-6B8B-40AF-AA14-DF546CEC1AA6");
            int BudgetYear = 2018;
            List<DepartmentItems> Items = new List<DepartmentItems>();
            Items.Add(new DepartmentItems
            {
                ItemName = "API",
                Quantity = "70000",
                UnitPrice = "70000",
                ItemCode = "IT0100",
                ItemCodeID = new Guid("93e6ffa5-e700-45b0-bde7-6b3570dab27b"),
                Deleted = false
            });
            List<DepartmentProject> Projects = new List<DepartmentProject>(new DepartmentProject[] {

            });
            Projects.Add(new DepartmentProject
            {
                ProcurementMethodID = new Guid("40fdeeb4-e487-4639-acbd-09c85715970a"),
                ProjectName = "Procurement of muyiwa",
                ProjectCategoryID = new Guid("71cf7646-9d37-43fb-ac4d-23d34ea4d4d5"),
                Deleted = false,
                Approved = true,
                // Items = new List<DepartmentItems>(Items),

            });

            var testProcurementNeedsToPlan = new ProcurementsController();
            JsonResult result = (JsonResult)testProcurementNeedsToPlan.ProcurementNeedsToPlan(DepartmentID, BudgetYear, Projects);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All procurement needs."));
        }

        [TestMethod]
        public void TestUpdateProcurementPlan()
        {
            Guid DepartmentID = new Guid("066AF5C4-3F41-419E-ADD5-77E8EC0E1E93");
            int BudgetYear = 2018;
            DepartmentItems Items = new DepartmentItems();
            Items.Quantity = "70000";
            Items.UnitPrice = "70000";
            Items.ItemName = "Office stationaries";
            Items.ItemCodeID = new Guid("93E6FFA5-E700-45B0-BDE7-6B3570DAB27B");
            Items.Deleted = false;
            List<DepartmentProject> Projects = new List<DepartmentProject>();
            Projects.Add(new DepartmentProject()
            {
                ProcurementMethodID = new Guid("3D30C690-F37B-43D9-A17C-D47A4F5DA127"),
                ProjectName = "Procurement of Office stationary",
                ProjectCategoryID = new Guid("71CF7646-9D37-43FB-AC4D-23D34EA4D4D5"),
                Deleted = false,
                Approved = true,
                Items = new List<DepartmentItems>() { Items }
            });
            var testUpdateProcurementPlan = new ProcurementsController();
            JsonResult result = (JsonResult)testUpdateProcurementPlan.UpdateProcurementPlan(DepartmentID, BudgetYear, Projects);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All procurement needs."));
        }

        [TestMethod]
        public void TestApproveProcurementPlan()
        {
            Guid DepartmentID = new Guid("066AF5C4-3F41-419E-ADD5-77E8EC0E1E93");
            int BudgetYear = 2018;
            DepartmentItems Items = new DepartmentItems();
            Items.Quantity = "70000";
            Items.UnitPrice = "70000";
            Items.ItemName = "Office stationaries";
            Items.ItemCodeID = new Guid("93E6FFA5-E700-45B0-BDE7-6B3570DAB27B");
            Items.Deleted = false;
            List<DepartmentProject> Projects = new List<DepartmentProject>();
            Projects.Add(new DepartmentProject()
            {
                ProcurementMethodID = new Guid("3D30C690-F37B-43D9-A17C-D47A4F5DA127"),
                ProjectName = "Procurement of Office stationary",
                ProjectCategoryID = new Guid("71CF7646-9D37-43FB-AC4D-23D34EA4D4D5"),
                Deleted = false,
                Attested = true,
                Items = new List<DepartmentItems>() { Items }
            });
            var testApproveProcurementPlan = new ProcurementsController();
            JsonResult result = (JsonResult)testApproveProcurementPlan.ApproveProcurementPlan(DepartmentID, BudgetYear, Projects);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All procurement needs."));
        }

    }
}

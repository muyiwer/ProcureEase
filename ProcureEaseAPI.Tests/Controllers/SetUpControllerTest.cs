//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ProcureEaseAPI.Models;
//using ProcureEaseAPI.Controllers;
//using System.Web.Http.Results;
//using System.Web.Mvc;
 
//namespace ProcureEaseAPI.Tests.Controllers
//{
//    [TestClass]
//    public class SetUpControllerTest
//    {
//        [TestMethod]
//        public void TestSourceOfFunds()
//        {
//            SourceOfFunds SourceOfFunds = new SourceOfFunds
//            {

//            };
//            var testSourceOfFunds = new SetUpController();
//            var result = (JsonResult)testSourceOfFunds.SourceOfFunds(SourceOfFunds);
//            Console.WriteLine(result.Data);
//            Assert.IsTrue((result.Data + "").Contains("All source of funds"));
//        }

//        [TestMethod]
//        public void TestProcurementMethod()
//        {
//            ProcurementMethod ProcurementMethod = new ProcurementMethod
//            {

//            };
//            var testProcurementMethod = new SetUpController();
//            var result = (JsonResult)testProcurementMethod.ProcurementMethod(ProcurementMethod);
//            Console.WriteLine(result.Data);
//            Assert.IsTrue((result.Data + "").Contains("All Procurement Method"));
//        }

//        [TestMethod]
//        public void TestProjectCategory()
//        {
//            ProjectCategory ProjectCategory = new ProjectCategory
//            {

//            };
//            var testProjectCategory = new SetUpController();
//            var result = (JsonResult)testProjectCategory.ProjectCategory(ProjectCategory);
//            Console.WriteLine(result.Data);
//            Assert.IsTrue((result.Data + "").Contains("All Project Category"));
//        }

//        [TestMethod]
//        public void TestUpdateBasicDetails()
//        {
//            OrganizationSettings OrganizationSettings = new OrganizationSettings();
//            List<TelephoneNumbers> telephoneNumbers = new List<TelephoneNumbers>() {new TelephoneNumbers(){
//                TelephoneNumber = "07065949501"
//            }
              
//            };
            
//            var testUpdateBasicDetails = new SetUpController();
//            var result = (JsonResult)testUpdateBasicDetails.UpdateBasicDetails(OrganizationSettings, telephoneNumbers);
//            Console.WriteLine(result.Data);
//            Assert.IsTrue((result.Data + "").Contains("Organization settings added successfully!!!"));
//        }

//        [TestMethod]
//        public void TestOrganizationSettings()
//        {
//            OrganizationSettings OrganizationSettings = new OrganizationSettings
//            {

//            };
//            var testOrganizationSettings = new SetUpController();
//            var result = (JsonResult)testOrganizationSettings.OrganizationSettings(OrganizationSettings);
//            Console.WriteLine(result.Data);
//            Assert.IsTrue((result.Data + "").Contains("OK"));
//        }
//    }
//}
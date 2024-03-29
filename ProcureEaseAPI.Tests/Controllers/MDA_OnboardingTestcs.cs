﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void TestListOfMDAs()
        {
            var testListOfMDAs = new HomeController();
            JsonResult result = (JsonResult) testListOfMDAs.ListOfMDAs();
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Ok"));
        }

        [TestMethod]
        public void TestActivate_Unsuccessful()
        {
            string AdministatorEmail = "aajekuko@techspecialistlimited.com";
            bool IsOnboarded = false;
            var testActivate = new HomeController();
            JsonResult result = (JsonResult)testActivate.Activate(AdministatorEmail, IsOnboarded);
            Console.WriteLine(result.Data);
            Assert.IsFalse((result.Data + "").Contains("This Organization has not been Onboarded"));
        }

        [TestMethod]
        public void TestActivate_Successful()
        {
            string AdministatorEmail = "annieajeks@gmail.com";
            bool IsOnboarded = true;
            var testActivate = new HomeController();
            JsonResult result = (JsonResult)testActivate.Activate(AdministatorEmail, IsOnboarded);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Organization Account has been Activated Successfully"));
        }

        [TestMethod]
        public void TestDeactivate_successful()
        {
            string AdministatorEmail = "annieajek@gmail.com";
            var testDeactivate = new HomeController();
            JsonResult result = (JsonResult)testDeactivate.Deactivate(AdministatorEmail);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Organization Account has been Deactivated"));
        }
        [TestMethod]
        public void TestDeactivate()
        {
            string AdministatorEmail = "aajekuko@techspecialistlimited.com";
            var testDeactivate = new HomeController();
            JsonResult result = (JsonResult)testDeactivate.Deactivate(AdministatorEmail);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Organization Account has been Deactivated"));
        }

        [TestMethod]
        public void TestManageOrganizationAccounts()
        {
            var testManageOrganizationAccounts = new HomeController();
            JsonResult result = (JsonResult)testManageOrganizationAccounts.ManageOrganizationAccounts();
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Ok"));
        }
    }
}

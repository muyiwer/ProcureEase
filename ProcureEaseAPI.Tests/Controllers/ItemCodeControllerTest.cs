﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProcureEaseAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
    public class ItemCodeControllerTest
    {
        string email = "oaro@techspecialistlimited.com";
        [TestMethod]
        public void TestGetItemCode_Invalid_Guid()
        {
            var testGetItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testGetItemCode);
            string id = "3A380F88-6B8B-40AF-AA14";                  
            JsonResult result = (JsonResult)testGetItemCode.Index(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)"));
        }

        [TestMethod]
        public void TestGetItemCode_Successful()
        {
            var testGetItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testGetItemCode);
            string id = "";
            JsonResult result = (JsonResult)testGetItemCode.Index(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All item codes"));
        }

        [TestMethod]
        public void TestAddtItemCode_Null_ItemCode()
        {
            var testAddtItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testAddtItemCode);
            string itemName = "Server";
            string itemCode = "";
            string CategoryID = "";
            JsonResult result = (JsonResult)testAddtItemCode.Add(itemName, itemCode, CategoryID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("ItemCode is null"));
        }


        [TestMethod]
        public void TestAddtItemCode_Existed_ItemCode()
        {
            var testAddtItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testAddtItemCode);
            string itemName = "Server";
            string itemCode = "IT0208";
            string CategoryID = "f1ced345-32aa-41f2-9045-90c02f2c209a";
            JsonResult result = (JsonResult)testAddtItemCode.Add(itemName, itemCode, CategoryID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("ItemCode already exist."));
        }


        [TestMethod]
        public void TestAddtItemCode_Successfull()
        {           
            var testAddtItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testAddtItemCode);
            string itemName = "Server";
            string itemCode = "IT0300";
            string CategoryID = "f1ced345-32aa-41f2-9045-90c02f2c209a";
            JsonResult result = (JsonResult)testAddtItemCode.Add(itemName,itemCode, CategoryID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All item codes"));
        }


        [TestMethod]
        public void TestUpdatetItemCode_Invalid_Guid()
        {
            var testUpdatetItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testUpdatetItemCode);
            string ItemName = "Server";
            string ItemCode = "IT0208";
            string ItemCodeID = "223333";
            string CategoryID = "f1ced345-32aa-41f2-9045-90c02f2c209a";
            JsonResult result = (JsonResult)testUpdatetItemCode.UpdateItemCode(ItemCode, ItemCodeID, ItemName, CategoryID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx)."));
        }

        [TestMethod]
        public void TestUpdatetItemCode_Existed_ItemCode()
        {
            var testUpdatetItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testUpdatetItemCode);
            string ItemName = "Server";
            string ItemCode = "DW0100";
            string ItemCodeID = "210F8DF4-34F9-4BA4-934A-A294972E447C";
            string CategoryID = "f1ced345-32aa-41f2-9045-90c02f2c209a";
            JsonResult result = (JsonResult)testUpdatetItemCode.UpdateItemCode(ItemCode, ItemCodeID, ItemName,CategoryID );
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("ItemCode already exist."));
        }


        [TestMethod]
        public void TestUpdatetItemCode_Successfully()
        {
            var testUpdatetItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testUpdatetItemCode);
            string ItemName = "Server";
            string ItemCode = "DW0900";
            string ItemCodeID = "210F8DF4-34F9-4BA4-934A-A294972E447C";
            string CategoryID = "f1ced345-32aa-41f2-9045-90c02f2c209a";
            JsonResult result = (JsonResult)testUpdatetItemCode.UpdateItemCode(ItemCode, ItemCodeID, ItemName, CategoryID);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Item updated successfully."));
        }

        [TestMethod]
        public void TestDeletetItemCodeID_Is_Null()
        {
            var testUpdatetItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testUpdatetItemCode);
            string id = "";
            JsonResult result = (JsonResult)testUpdatetItemCode.Delete(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("ItemCodeID is null"));
        }

        [TestMethod]
        public void TestDeletetItemCode_Successfully()
        {
            var testUpdatetItemCode = new ItemCodesController();
            Mocker.MockContextHeader(testUpdatetItemCode);
            string id = "210F8DF4-34F9-4BA4-934A-A294972E447C";
            JsonResult result = (JsonResult)testUpdatetItemCode.Delete(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Item deleted successfully."));
        }
    }
}

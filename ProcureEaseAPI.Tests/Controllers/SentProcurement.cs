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
    class SentProcurement
    {
        [TestMethod]
        public void TestSentProcurement()
        {
            string id = "3A380F88-6B8B-40AF-AA14-DF546CEC1AA6";
            string id2 = "452BE391-2A89-4212-A5C8-B0A0BB37DFE3";
            var testSentProcurement = new ProcureController();
            JsonResult result = (JsonResult)testSentProcurement.SentProcurement(id, id2);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("All sent procurement"));
        }
    }
}

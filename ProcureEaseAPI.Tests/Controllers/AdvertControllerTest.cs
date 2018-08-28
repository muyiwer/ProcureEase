using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcureEaseAPI.Controllers;
using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
    public class AdvertControllerTest
    {
        [TestMethod]
        public void TestGetAdvertSummay()
        {
            Adverts adverts = new Adverts
            {

            };
            var testGetAdvertSummay = new AdvertsController();
            var result = (JsonResult)testGetAdvertSummay.AdvertSummary();
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Advert Summary"));
        }

        [TestMethod]
        public void TestGetAdvertDetails()
        {
            Guid id = new Guid("B9571BD0-BCBE-4817-A22E-41D1FC9E82FD");        
            var testGetAdvertDetails = new AdvertsController();
            var result = (JsonResult)testGetAdvertDetails.AdvertDetails(id);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Advert details"));
        }       

        //[TestMethod]
        //public void UpdateDepartmentTest()
        //{
        //    // Arrange  
        //    var controller = new AdvertsController();
        //        Adverts adverts = new Adverts
        //    {
        //            AdvertStatusID = 1,
        //            Headline = "Test Update Advert",  
        //            Introduction = "Test Advert",  
        //    };
        //    List<AdvertPreparation> Adverts = new List<AdvertPreparation>() {new Adverts(){
        //        TelephoneNumber = "07065949501"
        //    }

        //    };
        //    // Act  
        //    ActionResult actionResult = controller.DraftAdvert(Adverts);
        //    var contentResult = actionResult asNegotiatedContentResult < Adverts >;
        //    Assert.IsNotNull(contentResult);
        //    Assert.AreEqual(HttpStatusCode.Accepted, contentResult.ToString());
        //    Assert.IsNotNull(contentResult.ToString());
        //}
    }
}

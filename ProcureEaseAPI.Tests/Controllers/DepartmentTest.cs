using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcureEaseAPI.Controllers;
using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
    class DepartmentTest
    {
        [TestMethod]
        public void TestAddDepartment()
        {
            string DepartmentName = "Procurement";
            var testAddDepartment = new DepartmentsController();
            var result = testAddDepartment.AddDepartment(DepartmentName);
            Assert.IsNull(result);
        }

        //[TestMethod]
        //public void TestEditDepartment()
        //{
        //    Guid DepartmentID = new Guid();
        //    string DepartmentName = "";
        //    var testAddDepartment = new DepartmentsController();
        //    var result = testAddDepartment.Edit();
        //    Assert.IsNull(result);
        //}
    }
}

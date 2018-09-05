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
    public class DepartmentTest
    {
        [TestMethod]
        public void TestAddDepartment()
        {
            string DepartmentName = "Procurementprocurement";
            var testAddDepartment = new DepartmentsController();
            JsonResult result = (JsonResult)testAddDepartment.AddDepartment(DepartmentName);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Department Added Successfully"));
        }

        [TestMethod]
        public void TestEditDepartment()
        {
            Department department = new Department();
            {
                department.DepartmentID = new Guid("7EB73724-93CA-4D46-98CD-298AD0CE2573");
                department.DepartmentName = "SecondProcurement";
            }
            var testAddDepartment = new DepartmentsController();
            JsonResult result = (JsonResult)testAddDepartment.Edit(department);
            Console.WriteLine(result.Data);
            Assert.IsTrue((result.Data + "").Contains("Editted successfully"));
        }
    }
}

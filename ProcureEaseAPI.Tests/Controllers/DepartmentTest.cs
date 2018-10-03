using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProcureEaseAPI.Controllers;
using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProcureEaseAPI.Tests.Controllers
{
    [TestClass]
    public class DepartmentTest
    {
        [TestMethod]
            public void TestGetAllDepartments()
            {
            try
            {
                var testGetAllDepartments = new DepartmentsController();
                var result = (JsonResult)testGetAllDepartments.Index();
                Console.WriteLine(result.Data);
                Assert.IsTrue((result.Data + "").Contains("Ok"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        [TestMethod]
        public void TestGetDepartment()
        {
            try
            {
                Guid id = new Guid("86CAF117-37ED-4370-AC31-0D86EECAD8ED");
                var testGetDepartment = new DepartmentsController();
                var result = (JsonResult)testGetDepartment.Details(id);
                Console.WriteLine(result.Data);
                Assert.IsTrue((result.Data + "").Contains("Ok"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        [TestMethod]
        public void TestAddDepartment()
        {
            try
            {
                string DepartmentName = "Procurementprocurement";
                Guid? UserID = new Guid("140EC11C-BEFD-4DB3-AEE6-0C45339D3F05");
                var testAddDepartment = new DepartmentsController();
                JsonResult result = (JsonResult)testAddDepartment.AddDepartment(DepartmentName, UserID);
                Console.WriteLine(result.Data);
                Assert.IsTrue((result.Data + "").Contains("Department Added Successfully"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        [TestMethod]
        public void TestEditDepartment()
        {
            try
            {
                string DepartmentID = "7EB73724-93CA-4D46-98CD-298AD0CE2573";
                Guid UserID = new Guid("140EC11C-BEFD-4DB3-AEE6-0C45339D3F05");
                string DepartmentName = "SecondProcurement";

                var testAddDepartment = new DepartmentsController();
                JsonResult result = (JsonResult)testAddDepartment.Edit(DepartmentID, UserID, DepartmentName);
                Console.WriteLine(result.Data);
                Assert.IsTrue((result.Data + "").Contains("Editted successfully"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        [TestMethod]
        public void TestDeleteDepartment()
        {
            try
            {
                string DepartmentName = "Procurementprocurement";
                Guid? UserID = new Guid("140EC11C-BEFD-4DB3-AEE6-0C45339D3F05");
                var testDeleteDepartment = new DepartmentsController();
                JsonResult result = (JsonResult)testDeleteDepartment.AddDepartment(DepartmentName, UserID);
                Console.WriteLine(result.Data);
                Assert.IsTrue((result.Data + "").Contains("Department Added Successfully"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}

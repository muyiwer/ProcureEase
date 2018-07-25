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
   public  class Users
    {
        [TestMethod]
        public void TestAddUser()
        {
            UserProfile UserProfile = new UserProfile
            {
                UserEmail = "muyiweraro@gmail.com"
            };      
            var testAddUser = new UsersController();
            var result = testAddUser.Add(UserProfile);
            Assert.IsNotNull(result);
            
        }
    }
}

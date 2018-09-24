using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProcureEaseAPI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Guid guid = new Guid();
            Console.WriteLine(guid.ToString());
        }
    }
}

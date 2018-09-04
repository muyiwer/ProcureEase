using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Utilities.Test
{
    [TestClass]
    public class EmailTemplateHelperTest
    {
        [TestMethod]
        public void TestGetTemplateContent_WithCorrectTemplateName()
        {
            string templateContent = new EmailTemplateHelper().GetTemplateContent("NMRC-Template");
            Assert.IsNotNull(templateContent, "Template content is null");
        }

        [TestMethod]
        public void TestGetTemplateContent_WithCorrectTemplateName_WithVariables()
        {
            string originalTemplateContent = new EmailTemplateHelper().GetTemplateContent("SampleTemplate");
            string newTemplateContent = string.Format(originalTemplateContent, "Muyiwa International", "Annie Connect International");
            Console.WriteLine("Original template content: " + originalTemplateContent);
            Console.WriteLine("New template content: " + newTemplateContent);
            Assert.IsNotNull(originalTemplateContent, "Original template content is null");
            Assert.IsNotNull(originalTemplateContent, "New template content is null");
            Assert.AreNotEqual(originalTemplateContent, newTemplateContent);
        }

        [TestMethod]
        public void TestGetTemplateContent_WithBlankOrIncorrectTemplateName()
        {
            try
            {
                new EmailTemplateHelper().GetTemplateContent("asdfasdf");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Blank template or template not found.");
            }
        }
    }
}
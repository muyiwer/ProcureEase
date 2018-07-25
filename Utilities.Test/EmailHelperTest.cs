using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Utilities.Test
{
    [TestClass]
    public class EmailHelperTest
    {
        [TestMethod]
        public void TestSendMail_SuccessfullyWithoutAwait()
        {
            string SenderEmail = "ibrolive@gmail.com";
            string SenderDisplayName = "Ibrahim Dauda (test)";
            string RecipientEmail = "idauda@techspecialistlimited.com";
            string BccEmail = "ibrolive@hotmail.com";
            string Subject = "MAIL SENT WITHOUT AWAIT - Testing 1 2 3 Testing";
            string Body = "this is the body of the email. some message here.";

            EmailHelper.Message message = new EmailHelper.Message(SenderEmail, SenderDisplayName, RecipientEmail, BccEmail, Subject, Body);

            EmailHelper emailHelper = new EmailHelper();
            emailHelper.SendMail(JsonConvert.SerializeObject(message));
        }

        [TestMethod]
        public async Task TestSendMail_SuccessfullyWithAwait()
        {
            string SenderEmail = "ibrolive@gmail.com";
            string SenderDisplayName = "Ibrahim Dauda (test)";
            string RecipientEmail = "idauda@techspecialistlimited.com";
            string BccEmail = "ibrolive@hotmail.com";
            string Subject = "MAIL SENT WITH AWAIT - Testing 1 2 3 Testing";
            string Body = "this is the body of the email. some message here.";

            EmailHelper.Message message = new EmailHelper.Message(SenderEmail, SenderDisplayName, RecipientEmail, BccEmail, Subject, Body);

            EmailHelper emailHelper = new EmailHelper();
            bool successStatus = await emailHelper.SendMail(JsonConvert.SerializeObject(message));
            Assert.IsTrue(successStatus);
        }

        [TestMethod]
        public async Task TestAddEmailToQueue_SuccessfullyWithAwait()
        {
            string RecipientEmail = "idauda@techspecialistlimited.com";
            string BccEmail = "muyiweraro@gmail.com";
            string Subject = "MAIL ADDED TO QUEUE - Testing 1 2 3 Testing";
            string Body = "this is the body of the email. some message here.";

            EmailHelper.Message message = new EmailHelper.Message(RecipientEmail, BccEmail, Subject, Body);

            EmailHelper emailHelper = new EmailHelper();
            
            bool successStatus = await emailHelper.AddEmailToQueue(message);
            Assert.IsTrue(successStatus);
        }
    }
}

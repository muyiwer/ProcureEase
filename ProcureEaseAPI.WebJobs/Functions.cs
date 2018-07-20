using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Utilities;

namespace ProcureEaseAPI.WebJobs
{
    public class Functions
    {
        EmailHelper _EmailHelper;
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("EmailQueue")] string message, TextWriter log)
        {
            // write code to reconstruct message into MailMessage
            log.WriteLine(message);
            SendEmail(message);
            WriteToAuditLog();
        }

        private static async Task SendEmail(string message)
        {
            // TODO: move configuration manager lines to the caller of this function
            //string EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
            //string SenderDisplayName = ConfigurationManager.AppSettings["SenderDisplayName"];
            //string Useremail = ConfigurationManager.AppSettings["RecipientEmailAddress"];
            //string recipientBCC = ConfigurationManager.AppSettings["RecipientBCCEmailAddress"];
            //string emailSubject = ConfigurationManager.AppSettings["SubjectOfEmailToUser"];
            //string messageBody = EmailTemplateHelper.NMRC_Template;

            EmailHelper emailHelper = new EmailHelper();
            await emailHelper.SendMail(message);
        }

        private static void WriteToAuditLog()
        {
            throw new NotImplementedException();
        }
    }
}
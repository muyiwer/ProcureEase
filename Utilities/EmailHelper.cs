using System.Net.Mail;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace Utilities
{
    public class EmailHelper
    {
        
        public EmailHelper()
        {
        }

        public async Task<bool> AddEmailToQueue(Message message)
        {
            try
            {
                string connectionString = "DefaultEndpointsProtocol=https;AccountName=procureeasequeue;AccountKey=qh+fw61I/jg0sGnwoyZOaJ1nvcDIGwM1xpkRWYKtwn/C9Ka0N/O2jJHm89v+J7a8z6NQWeSNWV1GVWtK1XrVjA==";
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                CloudQueue queue = queueClient.GetQueueReference("emailqueue");
                await queue.CreateIfNotExistsAsync();
                
                await queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(message)));
                LogHelper.Log(Log.Event.ADD_EMAIL_TO_QUEUE, "Mail added to queue. From: " + message.SenderEmail + ", To: " + message.RecipientEmail);

                return true;

                // TODO: Remove the following lines once the AddEmailToQueue implementation has been fixed.
                // There's an issue with the AddEmailToQueue implementation.
                // So we're temporarily also calling the below function to send the mail directly.
                // bool successStatus = await SendMail(JsonConvert.SerializeObject(message));
                // return successStatus;
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.ADD_EMAIL_TO_QUEUE, ex.Message);
                LogHelper.Log(Log.Event.ADD_EMAIL_TO_QUEUE, ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Used by ProcureEaseAPI.WebJobs.Functions.ProcessQueueMessage to send email.
        /// </summary>
        /// <param name="msg">The email to be sent. This is a serialized version of the EmailHelper.Message class.</param>
        /// <returns></returns>
        public async Task<bool> SendMail(string msg)
        {
            try
            {
                Message message = JsonConvert.DeserializeObject<Message>(msg);
                if(message == null || String.IsNullOrEmpty(msg)) {
                    LogHelper.Log(Log.Event.SEND_EMAIL, "Email message can not be null or empty.");
                } else
                {
                    SmtpClient client = new SmtpClient();
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.IsBodyHtml = true;
                    mailMessage.From = new MailAddress(message.SenderEmail, message.SenderDisplayName);
                    mailMessage.To.Add(message.RecipientEmail);
                    if (string.IsNullOrEmpty(message.BccEmail)==false) // check if BccEmail was provided
                    {
                        mailMessage.Bcc.Add(message.BccEmail); 
                    }
                    mailMessage.Subject = message.Subject;
                    mailMessage.Body = message.Body;
                    await client.SendMailAsync(mailMessage);
                    LogHelper.Log(Log.Event.SEND_EMAIL, "Mail sent successfully to " + message.RecipientEmail);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(Log.Event.SEND_EMAIL, ex.Message);
                LogHelper.Log(Log.Event.ADD_EMAIL_TO_QUEUE, ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Custom email message class to be used when an email is serialized and deserialized
        /// </summary>
        public class Message
        {
            public string SenderEmail { get; set; }
            public string SenderDisplayName { get; set; }
            public string RecipientEmail { get; set; }
            public string BccEmail { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }

            public Message() { }

            public Message(string recipientEmail, string subject, string body)
            {
                // TODO: Implement fetching of [senderEmail] and [displayName] from configuration.
                string senderEmail = "ibrolive@gmail.com";
                string displayName = "Ibrahim Dauda (test)";
                SenderEmail = senderEmail; SenderDisplayName = displayName;
                RecipientEmail = recipientEmail; Subject = subject; Body = body;
            }

            public Message(string recipientEmail, string bccEmail, string subject, string body)
            {
                // TODO: Implement fetching of [senderEmail] and [displayName] from configuration.
                string senderEmail = "ibrolive@gmail.com";
                string displayName = "Ibrahim Dauda (test)";
                SenderEmail = senderEmail; SenderDisplayName = displayName;
                RecipientEmail = recipientEmail; BccEmail = bccEmail; Subject = subject; Body = body;
            }

            public Message(string senderEmail, string displayName, string recipientEmail, string bccEmail, string subject, string body) {
                SenderEmail = senderEmail; SenderDisplayName = displayName;
                RecipientEmail = recipientEmail; BccEmail = bccEmail; Subject = subject; Body = body;
            }
        }
    }
}
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

        public void AddEmailToQueue(string from, string SenderDisplayName, string to, string bcc, string subject, string body)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=accountName;AccountKey=someKey");
                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                CloudQueue queue = queueClient.GetQueueReference("EmailQueue");
                queue.CreateIfNotExistsAsync();

                Message message = new Message(from, SenderDisplayName, to, bcc, subject, body);
                queue.AddMessageAsync(new CloudQueueMessage(JsonConvert.SerializeObject(message)));
            } catch(Exception ex)
            {
                LogHelper.Log(LogHelper.LogEvent.ADD_EMAIL_TO_QUEUE, ex.Message);
            }
        }
        
        public async Task SendMail(string msg)
        {
            try
            {
                Message message = (Message)JsonConvert.DeserializeObject(msg);
                SmtpClient client = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(message.From, message.SenderDisplayName);
                mailMessage.To.Add(message.To);
                mailMessage.Bcc.Add(message.Bcc);
                mailMessage.Subject = message.Subject;
                mailMessage.Body = message.Body;
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogHelper.LogEvent.SEND_EMAIL, ex.Message);
            }
        }

        public class Message
        {
            public string From { get; set; }
            public string SenderDisplayName { get; set; }
            public string To { get; set; }
            public string Bcc { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }

            public Message(string from, string displayName, string to, string bcc, string subject, string body) {
                From = from; SenderDisplayName = displayName;
                To = to; Bcc = bcc; Subject = subject; Body = body;
            }
        }
    }
}



using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class AuditTrailHelper
    {
        public AuditTrailHelper()
        {
        }

        //private void AuditMessage(string connectionString, AuditLog auditLog)
        //{
        //    var sendEmailSubscription = SubscriptionClient.CreateFromConnectionString(connectionString, "contact-us", "audit-request");
        //    sendEmailSubscription.OnMessage(message =>
        //    {
        //        try
        //        {
        //            var contactUsMessage = message.GetBody<ContactUsMessage>();
        //            auditLog.LogContactUsRequest(contactUsMessage);
        //            message.Complete();
        //        }
        //        catch (Exception)
        //        {
        //            message.Abandon();
        //        }

        //    });
        //}
    }
}



using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class LogHelper
    {
        public enum LogEvent
        {
            ADD_EMAIL_TO_QUEUE,
            SEND_EMAIL
        };

        public LogHelper()
        {
        }

        public static void Log(LogEvent logEvent, string message)
        {
            Console.WriteLine(logEvent + " : " + message);
        }
    }
}



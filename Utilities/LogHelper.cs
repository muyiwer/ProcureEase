using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class LogHelper : Log
    {
        public LogHelper()
        {
        }

        public static void Log(Event logEvent, string message)
        {
            Console.WriteLine(logEvent + " : " + message);
        }
    }
}



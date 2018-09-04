using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class LogHelper
    {
        public LogHelper()
        {
        }

        public static void Log(Log.Event logEvent, string message)
        {
            Console.WriteLine(logEvent + " : " + message);
        }
    }
}



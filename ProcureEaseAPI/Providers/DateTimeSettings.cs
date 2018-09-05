using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcureEaseAPI.Providers
{
    public class DateTimeSettings
    {
        public DateTime CurrentDate()
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now.Date;
            return dt;
        }

        public DateTime ParseDateTime(string Date)
        {
            DateTime dt = DateTime.Parse(Date);          
            return dt;
        }

        public int DateDifference(DateTime Date1, DateTime Date2)
        {
            int dt = Date1.Subtract(Date2).Days;
            return dt;
        }

        public int CurrentYear()
        {
            int dt = new int();
            dt = DateTime.Now.Year;
            return dt;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Log
    {
        public enum Event
        {
            ADD_EMAIL_TO_QUEUE,
            SEND_EMAIL,
            ADD_USER,
            INITIATE_PASSWORD_RESET,
            RESET_PASSWORD,
            SIGN_UP,
            EDIT_USER,
            UPDATE_USER_PROFILE,
            UPDATE_DEPARTMENT_HEAD,
            DELETE_USER,
            LOGIN,
            ADD_DEPARTMENT,
            EDIT_DEPARTMENT,
            DELETE_DEPARTMENT,
            GET_DEPARTMENT,
            GET_ALL_SOURCEOFFUNDS,
            GET_ALL_PROCUREMENTMETHOD,
            GET_ALL_PROJECTCATEGORY,
            UPDATE_BASICDETAILS,
            GET_ORGANIZATIONSETTINGS,
            ADD_SOURCEOFFUNDS,
            UPDATE_SOURCEOFFUNDS,
            ADD_PROJECTCATEGORY,
            UPDATE_PROJECTCATEGORY,
            ADD_PROCUREMENTMETHOD,
            UPDATE_PROCUREMENTMETHOD
        };
    }
}

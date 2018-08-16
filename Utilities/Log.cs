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
            CONFIGURATION,
            ADD_PROCUREMENT_PLAN_TO_ADVERT,
            ADD_ADVERT_TO_DRAFT,
            GET_ADVERT_DETAILS,
            GET_ADVERT_Summary,
            ADD_ADVERTCATEGORY
        };
    }
}

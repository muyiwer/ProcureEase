﻿using System;
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
            ALL_DRAFT_PROCUREMENT_NEEDS,
            POST_DRAFT_NEEDS,
            SEND_PROCUREMENTS_NEEDS,
            AUTO_COMPLETE_ITEMNAME,
            SENT_PROCUREMENTS,
            CONFIGURATION,
            PROCUREMENT_NEEDS_SUMMARY,
            PROCUREMENT_PLAN_SUMMARY,
            GET_ORGANIZATION_ID,
            REQUESTFORDEMO,
            GET_ALL_SOURCEOFFUNDS,
            GET_ALL_PROCUREMENTMETHOD,
            GET_ALL_PROJECTCATEGORY,
            UPDATE_BASICDETAILS,
            GET_ORGANIZATIONSETTINGS,
            UPDATE_SOURCEOFFUNDS,
            ADD_SOURCEOFFUNDS
        };
    }
}

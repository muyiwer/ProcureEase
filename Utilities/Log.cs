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
            ADD_SOURCEOFFUNDS,
            GET_ADVERT_DETAILS,
            ADVERT_DETAIL,
            Update_ADVERT,
            ADVERT_IN_DRAFT,
            PUBLISHED_ADVERT,
            ADD_ADVERTCATEGORY,
            EDIT_ADVERTCATEGORY,
            DELETE_ADVERTCATEGORY,
            GET_ADDED_PLAN_ON_ADVERT,
            ADD_ADVERT_TO_DRAFT,
            ADD_PROCUREMENT_PLAN_TO_ADVERT,
            ADD_PLAN_TO_ADVERT,
            GET_ADVERT_Summary,
            ADVERT_SUMMARY,
            GET_ADVERT,
            ADD_ADVERT,
            GET_DEPARTMENT,
            ADD_DEPARTMENT,
            ADD_PROCUREMENTMETHOD,
            UPDATE_PROCUREMENTMETHOD,
            ADD_PROJECTCATEGORY,
            UPDATE_PROJECTCATEGORY,
            EDIT_DEPARTMENT,
            ONBOARDING,
            DELETE_DEPARTMENT,
            DELETE_TELEPHONENUMBER,
            DEACTIVATE,
        };
    }
}

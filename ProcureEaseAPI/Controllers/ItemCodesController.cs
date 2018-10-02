using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace ProcureEaseAPI.Controllers
{
    public class ItemCodesController : Controller
    {
        private ProcureEaseEntities db = new ProcureEaseEntities();
        private CatalogsController catalog = new CatalogsController();
        // GET: ItemCodes
        public ActionResult Index(string id = "")
        {
            string email = Request.Headers["Email"];
            Guid? tenantId = catalog.GetTenantIDFromClientURL(email);
            if (tenantId == null)
            {
                return Json(new
                {
                    success = false,
                    message = "TenantId is null",
                    data = new { }
                }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(id))
            {
                return Json(new
                {
                    success = true,
                    message = "All item codes.",
                    data = db.ItemCode.Select(x => new
                    {
                        ItemCode = x.ItemCode1,
                        x.ItemCodeID,
                        x.ItemName,
                    }).OrderBy(x => x.ItemCode)
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Guid itemCodeGuidId = new Guid();
                try
                {
                    itemCodeGuidId = Guid.Parse(id);
                }
                catch (FormatException ex)
                {
                    return Json(new
                    {
                        success = false,
                        message = "" + ex.Message,
                        data = new
                        { }
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    success = true,
                    message = "All item codes.",
                    data = db.ItemCode.Where(x=>x.ItemCodeID == itemCodeGuidId).Select(x => new
                    {
                        ItemCode = x.ItemCode1,
                        x.ItemCodeID,
                        x.ItemName,
                    }).OrderBy(x => x.ItemCode)
                }, JsonRequestBehavior.AllowGet);
            }
            
        }

        // POST: ItemCodes
        [Providers.Authorize]
        public ActionResult Add(string itemName, string itemCode)
        {
            try
            {
                string email = Request.Headers["Email"];
                Guid? tenantId = catalog.GetTenantIDFromClientURL(email);
                if (tenantId == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "TenantId is null",
                        data = new { }
                    }, JsonRequestBehavior.AllowGet);
                }               
                if (string.IsNullOrEmpty(itemCode))
                {
                    LogHelper.Log(Log.Event.POST_ITEM_CODE, "ItemCode is null.");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("ItemCode is null.");
                }
                var checkIfItemCodeExist = db.ItemCode.Where(x => x.ItemCode1 == itemCode).ToList();
                if (checkIfItemCodeExist != null && checkIfItemCodeExist.Count > 0)
                {
                    LogHelper.Log(Log.Event.POST_ITEM_CODE, "ItemCode already exist.");
                    Response.StatusCode = (int)HttpStatusCode.Conflict;
                    return Error("ItemCode already exist.");
                }
                ItemCode ItemCode = new ItemCode();
                ItemCode.ItemCodeID = Guid.NewGuid();
                ItemCode.ItemCode1 = itemCode;
                ItemCode.ItemName = itemName;
                db.ItemCode.Add(ItemCode);
                db.SaveChanges();
                return Json(new {
                    success = true,
                    message = "Item created successfully.",
                    data = db.ItemCode.Select(x => new
                    {
                        ItemCode = x.ItemCode1,
                        x.ItemCodeID,
                        x.ItemName,
                    }).OrderBy(x=>x.ItemCode)
                }, JsonRequestBehavior.AllowGet);
            }catch(Exception ex)
            {
                LogHelper.Log(Log.Event.POST_ITEM_CODE, ex.Message + ex.StackTrace);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Error("" + ex.Message + ex.StackTrace);
            }
        }


        private ActionResult Error(string message)
        {
            return Json(new
            {
                success = false,
                message = message,
                data = new { }
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
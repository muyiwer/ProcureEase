using ProcureEaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Add(string ItemName, string ItemCode)
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
                if (string.IsNullOrEmpty(ItemCode))
                {
                    LogHelper.Log(Log.Event.POST_ITEM_CODE, "ItemCode is null.");
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("ItemCode is null.");
                }
                var checkIfItemCodeExist = db.ItemCode.Where(x => x.ItemCode1 == ItemCode).ToList();
                if (checkIfItemCodeExist != null && checkIfItemCodeExist.Count > 0)
                {
                    LogHelper.Log(Log.Event.POST_ITEM_CODE, "ItemCode already exist.");
                    Response.StatusCode = (int)HttpStatusCode.Conflict;
                    return Error("ItemCode already exist.");
                }
                ItemCode itemCode = new ItemCode();
                itemCode.ItemCodeID = Guid.NewGuid();
                itemCode.ItemCode1 = ItemCode;
                itemCode.ItemName = ItemName;
                db.ItemCode.Add(itemCode);
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

        [HttpPut]
        [Providers.Authorize]
        public ActionResult UpdateItemCode(string ItemCode, string ItemCodeID, string ItemName)
        {
            try
            {
                Guid guidID = new Guid();               
                try
                {
                    guidID = Guid.Parse(ItemCodeID);
                }
                catch (FormatException ex)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    LogHelper.Log(Log.Event.ALL_DRAFT_PROCUREMENT_NEEDS, "Guid format exeception");
                    return Error(ex.Message);
                }
                ItemCode items = db.ItemCode.Find(guidID);
                if(items == null)
                {
                    LogHelper.Log(Log.Event.UPDATE_ITEM_CODE, "Invalid ItemCodeID with: " + ItemCodeID);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Error("Invalid ItemCodeID with: " + ItemCodeID);
                }
                else
                {
                    if(ItemCode == items.ItemCode1)
                    {
                        items.ItemName = ItemName;
                        items.ItemCode1 = ItemCode;
                        db.Entry(items).State = EntityState.Modified;
                    }
                    else
                    {
                        var checkIfItemCodeExist = db.ItemCode.Where(x => x.ItemCode1 == ItemCode).ToList();
                        if (checkIfItemCodeExist != null && checkIfItemCodeExist.Count > 0)
                        {
                            LogHelper.Log(Log.Event.POST_ITEM_CODE, "ItemCode already exist.");
                            Response.StatusCode = (int)HttpStatusCode.Conflict;
                            return Error("ItemCode already exist.");
                        }
                        else
                        {
                            items.ItemName = ItemName;
                            items.ItemCode1 = ItemCode;
                            db.Entry(items).State = EntityState.Modified;
                        }
                    }
                    db.SaveChanges();
                    return Json(new
                    {
                        success = true,
                        message = "Item updated successfully.",
                        data = db.ItemCode.Select(x => new
                        {
                            ItemCode = x.ItemCode1,
                            x.ItemCodeID,
                            x.ItemName,
                        }).OrderBy(x => x.ItemCode)
                    }, JsonRequestBehavior.AllowGet);
                }
            }catch(Exception ex)
            {
                LogHelper.Log(Log.Event.UPDATE_ITEM_CODE, ex.Message + ex.StackTrace);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Error("" + ex.Message + ex.StackTrace);
            }
        }

        [HttpDelete]
        [Providers.Authorize]
        public ActionResult Delete(string id = "")
        {
            try
            {            
            Guid guidID = new Guid();
            try
            {
                guidID = Guid.Parse(id);
            }
            catch (FormatException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                LogHelper.Log(Log.Event.DELETE_ITEM_CODE, "Guid format exeception");
                return Error(ex.Message + ex.StackTrace);
            }
                ItemCode itemCode = db.ItemCode.Find(guidID);
                if(itemCode == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    LogHelper.Log(Log.Event.DELETE_ITEM_CODE, "ItemCodeID is null");
                    return Error("ItemCodeID is null");
                }
                else
                {
                    db.ItemCode.Remove(itemCode);
                    db.SaveChanges();
                }
                return Json(new
                {
                    success = true,
                    message = "Item deleted successfully.",
                    data = db.ItemCode.Select(x => new
                    {
                        ItemCode = x.ItemCode1,
                        x.ItemCodeID,
                        x.ItemName,
                    }).OrderBy(x => x.ItemCode)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                LogHelper.Log(Log.Event.DELETE_ITEM_CODE, ex.Message +ex.StackTrace);
                return Error(ex.Message + ex.StackTrace);
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
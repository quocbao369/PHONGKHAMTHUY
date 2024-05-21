using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Filters;
using PHONGKHAMTHUY.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHONGKHAMTHUY.Controllers
{
    [CheckSessionAttribute]
    public class SettingController : Controller
    {
        private SettingService settingService = new SettingService();
        // GET: Setting
        [HttpGet]
        public ActionResult LoginAccount()
        {
            var listAccount = settingService.getAllAccount();
            var groupNames = settingService.getNameAuth(listAccount);
            ViewBag.GroupNames = groupNames;

            return View(listAccount);
        }
        [HttpPost]
        public ActionResult LoginAccount(TAIKHOAN account)
        {
            var listAccount = settingService.getAllAccount();
            var groupNames = settingService.getNameAuth(listAccount);
            string fileName = null;
            ViewBag.GroupNames = groupNames;
            HttpPostedFileBase file = Request.Files["HINHDAIDIEN"];

            if (file != null && file.ContentLength > 0)
            {
                // Lưu file vào thư mục mong muốn
                fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Public/assets/images/avatar/"), fileName);
                file.SaveAs(path);
            }
            
            if(account.TENDANGNHAP != null)
            {
                string message = settingService.addNew(account,fileName);
                ViewBag.Message = message;
            }
            else
            {
                string message = settingService.updateUser(account, fileName);
                ViewBag.Message = message;
            }
            return View(listAccount);
        }
        [HttpGet]
        public ActionResult GetAccountData(int accountId)
        {
            var account = settingService.getAccountData(accountId);
            if (account != null)
            {
                return Json(account, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult deleteAccount(int id)
        {
            string message = settingService.delete(id);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            var listAccount = settingService.getAllAccount();
            var groupNames = settingService.getNameAuth(listAccount);
            ViewBag.GroupNames = groupNames;
            return View("LoginAccount", listAccount);
        }
    }
}
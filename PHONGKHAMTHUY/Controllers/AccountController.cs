using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using PHONGKHAMTHUY.Models;
using PHONGKHAMTHUY.Services;

namespace PHONGKHAMTHUY.Controllers
{
    public class AccountController : Controller
    {
        private AccountService account = new AccountService();

        // Đăng nhập tài khoản
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid) {
                
                // Nếu kiểm tra tồn tài tài khoản thì chuyển sang trang home
                if (account.isLogin(model))
                {
                    int id = account.getIdAccount(model.TENDANGNHAP);
                    string name = account.getNameAccount(model.TENDANGNHAP);
                    Session["nameAccount"] = name;
                    Session["idAccount"] = id;
                    Session["username"] = model.TENDANGNHAP;
                    Session["avatar"] = account.getAvatarAccount(model.TENDANGNHAP);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Tên đăng nhập hoặc mật khẩu chưa đúng";
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }
        // POST: /Account/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear(); // Xóa hết dữ liệu trong Session

            // Chuyển hướng đến trang đăng nhập hoặc trang chính (tùy thuộc vào yêu cầu của bạn)
            return RedirectToAction("Login", "Account");
        }
    }
}
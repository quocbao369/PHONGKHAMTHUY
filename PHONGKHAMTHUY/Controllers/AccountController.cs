using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using PHONGKHAMTHUY.Models;

namespace PHONGKHAMTHUY.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountModel model)
        {
            ModelState.AddModelError("name", "Student Name Already Exists.");
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}
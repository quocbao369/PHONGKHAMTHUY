using Microsoft.Extensions.Primitives;
using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Filters;
using PHONGKHAMTHUY.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHONGKHAMTHUY.Controllers
{
    [CheckSessionAttribute]
    public class HomeController : Controller
    {
        private HomeService homeService = new HomeService();

        public ActionResult Index()
        {
            string s = Session["idAccount"].ToString();
            object[] authority = homeService.getAuthority(int.Parse(s));
            Session["nameauth"] = authority[0];
            Session["auth"] = authority[1];
            ViewBag.CountCustomer = homeService.countCustomers();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
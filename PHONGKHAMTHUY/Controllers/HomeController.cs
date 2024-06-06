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
        private MedicineSevice medicineSevice = new MedicineSevice();

        public ActionResult Index()
        {
            string s = Session["idAccount"].ToString();
            object[] authority = homeService.getAuthority(int.Parse(s));
            Session["nameauth"] = authority[0];
            Session["auth"] = authority[1];

            var inforMedical = homeService.GetLatestDonThuoc();

            ViewBag.CountCustomer = homeService.countCustomers();
            ViewBag.CountBill = homeService.countBill();
            ViewBag.CountPCD = homeService.countPCD();
            ViewBag.MedicineList = homeService.getAllMedicineHSD();
            ViewBag.BillList = homeService.GetLatestHoaDon();
            ViewBag.TimeMedical = homeService.GetTimeElapsed(inforMedical.DONTHUOC.NGAYTAO);
            ViewBag.TimeMedical2 = homeService.GetTimeElapsed(homeService.GetLatestHoaDon().HOADON.NGAYTAO);

            return View(inforMedical);
        }
    }
}
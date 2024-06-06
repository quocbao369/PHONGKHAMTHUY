using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHONGKHAMTHUY.Controllers
{
    public class BillController : Controller
    {
        private BillService billService = new BillService();

        [HttpGet]
        public ActionResult ListBill()
        {
            var list = billService.getAll();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddBillThuoc(int id)
        {
            var list = billService.layDSTHUOC(id);
            return View(list);
        }

        [HttpPost]
        public ActionResult AddBillThuoc(int id, List<string> TENTHUOC, List<string> SOLUONG, List<string> GIATIEN, List<string> GIAM,string TONGTIEN)
        {
            string idtaikhoan = Session["idAccount"].ToString();
            string message = billService.addBillThuoc(id, TENTHUOC, SOLUONG, GIATIEN, GIAM, TONGTIEN, idtaikhoan);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var list = billService.layDSTHUOC(id);
            return View(list);
        }

        [HttpGet]
        public ActionResult AddBillPCD(int id)
        {
            var list = billService.layDSHM(id);
            return View(list);
        }

        [HttpPost]
        public ActionResult AddBillPCD(int id, List<string> TENTHUOC, List<string> SOLUONG, List<string> GIATIEN, List<string> GIAM, string TONGTIEN)
        {
            string idtaikhoan = Session["idAccount"].ToString();
            string message = billService.addBillPCD(id, TENTHUOC, SOLUONG, GIATIEN, GIAM, TONGTIEN, idtaikhoan);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var list = billService.layDSHM(id);
            return View(list);
        }

        [HttpGet]
        public ActionResult ShowBill(int id)
        {
            var list = billService.GetHoaDon(id);
            ViewBag.BillList=list;
            return View();
        }
    }
}
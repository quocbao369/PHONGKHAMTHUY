using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Filters;
using PHONGKHAMTHUY.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static System.Net.Mime.MediaTypeNames;

namespace PHONGKHAMTHUY.Controllers
{
    [CheckSessionAttribute]
    public class CSLAppointmentSlipController : Controller
    {
        private DataSQL db = new DataSQL();

        private CSLAppointmentSlipService CSLAppointment = new CSLAppointmentSlipService();
        // GET: CLSAppointmentSlip
        [HttpGet]
        public ActionResult ListCSL()
        {
            var list = CSLAppointment.getAll();
            return View(list);
        }


        // tạo kết quả xét nghiệm
        [HttpGet]
        public ActionResult GenerateTestResults(int id)
        {
            var list = CSLAppointment.getInfor(id);
            ViewBag.ID = id;
            return View(list);
        }
        [HttpPost]
        public ActionResult GenerateTestResults(int id, KETQUAXN kqxn)
        {
            string message = CSLAppointment.addResult(kqxn);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var list = CSLAppointment.getInfor(id);
            ViewBag.ID = id;
            return View("ShowResult", list);
        }

        // hiển thị thông tin của KQXN
        [HttpGet]
        public ActionResult ShowResult(int id)
        {
            var list = CSLAppointment.getInfor(id);
            ViewBag.ID = id;
            return View(list);
        }

        // hiển thị danh sách KQXN
        public ActionResult ListResult()
        {
            var list = CSLAppointment.getAllKQXN();
            return View(list);
        }

        // hiển thị danh sách đơn thuốc
        public ActionResult danhsachdonthuoc()
        {
            var list = CSLAppointment.laydsdonthuoc();
            return View(list);
        }

        // hiển thị chi tiết
        [HttpGet]
        public ActionResult ShowDonThuoc(int id)
        {
            var list = CSLAppointment.getDonThuoc(id);
            return View(list);
        }

        [HttpGet]
        public ActionResult QLdscsl()
        {
            var list = CSLAppointment.getDSCSL();
            return View(list);
        }

        [HttpPost]
        public ActionResult QLdscsl(CHIDINHCSL cd)
        {
            string message = CSLAppointment.adddmCD(cd);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var list = CSLAppointment.getDSCSL();
            return View(list);
        }

        [HttpPost]
        public ActionResult UpdateCSL(int id, Dictionary<string, string> updatedData)
        {
            // Retrieve the item from the database
            var item = db.CHIDINHCSL.Find(id);

            if (item != null)
            {
                // Update the item's properties
                foreach (var data in updatedData)
                {
                    switch (data.Key)
                    {
                        case "TEN":
                            item.TEN = data.Value;
                            break;
                        case "GIA":
                            item.GIA = int.Parse(data.Value);
                            break;
                        case "TENDANHMUC":
                            item.TENDANHMUC = data.Value;
                            break;
                        case "MOTA":
                            item.MOTA = data.Value;
                            break;
                    }
                }

                // Save changes to the database
                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        public ActionResult UpdateResult(int id, string PHUONGPHAPTHUNGHIEM, DateTime NGAYTRA, string KETQUA, string KETLUAN)
        {
            // Retrieve the item from the database
            var item = db.KETQUAXN.FirstOrDefault(u => u.IDHANGMUC == id);

            item.PHUONGPHAPTHUNGHIEM = PHUONGPHAPTHUNGHIEM;
            item.KETLUAN = KETLUAN;
            item.KETQUA = KETQUA;
            item.NGAYTRA = NGAYTRA;

            // Save changes to the database
            db.SaveChanges();
            var list = CSLAppointment.getInfor(id);
            return View("ShowResult", list);
        }
    }
}
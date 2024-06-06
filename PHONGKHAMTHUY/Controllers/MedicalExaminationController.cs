using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Filters;
using PHONGKHAMTHUY.Models;
using PHONGKHAMTHUY.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHONGKHAMTHUY.Controllers
{
    [CheckSessionAttribute]
    public class MedicalExaminationController : Controller
    {
        private MedicalExaminationSevice medicaSevice = new MedicalExaminationSevice();
        private PetSevice petSevice = new PetSevice();

        // GET: MedicalExamination
        [HttpGet]
        public ActionResult Index()
        {
            SetupViewBag();
            var pets = petSevice.getAllPet();
            var petsConvert = petSevice.convertPet(pets);
            return View(petsConvert);
        }

        [HttpPost]
        public ActionResult Index(LICHHEN lh)
        {
            string idtaikhoan = Session["idAccount"].ToString();
            string message = medicaSevice.addLichhen(lh, int.Parse(idtaikhoan));
            if (message != null)
            {
                ViewBag.Message = message;
            }
            SetupViewBag();
            var pets = petSevice.getAllPet();
            var petsConvert = petSevice.convertPet(pets);
            return View(petsConvert);
        }

        // hôm nay

        [HttpGet]
        public ActionResult MedicalExaminationList()
        {
            string idtk = Session["idAccount"].ToString();
            var list = medicaSevice.getListToday(int.Parse(idtk));
            return View(list);
        }

        // danh sách khám 

        [HttpGet]
        public ActionResult ListOfAppointments()
        {
            string idtk = Session["idAccount"].ToString();
            var list = medicaSevice.getAllListAppointment(int.Parse(idtk));
            return View(list);
        }

        [HttpPost]
        public ActionResult ListOfAppointments(int trangthai)
        {
            var list = medicaSevice.getDSKHAM(trangthai);
            return View(list);
        }

        // lấy thông tin lịch hẹn
        [HttpGet]
        public ActionResult GetInfor(int id)
        {
            var lh = medicaSevice.getInfoLichkham(id);
            if (lh != null)
            {
                return Json(lh, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // Cập nhật lịch khám

        [HttpPost]
        public ActionResult Update(LICHHEN lh, string homnay, string dakham)
        {
            string idtk = Session["idAccount"].ToString();
            string message = medicaSevice.updateLichkham(lh);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            if("homnay".Equals(homnay))
            {
                var list = medicaSevice.getListToday(int.Parse(idtk));
                return View("MedicalExaminationList",list);
            }
            else if ("dakham".Equals(dakham))
            {
                var list = medicaSevice.getAllListAppointment(int.Parse(idtk));
                return View("ListOfAppointments",list);
            }
            else
            {
                return null;
            }
            
        }

        // Phiếu chỉ định
        [HttpGet]
        public ActionResult Phieuchidinh(int id)
        {
            var list = medicaSevice.getPCD(id);
            return View(list);
        }

        [HttpPost]
        public ActionResult Phieuchidinh(int id, List<string> CHUANDOAN, string NOIDUNGCHUANDOAN,string LOIDAN, string IDVATNUOI)
        {
            string idtaikhoan = Session["idAccount"].ToString();
            string message = medicaSevice.addPCD(CHUANDOAN, NOIDUNGCHUANDOAN, LOIDAN, IDVATNUOI, idtaikhoan, id);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            var list = medicaSevice.getPCD(id);
            return View(list);
        }

        // Diễn tiến sinh hiệu
        [HttpGet]
        public ActionResult Dientienvasinhhieu(int id)
        {
            var list = medicaSevice.getSHDT(id);
            return View(list);
        }
        [HttpPost]
        public ActionResult Dientienvasinhhieu(int id, string NOIDUNG, string NHIETDO, string CANNANG)
        {
            string message = medicaSevice.addSHDT(id, NOIDUNG, NHIETDO, CANNANG);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var list = medicaSevice.getSHDT(id);
            return View(list);
        }

        // Kê đơn
        [HttpGet]
        public ActionResult Kedon(int id)
        {
            var list = medicaSevice.getKedon(id);
            return View(list);
        }

        [HttpGet]
        public ActionResult getMedicineDetails(int id)
        {
            var thuoc = medicaSevice.laythongtinthuoc(id);
            if (thuoc != null)
            {
                return Json(thuoc, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Kedon(int id, List<string> DSTHUOC, List<string> SOLUONG, string LOIDAN)
        {
            string idtaikhoan = Session["idAccount"].ToString();
            string message = medicaSevice.addDONTHUOC(id, DSTHUOC, SOLUONG, LOIDAN, idtaikhoan);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var list = medicaSevice.getKedon(id);
            return View(list);
        }

        public void SetupViewBag()
        {
            var doctors = medicaSevice.getAccountDoctor();
            ViewBag.Doctors = new SelectList(doctors, "IDTAIKHOAN", "HOTEN");
        }

    }
}
using PHONGKHAMTHUY.Domain;
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
            string message = medicaSevice.addLichhen(lh);
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
            var list = medicaSevice.getListToday();
            return View(list);
        }

        // danh sách khám 

        [HttpGet]
        public ActionResult ListOfAppointments()
        {
            var list = medicaSevice.getAllListAppointment();
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
            string message = medicaSevice.updateLichkham(lh);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            if("homnay".Equals(homnay))
            {
                var list = medicaSevice.getListToday();
                return View("MedicalExaminationList",list);
            }
            else if ("dakham".Equals(dakham))
            {
                var list = medicaSevice.getAllListAppointment();
                return View("ListOfAppointments",list);
            }
            else
            {
                return null;
            }
            
        }

        [HttpGet]
        public ActionResult Phieuchidinh(int id)
        {
            var list = medicaSevice.getPCD(id);
            return View(list);
        }

        [HttpPost]
        public ActionResult Phieuchidinh( List<string> CHUANDOAN, string NOIDUNGCHUANDOAN,string LOIDAN, string IDVATNUOI)
        {
            string message = medicaSevice.addPCD(CHUANDOAN, NOIDUNGCHUANDOAN, LOIDAN, IDVATNUOI);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            int id = int.Parse(IDVATNUOI);
            var list = medicaSevice.getPCD(id);
            return View(list);
        }

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
        [HttpGet]
        public ActionResult Kedon(int id)
        {
            return View();
        }







        public void SetupViewBag()
        {
            var doctors = medicaSevice.getAccountDoctor();
            ViewBag.Doctors = new SelectList(doctors, "IDTAIKHOAN", "HOTEN");
        }

    }
}
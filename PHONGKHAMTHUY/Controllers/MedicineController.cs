﻿using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Filters;
using PHONGKHAMTHUY.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHONGKHAMTHUY.Controllers
{
    [CheckSessionAttribute]
    public class MedicineController : Controller
    {
        private DataSQL db = new DataSQL();

        private MedicineSevice medicineSevice = new MedicineSevice();
        // GET: Medicine
        [HttpGet]
        public ActionResult MedicineList()
        {
            var lists = medicineSevice.getAllMedicine();
            var listConvert = medicineSevice.convertMCD(lists);
            SetupViewBag();

            return View(listConvert);
        }

        // Lấy thông tin thuốc
        [HttpGet]
        public ActionResult getInfor(int id)
        {
            var thuoc = medicineSevice.getInfor(id);
            if (thuoc != null)
            {
                return Json(thuoc, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // Thêm mới
        [HttpPost]
        public ActionResult Add(THUOCVAVATTU thuoc)
        {
            string message = medicineSevice.addMedicine(thuoc);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var lists = medicineSevice.getAllMedicine();
            var listConvert = medicineSevice.convertMCD(lists);
            SetupViewBag();
            return View("MedicineList", listConvert);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return RedirectToAction("MedicineList");
        }

        // Cập nhật
        [HttpPost]
        public ActionResult Update(THUOCVAVATTU thuoc)
        {
            string message = medicineSevice.updateMedicine(thuoc);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            var lists = medicineSevice.getAllMedicine();
            var listConvert = medicineSevice.convertMCD(lists);
            SetupViewBag();
            return View("MedicineList", listConvert);
        }

        [HttpGet]
        public ActionResult Update()
        {
            return RedirectToAction("MedicineList");
        }

        // Xóa
        [HttpGet]
        public ActionResult Delete(int id)
        {
            string message = medicineSevice.deleteMedicine(id);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            var lists = medicineSevice.getAllMedicine();
            var listConvert = medicineSevice.convertMCD(lists);
            SetupViewBag();
            return View("MedicineList", listConvert);
        }

        // Quản lý danh mục thuốc
        [HttpGet]
        public ActionResult DMthuoc()
        {
            var lists = medicineSevice.getDMthuoc();
            return View(lists);
        }

        [HttpPost]
        public ActionResult DMthuoc(string tenDM)
        {
            string message = medicineSevice.addDM(tenDM);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var lists = medicineSevice.getDMthuoc();
            return View(lists);
        }

        [HttpPost]
        public ActionResult UpdateDm(int id, Dictionary<string, string> updatedData)
        {
            var item = db.DANHMUC.Find(id);

            if (item != null)
            {
                // Update the item's properties
                foreach (var data in updatedData)
                {
                    switch (data.Key)
                    {
                        case "TENDANHMUC":
                            item.TENDANHMUC = data.Value;
                            break;
                    }
                }

                // Save changes to the database
                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        public void SetupViewBag()
        {
            var danhMucs = medicineSevice.getAllDirectory();
            ViewBag.DanhMucs = new SelectList(danhMucs, "IDDANHMUC", "TENDANHMUC");
        }
    }
}
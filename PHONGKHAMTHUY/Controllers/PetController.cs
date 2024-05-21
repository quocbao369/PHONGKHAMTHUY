using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Filters;
using PHONGKHAMTHUY.Models;
using PHONGKHAMTHUY.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHONGKHAMTHUY.Controllers
{
    [CheckSessionAttribute]
    public class PetController : Controller
    {
        private PetSevice petSevice = new PetSevice();
        // GET: Pet
        [HttpGet]
        public ActionResult PetList()
        {
            var pets = petSevice.getAllPet();
            var petsConvert = petSevice.convertPet(pets);
            return View(petsConvert);
        }

        // lấy thông tin của vật nuôi
        [HttpGet]
        public ActionResult GetPetInfor(int id)
        {
            var pet = petSevice.getPetInfo(id);
            if (pet != null)
            {
                return Json(pet, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // Thêm mới vật nuôi

        [HttpPost]
        public ActionResult Add(VATNUOI pet, string tenkhachhang, string gioitinh)
        {
            string message = petSevice.addPet(pet, tenkhachhang, gioitinh);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            var pets = petSevice.getAllPet();
            var petsConvert = petSevice.convertPet(pets);
            return View("PetList", petsConvert);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return RedirectToAction("PetList");
        }

        // Cập nhật vật nuôi
        [HttpPost]
        public ActionResult Update(PetModel pet)
        {
            string message = petSevice.updatePet(pet);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            var pets = petSevice.getAllPet();
            var petsConvert = petSevice.convertPet(pets);
            return View("PetList", petsConvert);
        }

        [HttpGet]
        public ActionResult Update()
        {
            
            return RedirectToAction("PetList");
        }

        // Xóa vật nuôi
        [HttpGet]
        public ActionResult Delete(int id)
        {
            string message = petSevice.deletePet(id);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            var pets = petSevice.getAllPet();
            var petsConvert = petSevice.convertPet(pets);
            return View("PetList", petsConvert);
        }
    }
}
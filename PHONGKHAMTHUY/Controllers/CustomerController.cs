using PHONGKHAMTHUY.Domain;
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
    public class CustomerController : Controller
    {
        private CustomerService customerService = new CustomerService();
        // GET: Customer
        [HttpGet]
        public ActionResult CustomerList()
        {
            var lists = customerService.getAllCustomer();
            return View(lists);
        }

        // hiển thị thông tin khách hàng
        [HttpGet]
        public ActionResult GetCustomerData(int id)
        {
            var cs = customerService.getCustomerData(id);
            if (cs != null)
            {
                return Json(cs, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }
        
        // Hàm này dùng để update
        [HttpPost]
        public ActionResult Update(KHACHHANG kh)
        {
            string message = customerService.updateCustomer(kh);
            if(message != null)
            {
                ViewBag.Message = message;
            }
            var lists = customerService.getAllCustomer();
            return View("CustomerList", lists);
        }

        // Hàm này dùng để thêm mới khách hàng
        [HttpPost]
        public ActionResult Add(KHACHHANG kh)
        {
            string message = customerService.addCustomer(kh);
            if (message != null)
            {
                ViewBag.Message = message;
            }
            var lists = customerService.getAllCustomer();
            return View("CustomerList", lists);
        }
        // Dùng để xóa khách hàng
        [HttpGet]
        public ActionResult deleteCustomer(int id)
        {
            string message = customerService.deleteCustomer(id);
            if (message != null)
            {
                ViewBag.Message = message;
            }

            var lists = customerService.getAllCustomer();
            return View("CustomerList", lists);
        }

    }
}
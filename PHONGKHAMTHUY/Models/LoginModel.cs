using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập vào ô này.")]
        [StringLength(10, ErrorMessage = "Không được quá 10 ký tự.")]
        public string TENDANGNHAP { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào ô này.")]
        [StringLength(50, ErrorMessage = "Không được quá 50 ký tự.")]
        public string MATKHAU { get; set; }

    }
}
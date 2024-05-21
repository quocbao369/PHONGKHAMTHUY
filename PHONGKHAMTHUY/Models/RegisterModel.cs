using System.ComponentModel.DataAnnotations;

namespace PHONGKHAMTHUY.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Vui lòng nhập vào ô này.")]
        [StringLength(10, ErrorMessage = "Không được quá 10 ký tự.")]
        public string TENDANGNHAP { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào ô này.")]
        [StringLength(50, ErrorMessage = "Không được quá 50 ký tự.")]
        public string MATKHAU { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào ô này.")]
        [StringLength(50, ErrorMessage = "Không được quá 50 ký tự.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập vào ô này.")]
        [StringLength(10, ErrorMessage = "Không được quá 10 ký tự.")]
        public string DIENTHOAI { get; set; }

    }
}
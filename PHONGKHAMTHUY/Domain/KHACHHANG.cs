using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("KHACHHANG")]
    public class KHACHHANG
    {
        [Key]
        public int IDKHACHHANG { get; set; }

        [StringLength(50)]
        public string HOTEN { get; set; }

        [StringLength(10)]
        public string GIOITINH { get; set; }

        [StringLength(10)]
        public string DIENTHOAI { get; set; }

        [StringLength (256)]
        public string DIACHI { get; set; }

        [StringLength(50)]
        public string LOAIKHACHHANG { get; set; }

        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
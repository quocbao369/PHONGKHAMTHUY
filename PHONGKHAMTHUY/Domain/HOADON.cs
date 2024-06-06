using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("HOADON")]
    public class HOADON
    {
        [Key]
        public int IDHOADON { get; set; }
        public int IDVATNUOI { get; set; }
        public int IDKHACHHANG { get; set; }
        public int IDTAIKHOAN { get; set; }
        public int IDLHD { get; set; }
        public string LOAIHOADON { get; set; }
        public string MAHOADON { get; set; }
        public int TONGTIEN {  get; set; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
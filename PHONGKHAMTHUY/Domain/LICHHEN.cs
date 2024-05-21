using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("LICHHEN")]
    public class LICHHEN
    {
        [Key]
        public int IDLICHKHAM { get; set; }

        public int IDVATNUOI { get; set; }
        public int IDKHACHHANG { get; set; }
        public int IDTAIKHOAN { get; set; }
        public int IDBACSI { get; set; }

        public DateTime? THOIGIANKHAM { get; set; }
        public string LYDO { get; set; }
        public int TRANGTHAI { get; set; }

        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("DONTHUOC")]
    public class DONTHUOC
    {
        [Key]
        public int IDDONTHUOC { get; set; }
        public int IDLICHKHAM { get; set; }
        public int IDTAIKHOAN { get; set; }
        public string MADSTHUOC { set; get; }
        public string LOIDAN { set; get; }
        public string TRANGTHAI { set; get; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
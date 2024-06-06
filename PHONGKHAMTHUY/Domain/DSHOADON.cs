using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("DSHOADON")]
    public class DSHOADON
    {
        [Key]
        public int IDDSHOADON { get; set; }
        public string MAHOADON { get; set; }
        public string TENTHUOC { get; set; }
        public int SOLUONG { get; set; }
        public int GIATIEN { get; set; }
        public int GIAM { get; set; }
        public int TONG { get; set; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
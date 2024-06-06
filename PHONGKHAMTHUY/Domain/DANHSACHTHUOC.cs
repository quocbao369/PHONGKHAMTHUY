using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("DANHSACHTHUOC")]
    public class DANHSACHTHUOC
    {
        [Key]
        public int IDDSTHUOC { get; set; }
        public string MADSTHUOC { set; get; }
        public string TENTHUOC { set; get; }
        public string SOLUONG { set; get; }
        public string TRANGTHAITHANHTOAN { set; get; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
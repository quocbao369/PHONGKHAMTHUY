using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("CHIDINHCSL")]
    public class CHIDINHCSL
    {
        [Key]
        public int IDCHIDINHCSL { get; set; }
        public string TEN { set; get; }
        public int GIA { set; get; }
        public string TENDANHMUC { set; get; }
        public string KETQUAXN { set; get; }
        public string MOTA { set; get; }

        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
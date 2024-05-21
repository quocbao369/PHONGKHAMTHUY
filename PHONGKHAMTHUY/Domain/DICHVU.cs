using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("DICHVU")]

    public class DICHVU
    {

        [Key]
        public int IDDICHVU { get; set; }
        public string TEN { set; get; }
        public int GIA { set; get; }
        public string TENDANHMUC { set; get; }
        public string MOTA { set; get; }

        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
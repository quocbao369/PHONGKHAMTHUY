using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("KETQUAXN")]
    public class KETQUAXN
    {
        [Key]
        public int IDKETQUAXN { get; set; }
        public int IDHANGMUC { get; set; }
        public string PHUONGPHAPTHUNGHIEM { get; set; }
        public string KETQUA { get; set; }
        public string KETLUAN { get; set; }

        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
        public DateTime? NGAYTRA { get; set; }
    }
}
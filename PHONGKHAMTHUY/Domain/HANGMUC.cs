using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("HANGMUC")]
    public class HANGMUC
    {
        [Key]
        public int IDHANGMUC { get; set; }
        public string MAHANGMUC { get; set; }
        public string LOAIHANGMUC { get; set; }
        public string TRANGTHAITHANHTOAN { set; get; }
        public string TRANGTHAI { get; set; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
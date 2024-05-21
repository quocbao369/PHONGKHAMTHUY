using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("CHUANDOAN")]
    public class CHUANDOAN
    {
        [Key]
        public int IDCHUANDOAN { get; set; }
        public string MACHUANDOAN { get; set; }
        public string LOAICHUANDOAN { get; set; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
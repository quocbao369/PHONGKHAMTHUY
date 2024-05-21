using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("DIENTIEN")]
    public class DIENTIEN
    {
        [Key]
        public int IDDIENTIEN { get; set; }
        public int IDVATNUOI { get; set; }
        public string NOIDUNG { get; set; }

        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
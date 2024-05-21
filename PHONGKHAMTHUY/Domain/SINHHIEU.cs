using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("SINHHIEU")]
    public class SINHHIEU
    {
        [Key]
        public int IDSINHHIEU { get; set; }
        public int IDVATNUOI { get; set; }
        public string NHIETDO { get; set; }
        public string CANNANG { get; set; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
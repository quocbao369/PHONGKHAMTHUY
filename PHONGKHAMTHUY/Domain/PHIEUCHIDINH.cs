﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Domain
{
    [Table("PHIEUCHIDINH")]
    public class PHIEUCHIDINH
    {
        [Key]
        public int IDPHIEUCHIDINH { get; set; }
        public int IDLICHKHAM { get; set; }
        public int IDVATNUOI { get; set; }
        public int IDTAIKHOAN { get; set; }
        public string MAHANGMUC { get; set; }
        public string NOIDUNG { get; set; }
        public string LOIDAN { get; set; }
        public string TRANGTHAI { get; set; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }
    }
}
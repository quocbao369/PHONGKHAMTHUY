﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class AccountModel
    {
        public int IDTAIKHOAN { get; set; }

        public int IDNHOMNGUOIDUNG { get; set; }

        [StringLength(10)]
        public string TENDANGNHAP { get; set; }

        [StringLength(50)]
        public string MATKHAU { get; set; }

        [StringLength(50)]
        public string HOTEN { get; set; }

        [StringLength(10)]
        public string GIOITINH { get; set; }

        [StringLength(10)]

        public string DIENTHOAI { get; set; }

        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(250)]
        public string HINHDAIDIEN { get; set; }

        [StringLength(50)]
        public string TRANGTHAI { get; set; }
        public DateTime? NGAYTHEM { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class MedicineModel
    {
        public int IDTHUOCVT { get; set; }
        public int IDDANHMUC { get; set; }
        public int IDNHAPKHO { get; set; }

        [StringLength(250)]
        public string TENTHUOCVT { get; set; }

        public int TONKHO { get; set; }

        [StringLength(50)]
        public string DONVI { get; set; }

        public int GIABAN { get; set; }

        public int GIANHAP { get; set; }

        [StringLength(50)]
        public string CACHDUNG { get; set; }

        [StringLength(50)]
        public string QUYCACH { get; set; }

        [StringLength(50)]
        public string SOLUONGTRENNGAY { get; set; }

        [StringLength(250)]
        public string THANHPHAN { get; set; }

        [StringLength(250)]
        public string GHICHU { get; set; }

        public DateTime? HSD { get; set; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }


        [StringLength(250)]
        public string TENDANHMUC { get; set; }
    }
}
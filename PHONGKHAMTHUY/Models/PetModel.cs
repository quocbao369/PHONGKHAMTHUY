using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class PetModel
    {
        public int IDVATNUOI { get; set; }
        public int IDKHACHHANG { get; set; }

        [StringLength(50)]
        public string TEN { get; set; }

        [StringLength(50)]
        public string LOAI { get; set; }

        [StringLength(50)]
        public string GIONG { get; set; }

        [StringLength(50)]
        public string TUOI { get; set; }

        [StringLength(50)]
        public string MAUSAC { get; set; }

        [StringLength(50)]
        public string CANNANG { get; set; }

        [StringLength(50)]
        public string GIOITINH { get; set; }

        public DateTime? NGAYSINH { get; set; }
        public DateTime? NGAYTAO { get; set; }
        public DateTime? NGAYSUA { get; set; }
        public DateTime? NGAYXOA { get; set; }


        public string TENKHACHHANG { get; set; }
    }
}
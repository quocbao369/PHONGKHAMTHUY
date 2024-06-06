using PHONGKHAMTHUY.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class HomeModel
    {
        public VATNUOI VATNUOI { get; set; }
        public KHACHHANG KHACHHANG { get; set; }
        public PHIEUCHIDINH PHIEUCHIDINH { get; set; }
        public TAIKHOAN TAIKHOAN { get; set; }
        public LICHHEN LICHHEN { get; set; }
        public DONTHUOC DONTHUOC { get; set; }
        public NHOMNGUOIDUNG NHOMNGUOIDUNG { get; set; }
        public List<NHOMNGUOIDUNG> LISTNHOMNGUOIDUNG { get; set; }
        public List<DANHSACHTHUOC> LISTDANHSACHTHUOC { get; set; }
        public THUOCVAVATTU THUOCVAVATTU { get; set; }
        public List<THUOCVAVATTU> LISTTHUOCVAVATTU { get; set; }

    }
}
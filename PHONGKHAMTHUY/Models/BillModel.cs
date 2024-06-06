using PHONGKHAMTHUY.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class BillModel
    {
        public VATNUOI VATNUOI { get; set; }
        public KHACHHANG KHACHHANG { get; set; }
        public HOADON HOADON { get; set; }
        public DSHOADON DSHOADON { get; set; }
        public LICHHEN LICHHEN { get; set; }
        public TAIKHOAN TAIKHOAN { get; set; }

        public List<DSHOADON> LISTDSHOADON { get; set; }
        public DONTHUOC DONTHUOC { get; set; }
        public DANHSACHTHUOC DANHSACHTHUOC { get; set; }
        public List<DANHSACHTHUOC> LISTDSDONTHUOC { get; set; }
        public THUOCVAVATTU THUOCVAVATTU { get; set; }
        public List<THUOCVAVATTU> LISTTHUOCVAVATTU { get; set; }
        public PHIEUCHIDINH PHIEUCHIDINH { get; set; }
        public HANGMUC HANGMUC { get; set; }
        public List<HANGMUC> LISTHANGMUC { get; set; }

        public CHIDINHCSL CHIDINHCSL { get; set; }
        public List<CHIDINHCSL> LISTCHIDINHCSL { get; set; }

    }
}
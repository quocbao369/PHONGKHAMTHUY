using PHONGKHAMTHUY.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class CSLAppointmentSlipModel
    {
        public PHIEUCHIDINH PHIEUCHIDINH { get; set; }
        public List<PHIEUCHIDINH> LISTPHIEUCHIDINH { get; set; }
        public VATNUOI VATNUOI { get; set; }
        public KHACHHANG KHACHHANG { get; set; }
        public TAIKHOAN TAIKHOAN { get; set; }
        public HANGMUC HANGMUC { get; set; }
        public List<HANGMUC> LISTHANGMUC { get; set; }
        public KETQUAXN KETQUAXN { get; set;}

        public DONTHUOC DONTHUOC { get; set; }
        public DANHSACHTHUOC DANHSACHTHUOC { get; set; }
        public List<DONTHUOC> LISTDONTHUOC { get; set; }
        public List<DANHSACHTHUOC> LISTDSDONTHUOC { get; set; }

        public LICHHEN LICHHEN { get; set; }
    }
}
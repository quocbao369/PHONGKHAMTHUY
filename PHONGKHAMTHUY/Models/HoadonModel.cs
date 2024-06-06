using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHONGKHAMTHUY.Domain;


namespace PHONGKHAMTHUY.Models
{
    public class HoadonModel
    {
        public KHACHHANG KHACHHANG { get; set; }
        public VATNUOI VATNUOI { get; set; }
        public List<DSHOADON> DSHOADON { get; set; }
        public HOADON HOADON { get; set; }
        public string TENTAIKHOAN { get; set; }
        public string TENNND { get; set; }

        public int TONGGIAMGIA {  get; set; }
        public int TONGCHUAGG {  get; set; }
    }
}
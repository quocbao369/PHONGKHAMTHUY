using PHONGKHAMTHUY.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class ListOfAppointmentsModel
    {
        public int IDVATNUOI { get; set; }
        public VATNUOI PET { get; set; }
        public KHACHHANG CUSTOMER { get; set; }

        public LICHHEN LICHHEN { get; set; }
        public PHIEUCHIDINH PHIEUCHIDINH { get; set; }

        public SINHHIEU SINHHIEU { get; set; }
        public DIENTIEN DIENTIEN { get; set; }
        public CHUANDOAN CHUANDOAN { get; set;}


        public List<SINHHIEU> LISTSINHHIEU { get; set; }
        public List<DIENTIEN> LISTDIENTIEN { get; set; }
        public List<CHUANDOAN> LISTCHUANDOAN { get; set; }

        public DICHVU DICHVU { get; set; }
        public CHIDINHCSL CHIDINHCSL { get; set; }

        public List<DICHVU> LISTDICHVU { get; set; }
        public List<CHIDINHCSL> LISTCHIDINHCSL { get; set; }

    }
}
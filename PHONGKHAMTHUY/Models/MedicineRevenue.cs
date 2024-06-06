using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Models
{
    public class MedicineRevenue
    {
        public string MedicineName { get; set; }
        public string Quantity { get; set; }
        public int Price { get; set; }
        public int TotalRevenue { get; set; }
    }
    public class CSLRevenue
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
    }
}
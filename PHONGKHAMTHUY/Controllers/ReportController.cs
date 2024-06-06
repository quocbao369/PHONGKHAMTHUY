using PHONGKHAMTHUY.Connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHONGKHAMTHUY.Models;


namespace PHONGKHAMTHUY.Controllers
{
    public class ReportController : Controller
    {
        private DataSQL db = new DataSQL();

        // GET: Report
        public ActionResult DoanhThu()
        {
            var hdt = db.HOADON.Where(u => u.LOAIHOADON == "DT").ToList();
            var hdpcd = db.HOADON.Where(u => u.LOAIHOADON == "PCD").ToList();

            int doanhthu1 = 0;
            int doanhthu2 = 0;

            foreach (var h in hdt)
            {
                doanhthu1 = h.TONGTIEN + doanhthu1; 
            }
            foreach (var h in hdpcd)
            {
                doanhthu2 = h.TONGTIEN + doanhthu2;
            }
            // lấy doanh thu từng tháng
            int currentYear = DateTime.Now.Year;

            var revenueData = db.HOADON
                .Where(hd => hd.NGAYTAO.HasValue && hd.NGAYTAO.Value.Year == currentYear)
                .GroupBy(hd => hd.NGAYTAO.Value.Month)
                .Select(g => new MonthlyRevenueViewModel
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(hd => hd.TONGTIEN)
                })
                .OrderBy(x => x.Month)
                .ToList();

            // Ensure all months are represented
            var allMonths = Enumerable.Range(1, 12).Select(m => new MonthlyRevenueViewModel
            {
                Month = m,
                TotalRevenue = revenueData.FirstOrDefault(rd => rd.Month == m)?.TotalRevenue ?? 0
            }).ToList();

            var dst = db.DANHSACHTHUOC.Where(u => u.TRANGTHAITHANHTOAN == "DTT").ToList();
            var groupedData = dst
                    .GroupBy(ds => ds.TENTHUOC)
                    .Select(g => new MedicineRevenue
                    {
                        MedicineName = g.Key,
                        Quantity = g.Sum(ds => int.TryParse(ds.SOLUONG, out var quantity) ? quantity : 0).ToString(),
                    })
                    .ToList();
            foreach (var t in groupedData)
            {
                t.Price = laygiaban(t.MedicineName);
                t.TotalRevenue = int.Parse(t.Quantity) * t.Price;
            }

            var csl = db.HANGMUC.Where(u => u.TRANGTHAITHANHTOAN == "DTT").ToList();
            var groupedDatacsl = csl
                    .GroupBy(ds => ds.LOAIHANGMUC)
                    .Select(g => new CSLRevenue
                    {
                        Name = g.Key,
                        Quantity = g.Count().ToString(),
                    })
                    .ToList();

            foreach (var t in groupedDatacsl)
            {
                t.Price = laygiaCSL(t.Name);
                t.Total = int.Parse(t.Quantity) * t.Price;
            }


            ViewBag.RevenueData2 = groupedData;
            ViewBag.RevenueData3 = groupedDatacsl;
            ViewBag.DoanhthuThuoc = doanhthu1;
            ViewBag.DoanhthuDichvu = doanhthu2;
            ViewBag.RevenueData = allMonths;
            return View();
        }
        // Hàm để lấy giá bán của thuốc từ bảng THUOCVAVATTU
        private int laygiaban(string medicineName)
        {
            var medicine = db.THUOCVAVATTU.FirstOrDefault(tvvt => tvvt.TENTHUOCVT == medicineName);
            return medicine.GIABAN;
        }
        private int laygiaCSL(string Name)
        {
            var medicine = db.CHIDINHCSL.FirstOrDefault(tvvt => tvvt.TEN == Name);
            return medicine.GIA;
        }
    }
}
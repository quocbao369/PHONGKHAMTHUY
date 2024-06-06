using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Services
{
    public class HomeService
    {
        private DataSQL db = new DataSQL();

        // Hàm này dùng để lấy quyền hạn của tài khoản
        public object[] getAuthority (int id)
        {
            var taikhoan = db.TAIKHOAN.FirstOrDefault(a => a.IDTAIKHOAN == id);
            var obj = db.NHOMNGUOIDUNG.FirstOrDefault(a => a.IDNHOMNGUOIDUNG == taikhoan.IDNHOMNGUOIDUNG);
            object[] result = new object[2];
            if (obj != null)
            {
                result[0] = obj.TENNHOM;
                result[1] = obj.QUYENHAN;
                return result;
            }
            else
            {
                return null;
            }
            
        }

        public int countCustomers()
        {
            var obj = db.KHACHHANG.Where(u => u.NGAYXOA == null).ToList();
            return obj.Count;
        }

        public int countBill()
        {
            var obj = db.HOADON.Where(u => u.NGAYXOA == null).ToList();
            return obj.Count;
        }
        public int countPCD()
        {
            var obj = db.PHIEUCHIDINH.Where(u => u.NGAYXOA == null).ToList();
            return obj.Count;
        }

        // Lấy danh sách thuốc sắp hết hạn
        public List<THUOCVAVATTU> getAllMedicineHSD()
        {
            DateTime currentDate = DateTime.Now;
            DateTime expiryDateLimit = currentDate.AddDays(30);

            var obj = db.THUOCVAVATTU
                        .Where(u => u.NGAYXOA == null && u.HSD != null && u.HSD <= expiryDateLimit)
                        .ToList();
            return obj;
        }

        // lấy đơn thuốc gần nhất
        public HomeModel GetLatestDonThuoc()
        {
            // Lấy đơn thuốc vừa tạo gần nhất
            var dt = db.DONTHUOC
                            .Where(u => u.NGAYXOA == null)
                            .OrderByDescending(u => u.NGAYTAO)
                            .FirstOrDefault();
            var lh = db.LICHHEN
                            .FirstOrDefault(u => u.IDLICHKHAM == dt.IDLICHKHAM);
            var tk = db.TAIKHOAN
                            .FirstOrDefault(u => u.IDTAIKHOAN == dt.IDTAIKHOAN);
            var vn = db.VATNUOI
                            .FirstOrDefault(u => u.IDVATNUOI == lh.IDVATNUOI);
            var kh = db.KHACHHANG
                            .FirstOrDefault(u => u.IDKHACHHANG == lh.IDKHACHHANG);
            var dsthuoc = db.DANHSACHTHUOC
                                    .Where(u => u.MADSTHUOC == dt.MADSTHUOC).ToList();

            var tvt = db.THUOCVAVATTU.Where(u => u.NGAYXOA == null).ToList();

            var lnd = db.NHOMNGUOIDUNG.FirstOrDefault(u => u.IDNHOMNGUOIDUNG == tk.IDNHOMNGUOIDUNG);

            HomeModel model = new HomeModel {
                DONTHUOC = dt,
                TAIKHOAN = tk,
                VATNUOI = vn,
                KHACHHANG = kh,
                LICHHEN = lh,
                NHOMNGUOIDUNG  = lnd,
                LISTDANHSACHTHUOC = dsthuoc,
                LISTTHUOCVAVATTU = tvt,
            };

            return model;
        }

        // lấy hóa đon gần nhất
        public HoadonModel GetLatestHoaDon()
        {
            // Lấy đơn thuốc vừa tạo gần nhất
            var hd = db.HOADON
                            .Where(u => u.NGAYXOA == null)
                            .OrderByDescending(u => u.NGAYTAO)
                            .FirstOrDefault();
            var tk = db.TAIKHOAN
                            .FirstOrDefault(u => u.IDTAIKHOAN == hd.IDTAIKHOAN);
            var vn = db.VATNUOI
                            .FirstOrDefault(u => u.IDVATNUOI == hd.IDVATNUOI);
            var kh = db.KHACHHANG
                            .FirstOrDefault(u => u.IDKHACHHANG == hd.IDKHACHHANG);
            var dshd = db.DSHOADON
                                    .Where(u => u.MAHOADON == hd.MAHOADON).ToList();

            var lnd = db.NHOMNGUOIDUNG.FirstOrDefault(u => u.IDNHOMNGUOIDUNG == tk.IDNHOMNGUOIDUNG);
            int tcgg = 0;
            int tgg = 0;
            foreach ( var d in dshd)
            {
                tcgg = d.TONG + tcgg;
                tgg = d.GIAM + tgg;
            }

            HoadonModel model = new HoadonModel
            {
                KHACHHANG = kh,
                VATNUOI = vn,
                DSHOADON = dshd,
                TENNND = lnd.TENNHOM,
                TENTAIKHOAN = tk.HOTEN,
                HOADON = hd,
                TONGCHUAGG = tcgg,
                TONGGIAMGIA = tgg,
            };

            return model;
        }

        public string GetTimeElapsed(DateTime? ngayTao)
        {
            if (ngayTao == null)
            {
                return "Unknown"; // Hoặc một thông báo phù hợp nếu giá trị null
            }

            var currentTime = DateTime.Now;
            var timeSpan = currentTime - ngayTao.Value;

            if (timeSpan.TotalMinutes < 1)
            {
                return "Just now";
            }
            else if (timeSpan.TotalMinutes < 60)
            {
                return $"{(int)timeSpan.TotalMinutes} Minutes Ago";
            }
            else if (timeSpan.TotalHours < 24)
            {
                return $"{(int)timeSpan.TotalHours} Hours Ago";
            }
            else if (timeSpan.TotalDays < 30)
            {
                return $"{(int)timeSpan.TotalDays} Days Ago";
            }
            else if (timeSpan.TotalDays < 365)
            {
                return $"{(int)(timeSpan.TotalDays / 30)} Months Ago";
            }
            else
            {
                return $"{(int)(timeSpan.TotalDays / 365)} Years Ago";
            }
        }

    }
}
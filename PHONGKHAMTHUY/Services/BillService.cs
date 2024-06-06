using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Models;

namespace PHONGKHAMTHUY.Services
{
    public class BillService
    {
        private DataSQL db = new DataSQL();

        // 
        public List<BillModel> getAll()
        {
            var hoadon = db.HOADON.Where(u => u.NGAYXOA == null).ToList();
            List<BillModel> lists = new List<BillModel>();

            foreach (var hd in hoadon)
            {
                var pet = db.VATNUOI.FirstOrDefault(c => c.IDVATNUOI == hd.IDVATNUOI);
                var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == hd.IDKHACHHANG);
                var account = db.TAIKHOAN.FirstOrDefault(c => c.IDTAIKHOAN == hd.IDTAIKHOAN);
                var dshoadon = db.DSHOADON.Where(u => u.MAHOADON == hd.MAHOADON).ToList();

                BillModel model = new BillModel
                {
                    VATNUOI = pet,
                    KHACHHANG = customer,
                    TAIKHOAN = account,
                    LISTDSHOADON = dshoadon,
                    HOADON = hd,
                };
                lists.Add(model);
            }
            return lists;
        }

        // 
        public BillModel layDSTHUOC(int id)
        {
            var donthuoc = db.DONTHUOC.FirstOrDefault(u => u.NGAYXOA == null && u.IDDONTHUOC == id);
            var dsthuoc = db.DANHSACHTHUOC.Where(u => u.MADSTHUOC == donthuoc.MADSTHUOC && u.TRANGTHAITHANHTOAN =="CTT").ToList();
            var lichhen = db.LICHHEN.FirstOrDefault(u => u.IDLICHKHAM == donthuoc.IDLICHKHAM);
            var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lichhen.IDVATNUOI);
            var customer = db.KHACHHANG.FirstOrDefault(u => u.IDKHACHHANG == lichhen.IDKHACHHANG);

            // Lấy danh sách tên thuốc từ dsthuoc
            var tenThuocList = dsthuoc.Select(d => d.TENTHUOC).ToList();

            // Lấy thông tin chi tiết của thuốc từ bảng THUOC
            var thuocDetails = db.THUOCVAVATTU.Where(t => tenThuocList.Contains(t.TENTHUOCVT)).ToList();
            BillModel model = new BillModel
            {
                DONTHUOC = donthuoc,
                LISTDSDONTHUOC = dsthuoc,
                LICHHEN = lichhen,
                VATNUOI = pet,
                KHACHHANG = customer,
                LISTTHUOCVAVATTU = thuocDetails,

            };
            return model;
        }

        public BillModel layDSHM(int id)
        {
            var pcd = db.PHIEUCHIDINH.FirstOrDefault(u => u.NGAYXOA == null && u.IDPHIEUCHIDINH == id);
            var dshangmuc = db.HANGMUC.Where(u => u.MAHANGMUC == pcd.MAHANGMUC && u.TRANGTHAITHANHTOAN == "CTT" && u.TRANGTHAI == "DXN").ToList();
            var lichhen = db.LICHHEN.FirstOrDefault(u => u.IDLICHKHAM == pcd.IDLICHKHAM);
            var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lichhen.IDVATNUOI);
            var customer = db.KHACHHANG.FirstOrDefault(u => u.IDKHACHHANG == lichhen.IDKHACHHANG);

            // Lấy danh sách tên thuốc từ dsthuoc
            var tenCDCSL = dshangmuc.Select(d => d.LOAIHANGMUC).ToList();

            // Lấy thông tin chi tiết của thuốc từ bảng THUOC
            var CDCSLdetail = db.CHIDINHCSL.Where(t => tenCDCSL.Contains(t.TEN)).ToList();
            BillModel model = new BillModel
            {
                PHIEUCHIDINH = pcd,
                LISTHANGMUC = dshangmuc,
                LICHHEN = lichhen,
                VATNUOI = pet,
                KHACHHANG = customer,
                LISTCHIDINHCSL = CDCSLdetail,

            };
            return model;
        }

        //
        public string addBillThuoc(int id, List<string> TENTHUOC, List<string> SOLUONG, List<string> GIATIEN, List<string> GIAM, string TONGTIEN, string idtaikhoan)
        {
            if (TENTHUOC == null || SOLUONG == null || GIATIEN == null || GIAM == null)
            {
                return "Vui lòng điền đủ thông tin";
            }
            else
            {
                bool checkThuoc = true;
                foreach (var item in TENTHUOC)
                {
                    var cdcsl = db.THUOCVAVATTU.FirstOrDefault(u => u.TENTHUOCVT == item);
                    if (cdcsl == null)
                    {
                        checkThuoc = false; break;
                    }
                    else
                    {
                        checkThuoc = true;
                    }
                }
                if (checkThuoc)
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            List<DSHOADON> dsHoadon = new List<DSHOADON>();

                            DateTime date = DateTime.Now;
                            int count = db.HOADON.Count();
                            for (int i = 0; i < TENTHUOC.Count; i++)
                            {
                                DSHOADON ds = new DSHOADON
                                {
                                    MAHOADON = "MAHD" + (count),
                                    TENTHUOC = TENTHUOC[i],
                                    SOLUONG = int.Parse(SOLUONG[i]),
                                    GIATIEN = int.Parse(GIATIEN[i]),
                                    GIAM = int.Parse(GIAM[i]),
                                    TONG = int.Parse(SOLUONG[i]) * int.Parse(GIATIEN[i]),
                                    NGAYTAO = date,
                                };

                                dsHoadon.Add(ds);
                            }

                            // Add all new items to the database
                            db.DSHOADON.AddRange(dsHoadon);
                            db.SaveChanges();

                            var donthuoc = db.DONTHUOC.FirstOrDefault(u => u.IDDONTHUOC == id);
                            var lichhen = db.LICHHEN.FirstOrDefault(u => u.IDLICHKHAM == donthuoc.IDLICHKHAM);
                            var khachhang = db.KHACHHANG.FirstOrDefault(u => u.IDKHACHHANG == lichhen.IDKHACHHANG);
                            var vatnuoi = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lichhen.IDVATNUOI);
                            var dsthuoc = db.DANHSACHTHUOC.Where(u => u.MADSTHUOC == donthuoc.MADSTHUOC && u.TRANGTHAITHANHTOAN == "CTT").ToList();

                            HOADON hd = new HOADON();
                            hd.NGAYTAO = date;
                            hd.MAHOADON = "MAHD" + count;
                            hd.IDVATNUOI = vatnuoi.IDVATNUOI;
                            hd.IDKHACHHANG = khachhang.IDKHACHHANG;
                            hd.IDLHD = id;
                            hd.LOAIHOADON = "DT";
                            hd.TONGTIEN = int.Parse(TONGTIEN);
                            hd.IDTAIKHOAN = int.Parse(idtaikhoan);
                            donthuoc.TRANGTHAI = "DTT";

                            // Cập nhật TRANGTHAITHANHTOAN cho từng mục trong danh sách
                            foreach (var item in dsthuoc)
                            {
                                item.TRANGTHAITHANHTOAN = "DTT";
                            }
                            
                            db.HOADON.Add(hd);
                            db.SaveChanges();

                            dbContextTransaction.Commit();
                            return "Đã thêm thành công";
                        }
                        catch (Exception)
                        {
                            // Nếu có lỗi, rollback giao dịch
                            dbContextTransaction.Rollback();
                            return "Tạo thất bại";
                        }
                    }
                }
                else
                {
                    return "Chỉ định CSL không tồn tại";
                }
            }
        }

        public string addBillPCD(int id, List<string> TENHANGMUC, List<string> SOLUONG, List<string> GIATIEN, List<string> GIAM, string TONGTIEN, string idtaikhoan)
        {
            if (TENHANGMUC == null || SOLUONG == null || GIATIEN == null || GIAM == null)
            {
                return "Vui lòng điền đủ thông tin";
            }
            else
            {
                bool check = true;
                foreach (var item in TENHANGMUC)
                {
                    var cdcsl = db.CHIDINHCSL.FirstOrDefault(u => u.TEN == item);
                    if (cdcsl == null)
                    {
                        check = false; break;
                    }
                    else
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            List<DSHOADON> dsHoadon = new List<DSHOADON>();

                            DateTime date = DateTime.Now;
                            int count = db.HOADON.Count();
                            for (int i = 0; i < TENHANGMUC.Count; i++)
                            {
                                DSHOADON ds = new DSHOADON
                                {
                                    MAHOADON = "MAHD" + (count),
                                    TENTHUOC = TENHANGMUC[i],
                                    SOLUONG = int.Parse(SOLUONG[i]),
                                    GIATIEN = int.Parse(GIATIEN[i]),
                                    GIAM = int.Parse(GIAM[i]),
                                    TONG = int.Parse(SOLUONG[i]) * int.Parse(GIATIEN[i]),
                                    NGAYTAO = date,
                                };

                                dsHoadon.Add(ds);
                            }

                            // Add all new items to the database
                            db.DSHOADON.AddRange(dsHoadon);
                            db.SaveChanges();

                            var pcd = db.PHIEUCHIDINH.FirstOrDefault(u => u.IDPHIEUCHIDINH == id);
                            var lichhen = db.LICHHEN.FirstOrDefault(u => u.IDLICHKHAM == pcd.IDLICHKHAM);
                            var khachhang = db.KHACHHANG.FirstOrDefault(u => u.IDKHACHHANG == lichhen.IDKHACHHANG);
                            var vatnuoi = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lichhen.IDVATNUOI);
                            var dshangmuc = db.HANGMUC.Where(u => u.MAHANGMUC == pcd.MAHANGMUC && u.TRANGTHAITHANHTOAN == "CTT" && u.TRANGTHAI == "DXN").ToList();

                            HOADON hd = new HOADON();
                            hd.NGAYTAO = date;
                            hd.MAHOADON = "MAHD" + count;
                            hd.IDVATNUOI = vatnuoi.IDVATNUOI;
                            hd.IDKHACHHANG = khachhang.IDKHACHHANG;
                            hd.IDLHD = id;
                            hd.LOAIHOADON = "PCD";
                            hd.TONGTIEN = int.Parse(TONGTIEN);
                            hd.IDTAIKHOAN = int.Parse(idtaikhoan);
                            pcd.TRANGTHAI = "DTT";

                            // Cập nhật TRANGTHAITHANHTOAN cho từng mục trong danh sách
                            foreach (var item in dshangmuc)
                            {
                                item.TRANGTHAITHANHTOAN = "DTT";
                            }

                            db.HOADON.Add(hd);
                            db.SaveChanges();

                            dbContextTransaction.Commit();
                            return "Đã thêm thành công";
                        }
                        catch (Exception)
                        {
                            // Nếu có lỗi, rollback giao dịch
                            dbContextTransaction.Rollback();
                            return "Tạo thất bại";
                        }
                    }
                }
                else
                {
                    return "Chỉ định CSL không tồn tại";
                }
            }
        }

        // lấy hóa đon gần nhất
        public HoadonModel GetHoaDon(int id)
        {
            // Lấy đơn thuốc vừa tạo gần nhất
            var hd = db.HOADON
                            .FirstOrDefault(u => u.NGAYXOA == null && u.IDHOADON == id);
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
            foreach (var d in dshd)
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


    }
}
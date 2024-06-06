using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace PHONGKHAMTHUY.Models
{
    public class MedicalExaminationSevice
    {
        private DataSQL db = new DataSQL();
        // Dùng để lấy danh sách bác sĩ
        public List<TAIKHOAN> getAccountDoctor()
        {
            var doctors = db.TAIKHOAN.Where(u => u.NGAYXOA == null && u.IDNHOMNGUOIDUNG == 2).ToList();
            return doctors;
        }

        // Lấy thông tin lịch khám
        public ListOfAppointmentsModel getInfoLichkham(int id)
        {
            LICHHEN lh = db.LICHHEN.FirstOrDefault(a => a.IDLICHKHAM == id && a.NGAYXOA == null);
            VATNUOI pet = db.VATNUOI.FirstOrDefault(a => a.IDVATNUOI == lh.IDVATNUOI);
            KHACHHANG customer = db.KHACHHANG.FirstOrDefault(a => a.IDKHACHHANG == lh.IDKHACHHANG);
            if(lh != null)
            {
                ListOfAppointmentsModel list = new ListOfAppointmentsModel
                {
                    PET = pet,
                    CUSTOMER = customer,
                    LICHHEN = lh,
                };
                return list;
            }
            else
            {
                return null;
            }
        }

        // Lấy danh sách lịch hẹn hôm nay
        public List<ListOfAppointmentsModel> getListToday(int idtk)
        {
            var taikhoan = db.TAIKHOAN.FirstOrDefault(c => c.IDTAIKHOAN == idtk);
            var auth = db.NHOMNGUOIDUNG.FirstOrDefault(u => u.IDNHOMNGUOIDUNG == taikhoan.IDNHOMNGUOIDUNG);

            if(auth.QUYENHAN == "BS")
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                List<ListOfAppointmentsModel> lists = new List<ListOfAppointmentsModel>();
                var lhs = db.LICHHEN
                                .Where(u => u.NGAYXOA == null && u.THOIGIANKHAM >= today && u.THOIGIANKHAM < tomorrow && u.IDBACSI == idtk)
                                .OrderByDescending(u => u.THOIGIANKHAM)
                                .ToList();

                foreach (var lh in lhs)
                {
                    var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == lh.IDKHACHHANG);
                    var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lh.IDVATNUOI);
                    ListOfAppointmentsModel list = new ListOfAppointmentsModel
                    {
                        PET = pet,
                        CUSTOMER = customer,
                        LICHHEN = lh,
                    };
                    lists.Add(list);
                }

                return lists;
            }
            else
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                List<ListOfAppointmentsModel> lists = new List<ListOfAppointmentsModel>();
                var lhs = db.LICHHEN
                                .Where(u => u.NGAYXOA == null && u.THOIGIANKHAM >= today && u.THOIGIANKHAM < tomorrow)
                                .OrderByDescending(u => u.THOIGIANKHAM)
                                .ToList();

                foreach (var lh in lhs)
                {
                    var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == lh.IDKHACHHANG);
                    var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lh.IDVATNUOI);
                    ListOfAppointmentsModel list = new ListOfAppointmentsModel
                    {
                        PET = pet,
                        CUSTOMER = customer,
                        LICHHEN = lh,
                    };
                    lists.Add(list);
                }

                return lists;
            }
           
        }

        // Lấy tất cả danh sách lịch khám
        public List<ListOfAppointmentsModel> getAllListAppointment(int idtk)
        {
            var taikhoan = db.TAIKHOAN.FirstOrDefault(c => c.IDTAIKHOAN == idtk);
            var auth = db.NHOMNGUOIDUNG.FirstOrDefault(u => u.IDNHOMNGUOIDUNG == taikhoan.IDNHOMNGUOIDUNG);
            if (auth.QUYENHAN == "BS")
            {
                List<ListOfAppointmentsModel> lists = new List<ListOfAppointmentsModel>();

                var lhs = db.LICHHEN
                                .Where(u => u.NGAYXOA == null && u.IDBACSI == idtk)
                                .OrderByDescending(u => u.THOIGIANKHAM)
                                .ToList();

                foreach (var lh in lhs)
                {
                    var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == lh.IDKHACHHANG);
                    var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lh.IDVATNUOI);
                    ListOfAppointmentsModel list = new ListOfAppointmentsModel
                    {
                        PET = pet,
                        CUSTOMER = customer,
                        LICHHEN = lh,
                    };
                    lists.Add(list);
                }

                return lists;
            }
            else
            {
                List<ListOfAppointmentsModel> lists = new List<ListOfAppointmentsModel>();

                var lhs = db.LICHHEN
                                .Where(u => u.NGAYXOA == null)
                                .OrderByDescending(u => u.THOIGIANKHAM)
                                .ToList();

                foreach (var lh in lhs)
                {
                    var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == lh.IDKHACHHANG);
                    var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lh.IDVATNUOI);
                    ListOfAppointmentsModel list = new ListOfAppointmentsModel
                    {
                        PET = pet,
                        CUSTOMER = customer,
                        LICHHEN = lh,
                    };
                    lists.Add(list);
                }

                return lists;
            }

        }

        // Lấy danh sáchs khám
        public List<ListOfAppointmentsModel> getDSKHAM(int trangthai)
        {
            List<ListOfAppointmentsModel> lists = new List<ListOfAppointmentsModel>();

            var lhs = db.LICHHEN
                            .Where(u => u.NGAYXOA == null && u.TRANGTHAI == trangthai)
                            .OrderByDescending(u => u.THOIGIANKHAM)
                            .ToList();

            foreach (var lh in lhs)
            {
                var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == lh.IDKHACHHANG);
                var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lh.IDVATNUOI);
                ListOfAppointmentsModel list = new ListOfAppointmentsModel
                {
                    PET = pet,
                    CUSTOMER = customer,
                    LICHHEN = lh,
                };
                lists.Add(list);
            }

            return lists;
        }

        // Thêm mới lịch hẹn

        public string addLichhen(LICHHEN lh, int idtaikhoan)
        {
            if (lh.IDVATNUOI == 0 || lh.THOIGIANKHAM == null || lh.LYDO == null)
            {
                return "Vui lòng điền đầy đủ thông tin";
            }

            DateTime date = DateTime.Now;
            DateTime date2 = date.AddMinutes(10);

            if (lh.THOIGIANKHAM < date2)
            {
                return "Ngày" + lh.THOIGIANKHAM + " không hợp lệ." ;
            }

            var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lh.IDVATNUOI);
            if (pet != null)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        
                        lh.NGAYTAO = date;
                        lh.IDKHACHHANG = pet.IDKHACHHANG;
                        lh.IDTAIKHOAN = idtaikhoan;


                        db.LICHHEN.Add(lh);
                        db.SaveChanges();

                        // Commit giao dịch nếu không có lỗi
                        dbContextTransaction.Commit();

                        return "Đã thêm thành công";
                    }
                    catch (Exception)
                    {
                        // Nếu có lỗi, rollback giao dịch
                        dbContextTransaction.Rollback();
                        return "Thêm lịch hẹn không thành công";
                    }
                }
            }
            else
            {
                return "Không tìm thấy vật nuôi";
            }            
        }

        public string updateLichkham(LICHHEN lh)
        {
            var lichhen = db.LICHHEN.FirstOrDefault(u => u.IDLICHKHAM == lh.IDLICHKHAM);
            if (lichhen == null)
            {
                return "Lịch khám không tồn tại";
            }
            else
            {
                DateTime date = DateTime.Now;

                lichhen.THOIGIANKHAM = lh.THOIGIANKHAM;
                lichhen.TRANGTHAI = lh.TRANGTHAI;
                lichhen.LYDO= lh.LYDO;
                lichhen.NGAYSUA = date;

                db.SaveChanges();

                return "Cập nhật thành công";
            }

        }

        public ListOfAppointmentsModel getSHDT(int id)
        {
            var sinhhieu = db.SINHHIEU.Where(u => u.IDVATNUOI == id && u.NGAYXOA == null).OrderByDescending(u => u.NGAYTAO).ToList();
            var dientien = db.DIENTIEN.Where(u => u.IDVATNUOI == id && u.NGAYXOA == null).OrderByDescending(u => u.NGAYTAO).ToList();
            var vatnuoi = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI== id);
            ListOfAppointmentsModel list = new ListOfAppointmentsModel
            {
                IDVATNUOI = id,
                LISTSINHHIEU = sinhhieu,
                LISTDIENTIEN = dientien,
                PET = vatnuoi,
            };
            return list;
        }

        public ListOfAppointmentsModel getPCD(int id)
        {
            var lichhen = db.LICHHEN.FirstOrDefault(u => u.IDLICHKHAM == id);
            var sinhhieu = db.SINHHIEU.Where(u => u.IDVATNUOI == lichhen.IDVATNUOI && u.NGAYXOA == null).OrderByDescending(u => u.NGAYTAO).ToList();
            var dientien = db.DIENTIEN.Where(u => u.IDVATNUOI == lichhen.IDVATNUOI && u.NGAYXOA == null).OrderByDescending(u => u.NGAYTAO).ToList();
            var vatnuoi = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lichhen.IDVATNUOI);
            var khachhang = db.KHACHHANG.FirstOrDefault(u => u.IDKHACHHANG == vatnuoi.IDKHACHHANG);
            var dsCDCSL = db.CHIDINHCSL.Where(u => u.NGAYXOA == null).ToList();
            ListOfAppointmentsModel list = new ListOfAppointmentsModel
            {
                IDVATNUOI = id,
                LISTSINHHIEU = sinhhieu,
                LISTDIENTIEN = dientien,
                PET = vatnuoi,
                CUSTOMER = khachhang,
                LISTCHIDINHCSL = dsCDCSL,
                LICHHEN = lichhen,
            };
            return list;
        }

        public string addSHDT(int id, string NOIDUNG, string NHIETDO, string CANNANG)
        {
            if(NOIDUNG != null)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var dientien = new DIENTIEN();
                        DateTime date = DateTime.Now;
                        dientien.NGAYTAO = date;
                        dientien.NOIDUNG = NOIDUNG;
                        dientien.IDVATNUOI = id;

                        db.DIENTIEN.Add(dientien);
                        db.SaveChanges();

                        // Commit giao dịch nếu không có lỗi
                        dbContextTransaction.Commit();

                        return "Đã thêm thành công";
                    }
                    catch (Exception)
                    {
                        // Nếu có lỗi, rollback giao dịch
                        dbContextTransaction.Rollback();
                        return "Thêm thất bại";
                    }
                }
            }
            else
            {
                if (NHIETDO != null || CANNANG != null)
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var sinhhieu = new SINHHIEU();
                            DateTime date = DateTime.Now;
                            sinhhieu.NGAYTAO = date;
                            sinhhieu.NHIETDO = NHIETDO;
                            sinhhieu.CANNANG = CANNANG;
                            sinhhieu.IDVATNUOI = id;

                            db.SINHHIEU.Add(sinhhieu);
                            db.SaveChanges();

                            // Commit giao dịch nếu không có lỗi
                            dbContextTransaction.Commit();

                            return "Đã thêm thành công";
                        }
                        catch (Exception)
                        {
                            // Nếu có lỗi, rollback giao dịch
                            dbContextTransaction.Rollback();
                            return "Thêm thất bại";
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

        }
        public string addPCD(List<string> CHUANDOAN, string NOIDUNGCHUANDOAN, string LOIDAN, string IDVATNUOI, string idtaikhoan, int id)
        {
            if (CHUANDOAN == null || NOIDUNGCHUANDOAN == null || LOIDAN == null || IDVATNUOI == null)
            {
                return "Vui lòng điền đủ thông tin";
            }
            else
            {
                bool checkCDCSL = true;
                foreach (var item in CHUANDOAN)
                {
                    var cdcsl = db.CHIDINHCSL.FirstOrDefault(u => u.TEN == item);
                    if(cdcsl == null)
                    {
                        checkCDCSL = false; break;
                    }
                    else
                    {
                        checkCDCSL = true;
                    }
                }
                if (checkCDCSL)
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DateTime date = DateTime.Now;
                            int count = db.PHIEUCHIDINH.Count();
                            foreach (var item in CHUANDOAN)
                            {
                                HANGMUC hm = new HANGMUC();
                                hm.MAHANGMUC = "MAHM" + count;
                                hm.LOAIHANGMUC = item;
                                hm.TRANGTHAI = "CXN";
                                hm.NGAYTAO = date;
                                hm.TRANGTHAITHANHTOAN = "CTT";

                                db.HANGMUC.Add(hm);
                                db.SaveChanges();
                            }
                            PHIEUCHIDINH pcd = new PHIEUCHIDINH();
                            pcd.NGAYTAO = date;
                            pcd.MAHANGMUC = "MAHM" + count;
                            pcd.IDVATNUOI = int.Parse(IDVATNUOI);
                            pcd.NOIDUNG = NOIDUNGCHUANDOAN;
                            pcd.LOIDAN = LOIDAN;
                            pcd.IDTAIKHOAN = int.Parse(idtaikhoan);
                            pcd.IDLICHKHAM = id;
                            pcd.TRANGTHAI = "CTT";

                            db.PHIEUCHIDINH.Add(pcd);
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

        public ListOfAppointmentsModel getKedon(int id)
        {
            var lichhen = db.LICHHEN.FirstOrDefault(u => u.IDLICHKHAM == id);
            var vatnuoi = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == lichhen.IDVATNUOI);
            var khachhang = db.KHACHHANG.FirstOrDefault(u => u.IDKHACHHANG == lichhen.IDKHACHHANG);
            var sinhhieu = db.SINHHIEU.Where(u => u.IDVATNUOI == lichhen.IDVATNUOI && u.NGAYXOA == null).OrderByDescending(u => u.NGAYTAO).ToList();
            var dientien = db.DIENTIEN.Where(u => u.IDVATNUOI == lichhen.IDVATNUOI && u.NGAYXOA == null).OrderByDescending(u => u.NGAYTAO).ToList();
            var phieuchidinh = db.PHIEUCHIDINH.FirstOrDefault(u => u.IDLICHKHAM == lichhen.IDLICHKHAM);
            List<KETQUAXN> listKQXN = new List<KETQUAXN>();

            List<HANGMUC> hangmuc = new List<HANGMUC>();

            if (phieuchidinh != null)
            {
                hangmuc = db.HANGMUC.Where(u => u.MAHANGMUC == phieuchidinh.MAHANGMUC && u.TRANGTHAI == "DXN").ToList();
                foreach (var hm in hangmuc)
                {
                    var kqxn = db.KETQUAXN.FirstOrDefault(u => u.IDHANGMUC == hm.IDHANGMUC);
                    if (kqxn != null)
                    {
                        listKQXN.Add(kqxn);
                    }
                }
            }
            var dsthuoctv = db.THUOCVAVATTU.Where(u => u.NGAYXOA == null).ToList();

            ListOfAppointmentsModel list = new ListOfAppointmentsModel
            {
                LICHHEN = lichhen,
                LISTSINHHIEU = sinhhieu,
                LISTDIENTIEN = dientien,
                PET = vatnuoi,
                CUSTOMER = khachhang,
                PHIEUCHIDINH = phieuchidinh,
                LISTHANGMUC = hangmuc,
                LISTKETQUAXN = listKQXN,
                LISTTHUOCVAVATTU = dsthuoctv,
            };
            return list;
        }

        // Thêm mới đơn thuốc
        public string addDONTHUOC(int id, List<string> DSTHUOC, List<string> SOLUONG, string LOIDAN, string idtaikhoan)
        {
            if (DSTHUOC == null || SOLUONG == null || LOIDAN == null)
            {
                return "Vui lòng điền đủ thông tin";
            }
            else
            {
                bool checkThuoc = true;
                foreach (var item in DSTHUOC)
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
                            List<DANHSACHTHUOC> danhSachThuocList = new List<DANHSACHTHUOC>();

                            DateTime date = DateTime.Now;
                            int count = db.DONTHUOC.Count();
                            for (int i = 0; i < DSTHUOC.Count; i++)
                            {
                                DANHSACHTHUOC ds = new DANHSACHTHUOC
                                {
                                    MADSTHUOC = "MADS" + (count),
                                    TENTHUOC = DSTHUOC[i],
                                    SOLUONG = SOLUONG[i],
                                    NGAYTAO = date,
                                    TRANGTHAITHANHTOAN = "CTT",
                                };

                                danhSachThuocList.Add(ds);
                            }

                            // Add all new items to the database
                            db.DANHSACHTHUOC.AddRange(danhSachThuocList);
                            db.SaveChanges();

                            DONTHUOC dt = new DONTHUOC();
                            dt.NGAYTAO = date;
                            dt.MADSTHUOC = "MADS" + count;
                            dt.LOIDAN = LOIDAN;
                            dt.IDLICHKHAM = id;
                            dt.IDTAIKHOAN = int.Parse(idtaikhoan);
                            dt.TRANGTHAI = "CTT";


                            db.DONTHUOC.Add(dt);
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


        public THUOCVAVATTU laythongtinthuoc(int id)
        {
            var thuoc = db.THUOCVAVATTU.FirstOrDefault(u => u.IDTHUOCVT == id);
            if(thuoc != null)
            {
                return thuoc;
            }
            return null;
        }

    }
}
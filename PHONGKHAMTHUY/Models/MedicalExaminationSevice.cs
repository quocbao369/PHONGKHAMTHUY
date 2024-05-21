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
            LICHHEN lh = db.LICHHEN.FirstOrDefault(a => a.IDLICHKHAM == id);
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
        public List<ListOfAppointmentsModel> getListToday()
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);
            List<ListOfAppointmentsModel> lists = new List<ListOfAppointmentsModel>();
            var lhs = db.LICHHEN
                            .Where(u => u.NGAYXOA == null && u.THOIGIANKHAM >= today && u.THOIGIANKHAM < tomorrow)
                            .OrderByDescending(u => u.THOIGIANKHAM)
                            .ToList();

            foreach ( var lh in lhs)
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

        // Lấy tất cả danh sách lịch khám
        public List<ListOfAppointmentsModel> getAllListAppointment()
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

        public string addLichhen(LICHHEN lh)
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
            var sinhhieu = db.SINHHIEU.Where(u => u.IDVATNUOI == id).OrderByDescending(u => u.NGAYTAO).ToList();
            var dientien = db.DIENTIEN.Where(u => u.IDVATNUOI == id).OrderByDescending(u => u.NGAYTAO).ToList();
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
            var sinhhieu = db.SINHHIEU.Where(u => u.IDVATNUOI == id).OrderByDescending(u => u.NGAYTAO).ToList();
            var dientien = db.DIENTIEN.Where(u => u.IDVATNUOI == id).OrderByDescending(u => u.NGAYTAO).ToList();
            var vatnuoi = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == id);
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
        public string addPCD(List<string> CHUANDOAN, string NOIDUNGCHUANDOAN, string LOIDAN, string IDVATNUOI)
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
                    var cdcsl = db.CHIDINHCSL.FirstOrDefault(u => u.TENDANHMUC == item);
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
                            PHIEUCHIDINH pcd = new PHIEUCHIDINH();
                            pcd.NGAYTAO = date;

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

    }
}
using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Services
{
    public class CSLAppointmentSlipService
    {
        private DataSQL db = new DataSQL();
        public List<CSLAppointmentSlipModel> getAll()
        {
            var pcds = db.PHIEUCHIDINH.Where(u => u.NGAYXOA == null).ToList();
            List<CSLAppointmentSlipModel> lists = new List<CSLAppointmentSlipModel>();

            foreach (var pcd in pcds)
            {
                var pet = db.VATNUOI.FirstOrDefault(c => c.IDVATNUOI == pcd.IDVATNUOI);
                var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == pet.IDKHACHHANG);
                var account = db.TAIKHOAN.FirstOrDefault(c => c.IDTAIKHOAN == pcd.IDTAIKHOAN);
                var hangmuc = db.HANGMUC.Where(c => c.MAHANGMUC == pcd.MAHANGMUC).ToList();
                var lichhen = db.LICHHEN.FirstOrDefault(c => c.IDLICHKHAM == pcd.IDLICHKHAM);
                CSLAppointmentSlipModel model = new CSLAppointmentSlipModel
                {
                    PHIEUCHIDINH = pcd,
                    VATNUOI = pet,
                    KHACHHANG = customer,
                    LISTHANGMUC = hangmuc,
                    TAIKHOAN = account,
                    LICHHEN = lichhen,
                };
                lists.Add(model);
            }

            return lists;
        }

        public CSLAppointmentSlipModel getInfor(int id)
        {
            var hangmuc = db.HANGMUC.FirstOrDefault(c => c.IDHANGMUC == id);
            var pcd = db.PHIEUCHIDINH.FirstOrDefault(u => u.MAHANGMUC == hangmuc.MAHANGMUC);
            var pet = db.VATNUOI.FirstOrDefault(c => c.IDVATNUOI == pcd.IDVATNUOI);
            var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == pet.IDKHACHHANG);
            var kqxn = db.KETQUAXN.FirstOrDefault(c => c.IDHANGMUC == id);

            CSLAppointmentSlipModel model = new CSLAppointmentSlipModel
            {
                HANGMUC = hangmuc,
                VATNUOI = pet,
                KHACHHANG = customer,
                PHIEUCHIDINH = pcd,
                KETQUAXN = kqxn,
            };

            return model;
        }
        public string addResult(KETQUAXN kqxn)
        {
            if (kqxn.IDHANGMUC == 0 || kqxn.PHUONGPHAPTHUNGHIEM == null || kqxn.KETQUA == null ||
                kqxn.KETLUAN == null || kqxn.NGAYTAO == null || kqxn.NGAYTRA == null)
            {
                return "Vui lòng điền đầy đủ thông tin";
            }
            else
            {

                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var hangmuc = db.HANGMUC.FirstOrDefault(u => u.TRANGTHAI == "CXN" && u.IDHANGMUC == kqxn.IDHANGMUC);

                        hangmuc.TRANGTHAI = "DXN";
                        hangmuc.NGAYSUA = kqxn.NGAYTRA;

                        db.KETQUAXN.Add(kqxn);
                        db.SaveChanges();

                        // Commit giao dịch nếu không có lỗi
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
        }

        // danh sách KQXN
        public List<CSLAppointmentSlipModel> getAllKQXN()
        {
            var kqxns = db.KETQUAXN.Where(u => u.NGAYXOA == null).ToList();

            List<CSLAppointmentSlipModel> lists = new List<CSLAppointmentSlipModel>();
            foreach (var kqxn in kqxns)
            {
                var hangmuc = db.HANGMUC.FirstOrDefault(c => c.IDHANGMUC == kqxn.IDHANGMUC);
                var pcd = db.PHIEUCHIDINH.FirstOrDefault(c => c.MAHANGMUC == hangmuc.MAHANGMUC);

                var pet = db.VATNUOI.FirstOrDefault(c => c.IDVATNUOI == pcd.IDVATNUOI);
                var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == pet.IDKHACHHANG);
                CSLAppointmentSlipModel model = new CSLAppointmentSlipModel
                {
                    PHIEUCHIDINH = pcd,
                    VATNUOI = pet,
                    KHACHHANG = customer,
                    HANGMUC = hangmuc,
                    KETQUAXN = kqxn,
                };
                lists.Add(model);
            }

            return lists;
        }

        public List<CSLAppointmentSlipModel> laydsdonthuoc()
        {
            var donthuoc = db.DONTHUOC.Where(u => u.NGAYXOA == null).ToList();

            List<CSLAppointmentSlipModel> lists = new List<CSLAppointmentSlipModel>();
            foreach (var dt in donthuoc)
            {
                var lichkham = db.LICHHEN.FirstOrDefault(u => u.IDLICHKHAM == dt.IDLICHKHAM);
                var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == lichkham.IDKHACHHANG);
                var pet = db.VATNUOI.FirstOrDefault(c => c.IDVATNUOI == lichkham.IDVATNUOI);
                var dsdonthuoc = db.DANHSACHTHUOC.Where(u => u.MADSTHUOC == dt.MADSTHUOC).ToList();
                var account = db.TAIKHOAN.FirstOrDefault(u => u.IDTAIKHOAN == lichkham.IDTAIKHOAN);
                CSLAppointmentSlipModel model = new CSLAppointmentSlipModel
                {
                    DONTHUOC = dt,
                    VATNUOI = pet,
                    KHACHHANG = customer,
                    LISTDSDONTHUOC = dsdonthuoc,
                    LICHHEN = lichkham,
                    TAIKHOAN = account,
                };
                lists.Add(model);
            }

            return lists;
        }

        // lấy đơn thuốc gần nhất
        public HomeModel getDonThuoc(int id)
        {
            var dt = db.DONTHUOC
                            .FirstOrDefault(u => u.NGAYXOA == null && u.IDDONTHUOC == id);
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

            HomeModel model = new HomeModel
            {
                DONTHUOC = dt,
                TAIKHOAN = tk,
                VATNUOI = vn,
                KHACHHANG = kh,
                LICHHEN = lh,
                NHOMNGUOIDUNG = lnd,
                LISTDANHSACHTHUOC = dsthuoc,
                LISTTHUOCVAVATTU = tvt,
            };

            return model;
        }

        // Lấy danh sách chỉ định csl
        public List<CHIDINHCSL> getDSCSL()
        {
            var cd = db.CHIDINHCSL.Where(u => u.NGAYXOA == null).ToList();

            return cd;
        }

        public string adddmCD(CHIDINHCSL cd)
        {

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    DateTime date = DateTime.Now;
                    cd.NGAYTAO = date;

                    db.CHIDINHCSL.Add(cd);
                    db.SaveChanges();

                    // Commit giao dịch nếu không có lỗi
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
    }
}
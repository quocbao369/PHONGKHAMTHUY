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
    public class MedicineSevice
    {
        private DataSQL db = new DataSQL();
        // Dùng để lấy danh sách vật nuôi
        public List<THUOCVAVATTU> getAllMedicine()
        {
            var obj = db.THUOCVAVATTU.Where(u => u.NGAYXOA == null).ToList();
            return obj;
        }
        
        // Dùng để lấy danh sách danh mục
        public List<DANHMUC> getAllDirectory()
        {
            var dm = db.DANHMUC.Where(u => u.NGAYXOA == null).ToList();
            return dm;
        }

        // Lấy thông tin thuốc và vật tư
        public THUOCVAVATTU getInfor(int id)
        {
            THUOCVAVATTU thuoc = db.THUOCVAVATTU.FirstOrDefault(a => a.IDTHUOCVT == id);
            return thuoc;
        }

        // Thêm thuốc và vật tư mới
        public string addMedicine(THUOCVAVATTU thuoc)
        {
            if(thuoc.IDDANHMUC == 0|| thuoc.TENTHUOCVT == null|| thuoc.DONVI == null|| thuoc.GIABAN == 0|| thuoc.GIANHAP == 0|| thuoc.CACHDUNG == null||
                thuoc.QUYCACH == null|| thuoc.SOLUONGTRENNGAY == null|| thuoc.THANHPHAN == null|| thuoc.GHICHU == null || thuoc.TONKHO == 0)
            {
                return "Vui lòng điền đầy đủ thông tin" ;
            }
            var isthuoc = db.THUOCVAVATTU.FirstOrDefault(u => u.TENTHUOCVT == thuoc.TENTHUOCVT);
            if (isthuoc == null)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        DateTime date = DateTime.Now;
                        thuoc.NGAYTAO = date;


                        db.THUOCVAVATTU.Add(thuoc);
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
                return "Loại thuốc này đã tồn tại";
            }


        }

        // Cập nhật thông tin thuốc vật tư
        public string updateMedicine(THUOCVAVATTU thuoc)
        {
            var isthuoc = db.THUOCVAVATTU.FirstOrDefault(u => u.TENTHUOCVT == thuoc.TENTHUOCVT & u.IDTHUOCVT != thuoc.IDTHUOCVT);

            if(isthuoc != null)
            {
                return "Tên thuốc đã tồn tại";
            }
            THUOCVAVATTU tvt = db.THUOCVAVATTU.FirstOrDefault(a => a.IDTHUOCVT == thuoc.IDTHUOCVT);
            if(tvt != null)
            {
                tvt.IDDANHMUC = thuoc.IDDANHMUC;
                tvt.TENTHUOCVT = thuoc.TENTHUOCVT;
                tvt.DONVI = thuoc.DONVI;
                tvt.GIABAN = thuoc.GIABAN;
                tvt.GIANHAP = thuoc.GIANHAP;
                tvt.CACHDUNG = thuoc.CACHDUNG;
                tvt.QUYCACH = thuoc.QUYCACH;
                tvt.SOLUONGTRENNGAY = thuoc.SOLUONGTRENNGAY;
                tvt.THANHPHAN = thuoc.THANHPHAN;
                tvt.GHICHU = thuoc.GHICHU;
                tvt.HSD = thuoc.HSD;

                DateTime date = DateTime.Now;
                tvt.NGAYSUA = date;
                db.SaveChanges();
                return "Cập nhật thông tin thuốc và vật tư thành công";
            }
            else
            {
                return "Không tìm thấy thuốc và vật tư";
            }
        }

        // Xóa thuốc/vật tư

        public string deleteMedicine(int id)
        {

            // Lấy thông tin tài khoản từ cơ sở dữ liệu
            var thuoc = db.THUOCVAVATTU.FirstOrDefault(u => u.IDTHUOCVT == id);

            if (thuoc != null)
            {
                DateTime date = DateTime.Now;
                thuoc.NGAYXOA = date;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return "Xóa thành công";
            }
            else
            {
                return "Không tìm thấy thuốc/vật tư";
            }
        }

        // lấy danh sách danh mục thuốc
        public List<DANHMUC> getDMthuoc()
        {
            var obj = db.DANHMUC.Where(u => u.NGAYXOA == null).ToList();
            return obj;
        }

        // Thêm mới danh mục
        public string addDM(string tendm)
        {
            if (tendm == null)
            {
                return "Vui lòng điền đầy đủ thông tin";
            }
            var dm = db.DANHMUC.FirstOrDefault(u => u.TENDANHMUC == tendm);
            if (dm == null)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        DateTime date = DateTime.Now;
                        DANHMUC adddm = new DANHMUC();

                        adddm.NGAYTAO = date;
                        adddm.TENDANHMUC = tendm;


                        db.DANHMUC.Add(adddm);
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
                return "Danh mục này đã tồn tại";
            }


        }




        // Convert dạng thuốc và vật tư
        public List<MedicineModel> convertMCD(List<THUOCVAVATTU> listmedicine)
        {
            List<MedicineModel> mdcs = new List<MedicineModel>();
            foreach (var mdc in listmedicine)
            {
                // Lấy thông tin khách hàng từ cơ sở dữ liệu dựa trên IDKHACHHANG của vật nuôi
                var danhmuc = db.DANHMUC.FirstOrDefault(c => c.IDDANHMUC == mdc.IDDANHMUC);
                MedicineModel mcdModel = new MedicineModel
                {
                    IDTHUOCVT = mdc.IDTHUOCVT,
                    IDDANHMUC = mdc.IDDANHMUC,
                    TENTHUOCVT = mdc.TENTHUOCVT,
                    TONKHO = mdc.TONKHO,
                    DONVI = mdc.DONVI,
                    GIABAN = mdc.GIABAN,
                    GIANHAP = mdc.GIANHAP,
                    CACHDUNG = mdc.CACHDUNG,
                    QUYCACH = mdc.QUYCACH,
                    SOLUONGTRENNGAY = mdc.SOLUONGTRENNGAY,
                    THANHPHAN = mdc.THANHPHAN,
                    GHICHU = mdc.GHICHU,
                    NGAYTAO = mdc.NGAYTAO,
                    NGAYSUA = mdc.NGAYSUA,
                    NGAYXOA = mdc.NGAYXOA,  
                    
                    TENDANHMUC = danhmuc.TENDANHMUC,
                };
                mdcs.Add(mcdModel);
            }
            return mdcs;
        }
    }
}
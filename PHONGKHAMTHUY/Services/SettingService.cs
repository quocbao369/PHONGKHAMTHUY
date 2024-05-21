using Microsoft.Ajax.Utilities;
using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Services
{
    public class SettingService
    {
        private DataSQL db = new DataSQL();

        // Hàm này dùng để lấy danh sách tài khoản
        public List<TAIKHOAN> getAllAccount()
        {
            var obj = db.TAIKHOAN.Where(u => u.NGAYXOA == null).ToList();
            return obj;
        }

        // Hàm này dùng để lấy thông tin chi tiết
        public TAIKHOAN getAccountData(int accountId) {
            TAIKHOAN account = db.TAIKHOAN.FirstOrDefault(a => a.IDTAIKHOAN == accountId);
            if (account != null)
            {
                return account;
            }
            else
            {
                return null;
            }
        }

        // Hàm này dùng để lấy tên của nhóm người dùng
        public Dictionary<int, string> getNameAuth(List<TAIKHOAN> accounts)
        {
            var groupNames = new Dictionary<int, string>();

            // Tạo một danh sách chứa tất cả các IDNHOMNGUOIDUNG cần truy vấn
            var groupIds = accounts.Select(a => a.IDNHOMNGUOIDUNG).Distinct().ToList();

            // Truy vấn cơ sở dữ liệu để lấy thông tin của các nhóm người dùng
            var groups = db.NHOMNGUOIDUNG.Where(g => groupIds.Contains(g.IDNHOMNGUOIDUNG)).ToList();

            // Đổ dữ liệu vào từ điển
            foreach (var account in accounts)
            {
                var group = groups.FirstOrDefault(g => g.IDNHOMNGUOIDUNG == account.IDNHOMNGUOIDUNG);
                if (group != null && !groupNames.ContainsKey(account.IDNHOMNGUOIDUNG))
                {
                    groupNames.Add(account.IDNHOMNGUOIDUNG, group.TENNHOM);
                }
            }
            return groupNames;
        }


        // Hàm này dùng để thêm mới user account
        public string addNew(TAIKHOAN acc,string HDD) 
        {
            var isUsername = db.TAIKHOAN.FirstOrDefault(u => u.TENDANGNHAP == acc.TENDANGNHAP);
            var isEmail = db.TAIKHOAN.FirstOrDefault(u => u.EMAIL == acc.EMAIL);
            var isPhone = db.TAIKHOAN.FirstOrDefault(u => u.DIENTHOAI == acc.DIENTHOAI);
            if (isUsername != null)
            {
                return "Tên tài khoản đã tồn tại";
            }
            if (isEmail != null)
            {
                return "Email đã tồn tại";
            }
            if (isPhone != null)
            {
                return "Số điện thoại đã tồn tại";
            }
            else
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        DateTime date = DateTime.Now;
                        // Tạo một đối tượng TAIKHOAN mới và gán dữ liệu
                        TAIKHOAN account = new TAIKHOAN();
                        account.IDNHOMNGUOIDUNG = acc.IDNHOMNGUOIDUNG;
                        account.TENDANGNHAP = acc.TENDANGNHAP;
                        account.MATKHAU = acc.MATKHAU;
                        account.HOTEN = acc.HOTEN;
                        account.GIOITINH = acc.GIOITINH;
                        account.EMAIL = acc.EMAIL;
                        account.DIENTHOAI = acc.DIENTHOAI;
                        account.HINHDAIDIEN = HDD;
                        account.NGAYTHEM = date;

                        // Thêm đối tượng vào DbSet và lưu thay đổi vào cơ sở dữ liệu
                        db.TAIKHOAN.Add(account);
                        db.SaveChanges();

                        // Commit giao dịch nếu không có lỗi
                        dbContextTransaction.Commit();
                        return "Tạo tài khoản thành công";
                    }
                    catch (Exception)
                    {
                        // Nếu có lỗi, rollback giao dịch
                        dbContextTransaction.Rollback();
                        return "Tạo tài khoản thất bại";
                    }
                }
            }    
        }
        // Hàm này dùng để thêm mới user account
        public string updateUser(TAIKHOAN acc,string HDD)
        {

            // Nếu tài khoản khác sử dụng email hoặc số điện thoại này thì trả về thông báo lỗi
            if(acc.IDNHOMNGUOIDUNG == 0)
            {
                return "Vui lòng chọn nhóm người dùng";
            }

            // Lấy thông tin tài khoản từ cơ sở dữ liệu
            var account = db.TAIKHOAN.FirstOrDefault(u => u.IDTAIKHOAN == acc.IDTAIKHOAN);

            if (account != null)
            {
                // Cập nhật thông tin tài khoản
                account.IDNHOMNGUOIDUNG = acc.IDNHOMNGUOIDUNG;
                account.MATKHAU = acc.MATKHAU;
                account.HOTEN = acc.HOTEN;
                account.GIOITINH = acc.GIOITINH;
                account.EMAIL = acc.EMAIL;
                account.DIENTHOAI = acc.DIENTHOAI;
                if(HDD != null)
                {
                    account.HINHDAIDIEN = HDD;
                }
                DateTime date = DateTime.Now;
                account.NGAYSUA = date;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return "Cập nhật thông tin tài khoản thành công";
            }
            else
            {
                return "Không tìm thấy tài khoản";
            }
        }

        // Hàm này dùng để xóa user account
        public string delete(int IDTAIKHOAN)
        {

            // Lấy thông tin tài khoản từ cơ sở dữ liệu
            var account = db.TAIKHOAN.FirstOrDefault(u => u.IDTAIKHOAN == IDTAIKHOAN);

            if (account != null)
            {
                DateTime date = DateTime.Now;
                account.NGAYXOA = date;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return "Xóa tài khoản thành công";
            }
            else
            {
                return "Không tìm thấy tài khoản";
            }
        }
    }
}
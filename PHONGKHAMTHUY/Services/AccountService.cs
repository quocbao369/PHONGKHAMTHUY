using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Models;
using System;
using System.Linq;

namespace PHONGKHAMTHUY.Services
{
    public class AccountService
    {
        private DataSQL db = new DataSQL();

        /*Hàm kiểm tra thông tin tài khoản để đăng nhập*/
        public bool isLogin(LoginModel model)
        {
            var isAccount = db.TAIKHOAN.FirstOrDefault(u => u.TENDANGNHAP == model.TENDANGNHAP && u.MATKHAU == model.MATKHAU);

            // Nếu tồn tại tài khoản thì trả về true và ngược lại
            if (isAccount != null) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Hàm này dùng để lấy id của tài khoản
        public int getIdAccount(string username)
        {
            var acc = db.TAIKHOAN.FirstOrDefault(u => u.TENDANGNHAP == username);
            if (acc != null)
            {
                return acc.IDTAIKHOAN;
            }
            return 0;
        }
        public string getNameAccount(string username)
        {
            var acc = db.TAIKHOAN.FirstOrDefault(u => u.TENDANGNHAP == username);
            if (acc != null)
            {
                return acc.HOTEN;
            }
            return null;
        }

        public string getAvatarAccount(string username)
        {
            var acc = db.TAIKHOAN.FirstOrDefault(u => u.TENDANGNHAP == username);
            if (acc != null)
            {
                return acc.HINHDAIDIEN;
            }
            return null;
        }

        // Hàm này dùng đê đăng ký tài khoản
        public string isRegister(RegisterModel model)
        {

            var isUsername = db.TAIKHOAN.FirstOrDefault(u => u.TENDANGNHAP == model.TENDANGNHAP);
            var isEmail = db.TAIKHOAN.FirstOrDefault(u => u.EMAIL == model.EMAIL);
            var isPhone = db.TAIKHOAN.FirstOrDefault(u => u.DIENTHOAI == model.DIENTHOAI);
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
                        account.IDNHOMNGUOIDUNG = 6;
                        account.TENDANGNHAP = model.TENDANGNHAP;
                        account.MATKHAU = model.MATKHAU;
                        account.EMAIL = model.EMAIL;
                        account.DIENTHOAI = model.DIENTHOAI;
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
    }
}
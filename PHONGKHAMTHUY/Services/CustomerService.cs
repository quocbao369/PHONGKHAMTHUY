using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace PHONGKHAMTHUY.Services
{
    public class CustomerService
    {
        private DataSQL db = new DataSQL();
        // Hàm này dùng để lấy danh sách khách hàng
        public List<KHACHHANG> getAllCustomer()
        {
            var obj = db.KHACHHANG.Where(u => u.NGAYXOA == null).ToList();
            return obj;
        }

        // hàm này dùng để lấy thông tin khách hàng
        public KHACHHANG getCustomerData(int id)
        {
            KHACHHANG cs = db.KHACHHANG.FirstOrDefault(a => a.IDKHACHHANG == id);
            if (cs != null)
            {
                return cs;
            }
            else
            {
                return null;
            }
        }

        // hàm này dùng để update khách hàng
        public string updateCustomer(KHACHHANG kh)
        {
            var isPhone = db.KHACHHANG.FirstOrDefault(u => u.DIENTHOAI == kh.DIENTHOAI && u.IDKHACHHANG == kh.IDKHACHHANG);
            if (isPhone == null)
            {
                var isPhone2 = db.KHACHHANG.FirstOrDefault(u => u.DIENTHOAI == kh.DIENTHOAI);
                if (isPhone2 != null)
                {
                    return "Số điện thoại đã tồn tại";
                }
            }

            var cs = db.KHACHHANG.FirstOrDefault(u => u.IDKHACHHANG == kh.IDKHACHHANG);

            if (cs != null)
            {
                cs.HOTEN = kh.HOTEN;
                cs.DIENTHOAI = kh.DIENTHOAI;
                cs.DIACHI = kh.DIACHI;
                cs.GIOITINH = kh.GIOITINH;
                cs.LOAIKHACHHANG = kh.LOAIKHACHHANG;

                DateTime date = DateTime.Now;
                cs.NGAYSUA = date;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return "Cập nhật thông tin khách hàng thành công";
            }
            else
            {
                return "Không tìm thấy tài khoản";
            }
        }

        // Hàm này dùng để thêm mới vào cơ sở dữ liệu
        public string addCustomer(KHACHHANG kh, string TEN, string LOAI, string GIONG, string GIOITINHVN, string TUOI, string MAUSAC, string CANNANG, string NGAYSINH)
        {
            if (TEN != "")
            {
                return "Thêm vật nuôi";
            }
            if (kh.HOTEN == null)
            {
                return "Vui lòng điền thông tin khách hàng";
            }
            else
            {
                var isName = db.KHACHHANG.FirstOrDefault(u => u.HOTEN == kh.HOTEN);
                if (isName != null)
                {
                    return "Tên khách hàng đã tồn tại";
                }
                else
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DateTime date = DateTime.Now;

                            KHACHHANG cs = new KHACHHANG();
                            cs.LOAIKHACHHANG = kh.LOAIKHACHHANG;
                            cs.HOTEN = kh.HOTEN;
                            cs.DIENTHOAI = kh.DIENTHOAI;
                            cs.DIACHI = kh.DIACHI;
                            cs.GIOITINH = kh.GIOITINH;
                            cs.NGAYTAO = date;

                            // Thêm đối tượng vào DbSet và lưu thay đổi vào cơ sở dữ liệu
                            db.KHACHHANG.Add(cs);
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
        // Hàm này dùng để xóa khách hàng
        public string deleteCustomer(int IDKHACHHANG)
        {

            // Lấy thông tin tài khoản từ cơ sở dữ liệu
            var cs = db.KHACHHANG.FirstOrDefault(u => u.IDKHACHHANG == IDKHACHHANG);
            var petlist = db.VATNUOI
            .Where(u => u.IDKHACHHANG == IDKHACHHANG && u.NGAYXOA == null)
            .ToList();

            if (cs != null)
            {
                DateTime date = DateTime.Now;
                cs.NGAYXOA = date;
                if(petlist != null)
                {
                    foreach (var pet in petlist)
                    {
                        pet.NGAYXOA = date; // Đặt NGAYXOA thành thời gian hiện tại
                    }
                }

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
using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
using PHONGKHAMTHUY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHONGKHAMTHUY.Services
{
    public class PetSevice
    {
        private DataSQL db = new DataSQL();

        // Dùng để lấy danh sách vật nuôi
        public List<VATNUOI> getAllPet()
        {
            var obj = db.VATNUOI.Where(u => u.NGAYXOA == null).ToList();
            return obj;
        }

        // Lấy thông tin của vật nuôi
        public PetModel getPetInfo(int id)
        {
            VATNUOI pet = db.VATNUOI.FirstOrDefault(a => a.IDVATNUOI == id);
            KHACHHANG cs = db.KHACHHANG.FirstOrDefault(a => a.IDKHACHHANG == pet.IDKHACHHANG);
            if (pet != null)
            {
                PetModel petModel = new PetModel
                {
                    IDVATNUOI = pet.IDVATNUOI,
                    TEN = pet.TEN,
                    IDKHACHHANG = pet.IDKHACHHANG,
                    LOAI = pet.LOAI,
                    GIONG = pet.GIONG,
                    TUOI = pet.TUOI,
                    MAUSAC = pet.MAUSAC,
                    CANNANG = pet.CANNANG,
                    GIOITINH = pet.GIOITINH,
                    NGAYSINH = pet.NGAYSINH,
                    NGAYTAO = pet.NGAYTAO,
                    NGAYSUA = pet.NGAYSUA,
                    NGAYXOA = pet.NGAYXOA,
                    TENKHACHHANG = cs.HOTEN,
                };
                
                return petModel;
            }
            else
            {
                return null;
            }
        }

        // Thêm vật nuôi
        public string addPet(VATNUOI pet, string tenkhachhang, string gioitinh)
        {
            if(pet.TEN == null || pet.NGAYSINH == null || pet.LOAI == "" || gioitinh == "" ||
                pet.GIONG == "" || pet.MAUSAC == "" || pet.CANNANG == null || pet.TUOI == null)
            {
                return "Vui lòng điền đầy đủ thông tin";
            }
            if(tenkhachhang == null)
            {
                return "Vui lòng nhập tên khác hàng";
            }
            else
            {
                var customer = db.KHACHHANG.FirstOrDefault(u => u.HOTEN == tenkhachhang);
                if (customer != null)
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            DateTime date = DateTime.Now;
                            pet.NGAYTAO = date;
                            pet.IDKHACHHANG = customer.IDKHACHHANG;
                            pet.GIOITINH = gioitinh;

                            db.VATNUOI.Add(pet);
                            db.SaveChanges();

                            // Commit giao dịch nếu không có lỗi
                            dbContextTransaction.Commit();

                            return "Đã thêm thành công";
                        }
                        catch (Exception)
                        {
                            // Nếu có lỗi, rollback giao dịch
                            dbContextTransaction.Rollback();
                            return "Tạo tài khoản thất bại";
                        }
                    }
                }
                else
                {
                    return "Tên khách hàng đã tồn tại";
                }
            }

        }

        //Cập nhật vật nuôi
        public string updatePet(PetModel petModel)
        {
            var customer = db.KHACHHANG.FirstOrDefault(u => u.HOTEN == petModel.TENKHACHHANG);
            if (customer == null)
            {
                return "Tài khoản không tồn tại";

            }

            var pet = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == petModel.IDVATNUOI);

            if (pet != null)
            {
                pet.IDKHACHHANG = customer.IDKHACHHANG;
                pet.TEN = petModel.TEN;
                pet.GIONG = petModel.GIONG;
                pet.LOAI = petModel.LOAI;
                pet.MAUSAC = petModel.MAUSAC;
                pet.CANNANG = petModel.CANNANG;
                pet.GIOITINH = petModel.GIOITINH;
                pet.NGAYSINH = petModel.NGAYSINH;

                DateTime date = DateTime.Now;
                pet.NGAYSUA = date;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return "Cập nhật thông tin vật nuôi thành công";
            }
            else
            {
                return "Không tìm thấy vật nuôi";
            }
        }

        // Xóa vật nuôi
        public string deletePet(int id)
        {

            // Lấy thông tin tài khoản từ cơ sở dữ liệu
            var cs = db.VATNUOI.FirstOrDefault(u => u.IDVATNUOI == id);

            if (cs != null)
            {
                DateTime date = DateTime.Now;
                cs.NGAYXOA = date;

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return "Xóa vật nuôi thành công";
            }
            else
            {
                return "Không tìm thấy vật nuôi";
            }
        }

        // Convert dạng vật nuôi
        public List<PetModel> convertPet(List<VATNUOI> listpets)
        {
            List<PetModel> pets = new List<PetModel>();
            foreach (var pet in listpets)
            {
                // Lấy thông tin khách hàng từ cơ sở dữ liệu dựa trên IDKHACHHANG của vật nuôi
                var customer = db.KHACHHANG.FirstOrDefault(c => c.IDKHACHHANG == pet.IDKHACHHANG);
                PetModel petModel = new PetModel
                {
                    IDVATNUOI = pet.IDVATNUOI,
                    TEN = pet.TEN,
                    IDKHACHHANG = pet.IDKHACHHANG,
                    LOAI = pet.LOAI,
                    GIONG = pet.GIONG,
                    TUOI = pet.TUOI,
                    MAUSAC = pet.MAUSAC,
                    CANNANG = pet.CANNANG,
                    GIOITINH = pet.GIOITINH,
                    NGAYSINH = pet.NGAYSINH,
                    NGAYTAO = pet.NGAYTAO,
                    NGAYSUA = pet.NGAYSUA,
                    NGAYXOA = pet.NGAYXOA,
                    TENKHACHHANG = customer != null ? customer.HOTEN : "",
                };
                pets.Add(petModel);
            }
            return pets;
        }

    }
}
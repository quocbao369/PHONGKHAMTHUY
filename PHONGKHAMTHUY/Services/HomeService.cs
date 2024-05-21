using PHONGKHAMTHUY.Connect;
using PHONGKHAMTHUY.Domain;
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
            var obj = db.NHOMNGUOIDUNG.FirstOrDefault(a => a.IDNHOMNGUOIDUNG == id);
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
    }
}
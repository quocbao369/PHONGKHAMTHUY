using PHONGKHAMTHUY.Domain;
using System.Data.Entity;

namespace PHONGKHAMTHUY.Connect
{
    public class DataSQL : DbContext
    {
        public DataSQL() : base("name=PHONGKHAMDATA")
        {
            Database.SetInitializer<DataSQL>(null);
        }
        public DbSet<TAIKHOAN> TAIKHOAN { get; set; }
        public DbSet<DANHMUC> DANHMUC { get; set; }
        public DbSet<NHOMNGUOIDUNG> NHOMNGUOIDUNG { get; set; }
        public DbSet<KHACHHANG> KHACHHANG { get; set; }
        public DbSet<VATNUOI> VATNUOI { get; set; }

        public DbSet<THUOCVAVATTU> THUOCVAVATTU { get; set; }
        public DbSet<LICHHEN> LICHHEN { get; set; }

        public DbSet<DIENTIEN> DIENTIEN { get; set; }
        public DbSet<SINHHIEU> SINHHIEU { get; set; }
        public DbSet<CHUANDOAN> CHUANDOAN { get; set; }
        public DbSet<PHIEUCHIDINH> PHIEUCHIDINH { get; set; }

        public DbSet<DICHVU> DICHVU { get; set; }
        public DbSet<CHIDINHCSL> CHIDINHCSL { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PHONGKHAMTHUY.Domain
{
    [Table("NHOMNGUOIDUNG")]
    public class NHOMNGUOIDUNG
    {
        [Key]
        public int IDNHOMNGUOIDUNG { get; set; }

        [StringLength(50)]
        public string TENNHOM { get; set; }

        [StringLength(10)]
        public string QUYENHAN { get; set; }

        [StringLength(50)]
        public string TRANGTHAI { get; set; }
    }
}
namespace QLCuaHangThoiTrang_63133125.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTietDonHang = new HashSet<ChiTietDonHang>();
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDon { get; set; }
        [Display(Name = "Ngày đặt hàng")]

        public DateTime? NgayDat { get; set; }

        public int? TinhTrang { get; set; }
        [Display(Name = "Phương thức thanh toán")]

        public int? ThanhToan { get; set; }

        [Display(Name = "Tổng tiền")]

        public decimal? TongTien { get; set; }
        [Display(Name = "Tên người dùng")]

        public int? MaNguoiDung { get; set; }
        [Display(Name = "Địa chỉ nhận hàng")]

        public string DiaChiNhanHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}

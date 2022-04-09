namespace Travel_Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            ChiTietDatTours = new HashSet<ChiTietDatTour>();
        }

        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(5)]
        public string MaKhachHang { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Vui lòng nhập họ tên.")]
        public string Ten { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string SDT { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Vui lòng nhập email phù hợp")]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string MatKhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatTour> ChiTietDatTours { get; set; }
    }
}

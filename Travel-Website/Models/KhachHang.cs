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
        [StringLength(50)]
        public string Ten { get; set; }

        [StringLength(12)]
        public string SDT { get; set; }

        [StringLength(30)]
        public string TenDangNhap { get; set; }

        [StringLength(100)]
        public string MatKhau { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatTour> ChiTietDatTours { get; set; }
    }
}

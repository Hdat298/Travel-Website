namespace Travel_Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietDatTour")]
    public partial class ChiTietDatTour
    {
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(5)]
        public string MaChiTietDatTour { get; set; }

        public int? MaKhachHang { get; set; }

        public int? MaDatTour { get; set; }

        public virtual DatTour DatTour { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}

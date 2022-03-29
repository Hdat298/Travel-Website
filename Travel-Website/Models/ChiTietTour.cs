namespace Travel_Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietTour")]
    public partial class ChiTietTour
    {
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(5)]
        public string MaChiTietTour { get; set; }

        public int? MaTinhThanh { get; set; }

        public int? MaTour { get; set; }

        public virtual TinhThanh TinhThanh { get; set; }

        public virtual Tour Tour { get; set; }
    }
}

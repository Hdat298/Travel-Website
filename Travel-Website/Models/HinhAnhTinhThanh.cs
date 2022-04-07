namespace Travel_Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HinhAnhTinhThanh")]
    public partial class HinhAnhTinhThanh
    {
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(5)]
        public string MaHinhAnhTinhThanh { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        public int? MaTinhThanh { get; set; }

        [Column(TypeName = "image")]
        public byte[] HinhAnh { get; set; }

        public virtual TinhThanh TinhThanh { get; set; }

        public List<TinhThanh> ListTinhThanh = new List<TinhThanh>();
    }
}

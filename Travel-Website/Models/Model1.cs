using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Travel_Website.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ChiTietDatTour> ChiTietDatTours { get; set; }
        public virtual DbSet<ChiTietTour> ChiTietTours { get; set; }
        public virtual DbSet<DatTour> DatTours { get; set; }
        public virtual DbSet<HinhAnhTinhThanh> HinhAnhTinhThanhs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiTour> LoaiTours { get; set; }
        public virtual DbSet<TaiKhoanAdmin> TaiKhoanAdmins { get; set; }
        public virtual DbSet<TinhThanh> TinhThanhs { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDatTour>()
                .Property(e => e.MaChiTietDatTour)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietTour>()
                .Property(e => e.MaChiTietTour)
                .IsUnicode(false);

            modelBuilder.Entity<DatTour>()
                .Property(e => e.MaDatTour)
                .IsUnicode(false);

            modelBuilder.Entity<DatTour>()
                .HasMany(e => e.ChiTietDatTours)
                .WithOptional(e => e.DatTour)
                .HasForeignKey(e => e.MaDatTour);

            modelBuilder.Entity<HinhAnhTinhThanh>()
                .Property(e => e.MaHinhAnhTinhThanh)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKhachHang)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.ChiTietDatTours)
                .WithOptional(e => e.KhachHang)
                .HasForeignKey(e => e.MaKhachHang);

            modelBuilder.Entity<LoaiTour>()
                .Property(e => e.MaLoaiTour)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiTour>()
                .HasMany(e => e.Tours)
                .WithOptional(e => e.LoaiTour)
                .HasForeignKey(e => e.MaLoaiTour);

            modelBuilder.Entity<TaiKhoanAdmin>()
                .Property(e => e.MaTaiKhoanAdmin)
                .IsUnicode(false);

            modelBuilder.Entity<TinhThanh>()
                .Property(e => e.MaTinhThanh)
                .IsUnicode(false);

            modelBuilder.Entity<TinhThanh>()
                .HasMany(e => e.ChiTietTours)
                .WithOptional(e => e.TinhThanh)
                .HasForeignKey(e => e.MaTinhThanh);

            modelBuilder.Entity<TinhThanh>()
                .HasMany(e => e.HinhAnhTinhThanhs)
                .WithOptional(e => e.TinhThanh)
                .HasForeignKey(e => e.MaTinhThanh);

            modelBuilder.Entity<Tour>()
                .Property(e => e.MaTour)
                .IsUnicode(false);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.ChiTietTours)
                .WithOptional(e => e.Tour)
                .HasForeignKey(e => e.MaTour);

            modelBuilder.Entity<Tour>()
                .HasMany(e => e.DatTours)
                .WithOptional(e => e.Tour)
                .HasForeignKey(e => e.MaTour);
        }
    }
}

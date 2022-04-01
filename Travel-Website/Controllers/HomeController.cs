using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;
using System.Dynamic;

namespace Travel_Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            dynamic dy = new ExpandoObject();
            dy.hinhanhttlist = getHinhAnhTinhThanh();
            dy.tinhthanhlist = getTinhThanh();
            dy.tourlist = getTour();
            dy.loaitourlist = getLoaiTours();
            return View(dy);
        }

        public List<HinhAnhTinhThanh> getHinhAnhTinhThanh()
        {
            Model1 context = new Model1();
            List<HinhAnhTinhThanh> tinhThanhs = context.HinhAnhTinhThanhs.ToList();
            return tinhThanhs;
        }

        public List<TinhThanh> getTinhThanh()
        {
            Model1 context = new Model1();
            List<TinhThanh> tinhThanhs = context.TinhThanhs.ToList();
            return tinhThanhs;
        }

        public List<Tour> getTour()
        {
            Model1 context = new Model1();
            List<Tour> tinhThanhs = context.Tours.ToList();
            return tinhThanhs;
        }

        public List<LoaiTour> getLoaiTours()
        {
            Model1 context = new Model1();
            List<LoaiTour> tinhThanhs = context.LoaiTours.ToList();
            return tinhThanhs;
        }

        public ActionResult Signup(KhachHang KhachHang)
        {
            ViewBag.Message = "DDT Group - Đăng ký tài khoản";
            if (Request.Form.Count > 0)
            {
                Model1 context = new Model1();
                KhachHang KhachHang1 = new KhachHang();

                if (context.KhachHangs.Any(x => x.TenDangNhap == KhachHang.TenDangNhap))
                {
                    ViewBag.DuplicateMessage = "Tên đăng nhập đã tồn tại.";
                    return View();
                }

                KhachHang1.Ten = Request.Form["Ten"];
                KhachHang1.SDT = Request.Form["SDT"];
                KhachHang1.TenDangNhap = Request.Form["TenDangNhap"];
                KhachHang1.MatKhau = Request.Form["MatKhau"];

                context.KhachHangs.Add(KhachHang1);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public List<Tour> getspecificTour(int id)
        {
            Model1 context = new Model1();
            List<Tour> p = context.Tours.Where(x => x.ID == id).ToList();
            return p;
        }

        public ActionResult DatTour(int id)
        {
            dynamic dy1 = new ExpandoObject();
            dy1.tourlist = getspecificTour(id);
            //Model1 context = new Model1();
            //Tour p = context.Tours.FirstOrDefault(x => x.ID == id);
            return View(dy1);
        }
    }
}
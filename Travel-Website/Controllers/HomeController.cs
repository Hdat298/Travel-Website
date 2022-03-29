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

        public ActionResult Signup()
        {
            ViewBag.Message = "DDT Group - Đăng ký tài khoản";

            return View();
        }
    }
}
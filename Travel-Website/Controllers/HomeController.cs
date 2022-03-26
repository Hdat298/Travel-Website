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
            return View(dy);
        }

        public List<HinhAnhTinhThanh> getHinhAnhTinhThanh()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<HinhAnhTinhThanh> tinhThanhs = context.HinhAnhTinhThanhs.ToList();
            return tinhThanhs;
        }

        public List<TinhThanh> getTinhThanh()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<TinhThanh> tinhThanhs = context.TinhThanhs.ToList();
            return tinhThanhs;
        }

        public List<Tour> getTour()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<Tour> tinhThanhs = context.Tours.ToList();
            return tinhThanhs;
        }

        public ActionResult Signup()
        {
            ViewBag.Message = "DDT Group - Đăng ký tài khoản";

            return View();
        }
    }
}
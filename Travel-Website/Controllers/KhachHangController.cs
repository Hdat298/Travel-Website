using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        public ActionResult Index()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<KhachHang> KhachHangs = context.KhachHangs.ToList();
            return View(KhachHangs);
        }

        public ActionResult Details(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            KhachHang p = context.KhachHangs.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                KhachHang KhachHang = new KhachHang();
                KhachHang.Ten = Request.Form["TenKhachHang"];
                KhachHang.Tuoi = Request.Form["Tuoi"];
                KhachHang.GioiTinh = Request.Form["GioiTinh"];
                KhachHang.SDT = Request.Form["SDT"];
                KhachHang.TenDangNhap = Request.Form["TenDangNhap"];
                KhachHang.MatKhau = Request.Form["MatKhau"];

                context.KhachHangs.InsertOnSubmit(KhachHang);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            KhachHang KhachHang = context.KhachHangs.FirstOrDefault(x => x.ID == id);
            if (Request.Form.Count == 0)
            {
                return View(KhachHang);
            }
            KhachHang.Ten = Request.Form["TenKhachHang"];
            KhachHang.Tuoi = Request.Form["Tuoi"];
            KhachHang.GioiTinh = Request.Form["GioiTinh"];
            KhachHang.SDT = Request.Form["SDT"];
            KhachHang.TenDangNhap = Request.Form["TenDangNhap"];
            KhachHang.MatKhau = Request.Form["MatKhau"];

            context.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            KhachHang KhachHang = context.KhachHangs.FirstOrDefault(x => x.ID == id);
            if (KhachHang != null)
            {
                context.KhachHangs.DeleteOnSubmit(KhachHang);
                context.SubmitChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
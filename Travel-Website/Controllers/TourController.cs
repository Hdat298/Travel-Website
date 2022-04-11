using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class TourController : Controller
    {
        // GET: Tour
        public ActionResult Index()
        {
            if (Session["adAccount"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Model1 context = new Model1();
            List<Tour> Tours = context.Tours.ToList();
            return View(Tours);
        }

        public ActionResult Details(int id)
        {
            Model1 context = new Model1();
            Tour p = context.Tours.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        public ActionResult Create()
        {
            Tour Tour = new Tour();
            Model1 context = new Model1();
            Tour.ListLoaiTour = context.LoaiTours.ToList();
            return View(Tour);
        }

        [HttpPost]
        public ActionResult Create(Tour Tour, HttpPostedFileBase file)
        {
            Model1 context = new Model1();
            Tour.ListLoaiTour = context.LoaiTours.ToList();

            if (Request.Form.Count > 0)
            {
                
                Tour.TenTour = Request.Form["TenTour"];
                Tour.Gia = int.Parse(Request.Form["Gia"]);
                Tour.NgayKhoiHanh = Convert.ToDateTime(Request.Form["NgayKhoiHanh"], System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                Tour.NgayKetThuc = Convert.ToDateTime(Request.Form["NgayKetThuc"], System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
                Tour.SoCho = int.Parse(Request.Form["SoCho"]);
                Tour.NoiDung = Request.Form["NoiDung"];
                Tour.MaLoaiTour = int.Parse(Request.Form["MaLoaiTour"]);
                Tour.ChiTietTour = Request.Form["ChiTietTour"];

                if (file != null)
                {
                    Tour.HinhAnh = new byte[file.ContentLength];
                    file.InputStream.Read(Tour.HinhAnh, 0, file.ContentLength);
                }

                context.Tours.Add(Tour);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            Model1 context = new Model1();
            Tour Tour = context.Tours.FirstOrDefault(x => x.ID == id);
            Tour.ListLoaiTour = context.LoaiTours.ToList();
            return View(Tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase file)
        {
            ModelState.Clear();
            Model1 context = new Model1();
            Tour Tour = context.Tours.FirstOrDefault(x => x.ID == id);
            Tour.ListLoaiTour = context.LoaiTours.ToList();

            if (Request.Form.Count == 0)
            {
                return View(Tour);
            }

            Tour.TenTour = Request.Form["TenTour"];
            Tour.Gia = int.Parse(Request.Form["Gia"]);
            Tour.NgayKhoiHanh = Convert.ToDateTime(Request.Form["NgayKhoiHanh"], System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            Tour.NgayKetThuc = Convert.ToDateTime(Request.Form["NgayKetThuc"], System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);
            Tour.SoCho = int.Parse(Request.Form["SoCho"]);
            Tour.NoiDung = Request.Form["NoiDung"];
            Tour.MaLoaiTour = int.Parse(Request.Form["MaLoaiTour"]);
            Tour.ChiTietTour = Request.Form["ChiTietTour"];

            if (file != null)
            {
                Tour.HinhAnh = new byte[file.ContentLength];
                file.InputStream.Read(Tour.HinhAnh, 0, file.ContentLength);
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Model1 context = new Model1();
            Tour Tour = context.Tours.FirstOrDefault(x => x.ID == id);
            if (Tour != null)
            {
                context.Tours.Remove(Tour);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult tourDetail()
        {
            return View();
        }
    }
}
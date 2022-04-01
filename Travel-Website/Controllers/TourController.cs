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
            if (Request.Form.Count > 0)
            {
                Model1 context = new Model1();
                Tour Tour = new Tour();
                Tour.TenTour = Request.Form["TenTour"];
                Tour.Gia = int.Parse(Request.Form["Gia"]);
                Tour.NgayKhoiHanh = Convert.ToDateTime(Request.Form["NgayKhoiHanh"]);
                Tour.NgayKetThuc = Convert.ToDateTime(Request.Form["NgayKetThuc"]);
                Tour.SoCho = int.Parse(Request.Form["SoCho"]);
                Tour.NoiDung = Request.Form["NoiDung"]; 

                context.Tours.Add(Tour);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Model1 context = new Model1();
            Tour Tour = context.Tours.FirstOrDefault(x => x.ID == id);
            if (Request.Form.Count == 0)
            {
                return View(Tour);
            }
            Tour.TenTour = Request.Form["TenTour"];
            Tour.Gia = int.Parse(Request.Form["Gia"]);
            Tour.NgayKhoiHanh = Convert.ToDateTime(Request.Form["NgayKhoiHanh"]);
            Tour.NgayKetThuc = Convert.ToDateTime(Request.Form["NgayKetThuc"]);
            Tour.SoCho = int.Parse(Request.Form["SoCho"]);
            Tour.NoiDung = Request.Form["NoiDung"];

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
    }
}
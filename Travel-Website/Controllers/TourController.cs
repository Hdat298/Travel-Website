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
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<Tour> Tours = context.Tours.ToList();
            return View(Tours);
        }

        public ActionResult Details(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            Tour p = context.Tours.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        //public ActionResult HinhAnhs(int id)
        //{
        //    DataClasses1DataContext context = new DataClasses1DataContext();
        //    List<HinhAnhTour> Tours = context.HinhAnhTours.Where(x => x.MaTour == id).ToList();
        //    return View(Tours);
        //}

        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                Tour Tour = new Tour();
                Tour.TenTour = Request.Form["TenTour"];
                Tour.Gia = int.Parse(Request.Form["Gia"]);
                Tour.NgayKhoiHanh = Convert.ToDateTime(Request.Form["NgayKhoiHanh"]);
                Tour.NgayKetThuc = Convert.ToDateTime(Request.Form["NgayKetThuc"]);
                Tour.SoCho = int.Parse(Request.Form["SoCho"]);
                Tour.NoiDung = Request.Form["NoiDung"]; 

                context.Tours.InsertOnSubmit(Tour);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
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

            context.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            Tour Tour = context.Tours.FirstOrDefault(x => x.ID == id);
            if (Tour != null)
            {
                context.Tours.DeleteOnSubmit(Tour);
                context.SubmitChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
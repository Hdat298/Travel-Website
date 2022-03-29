using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class LoaiTourController : Controller
    {
        // GET: LoaiLoaiTour
        public ActionResult Index()
        {
            Model1 context = new Model1();
            List<LoaiTour> LoaiTours = context.LoaiTours.ToList();
            return View(LoaiTours);
        }

        public ActionResult Details(int id)
        {
            Model1 context = new Model1();
            LoaiTour p = context.LoaiTours.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        //public ActionResult HinhAnhs(int id)
        //{
        //    Model1 context = new Model1();
        //    List<HinhAnhLoaiTour> LoaiTours = context.HinhAnhLoaiTours.Where(x => x.MaLoaiTour == id).ToList();
        //    return View(LoaiTours);
        //}

        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                Model1 context = new Model1();
                LoaiTour LoaiTour = new LoaiTour();
                LoaiTour.TenLoaiTour = Request.Form["TenLoaiTour"];
                LoaiTour.Mota = Request.Form["MoTa"];


                context.LoaiTours.Add(LoaiTour);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Model1 context = new Model1();
            LoaiTour LoaiTour = context.LoaiTours.FirstOrDefault(x => x.ID == id);
            if (Request.Form.Count == 0)
            {
                return View(LoaiTour);
            }
            LoaiTour.TenLoaiTour = Request.Form["TenLoaiTour"];
            LoaiTour.Mota = Request.Form["MoTa"];

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Model1 context = new Model1();
            LoaiTour LoaiTour = context.LoaiTours.FirstOrDefault(x => x.ID == id);
            if (LoaiTour != null)
            {
                context.LoaiTours.Remove(LoaiTour);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
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
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<LoaiTour> LoaiTours = context.LoaiTours.ToList();
            return View(LoaiTours);
        }

        public ActionResult Details(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            LoaiTour p = context.LoaiTours.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        //public ActionResult HinhAnhs(int id)
        //{
        //    DataClasses1DataContext context = new DataClasses1DataContext();
        //    List<HinhAnhLoaiTour> LoaiTours = context.HinhAnhLoaiTours.Where(x => x.MaLoaiTour == id).ToList();
        //    return View(LoaiTours);
        //}

        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                LoaiTour LoaiTour = new LoaiTour();
                LoaiTour.TenLoaiTour = Request.Form["TenLoaiTour"];
                LoaiTour.Mota = Request.Form["MoTa"];


                context.LoaiTours.InsertOnSubmit(LoaiTour);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            LoaiTour LoaiTour = context.LoaiTours.FirstOrDefault(x => x.ID == id);
            if (Request.Form.Count == 0)
            {
                return View(LoaiTour);
            }
            LoaiTour.TenLoaiTour = Request.Form["TenLoaiTour"];
            LoaiTour.Mota = Request.Form["MoTa"];

            context.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            LoaiTour LoaiTour = context.LoaiTours.FirstOrDefault(x => x.ID == id);
            if (LoaiTour != null)
            {
                context.LoaiTours.DeleteOnSubmit(LoaiTour);
                context.SubmitChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
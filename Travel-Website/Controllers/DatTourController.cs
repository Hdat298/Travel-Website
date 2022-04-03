using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class DatTourController : Controller
    {
        // GET: DatTour
        public ActionResult Index()
        {
            Model1 context = new Model1();
            List<DatTour> KhachHangs = context.DatTours.ToList();
            return View(KhachHangs);
        }
        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                Model1 context = new Model1();
                DatTour tinhThanh = new DatTour();
                tinhThanh.NgayDat = Convert.ToDateTime(Request.Form["NgayDat"]);
                tinhThanh.SoCho = int.Parse(Request.Form["SoCho"]);
                tinhThanh.ThanhTien = int.Parse(Request.Form["ThanhTien"]);
                tinhThanh.MaTour = int.Parse(Request.Form["MaTour"]);

                context.DatTours.Add(tinhThanh);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
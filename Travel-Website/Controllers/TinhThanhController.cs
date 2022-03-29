using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class TinhThanhController : Controller
    {
        // GET: TinhThanh
        public ActionResult Index()
        {
            Model1 context = new Model1();
            List<TinhThanh> tinhThanhs = context.TinhThanhs.ToList();
            return View(tinhThanhs);
        }

        public ActionResult Details(int id)
        {
            Model1 context = new Model1();
            TinhThanh p = context.TinhThanhs.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        public ActionResult HinhAnhs(int id)
        {
            Model1 context = new Model1();
            List<HinhAnhTinhThanh> TinhThanhs = context.HinhAnhTinhThanhs.Where(x => x.MaTinhThanh == id).ToList();
            return View(TinhThanhs);
        }

        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                Model1 context = new Model1();
                TinhThanh tinhThanh = new TinhThanh();
                tinhThanh.TenTinhThanh = Request.Form["TenTinhThanh"];
                tinhThanh.MoTa = Request.Form["MoTa"];

                context.TinhThanhs.Add(tinhThanh);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Model1 context = new Model1();
            TinhThanh tinhThanh = context.TinhThanhs.FirstOrDefault(x => x.ID == id);
            if (Request.Form.Count == 0)
            {
                return View(tinhThanh);
            }
            tinhThanh.TenTinhThanh = Request.Form["TenTinhThanh"];
            tinhThanh.MoTa = Request.Form["MoTa"];

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Model1 context = new Model1();
            TinhThanh tinhThanh = context.TinhThanhs.FirstOrDefault(x => x.ID == id);
            if (tinhThanh != null)
            {
                context.TinhThanhs.Remove(tinhThanh);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
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
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<TinhThanh> tinhThanhs = context.TinhThanhs.ToList();
            return View(tinhThanhs);
        }

        public ActionResult Details(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            TinhThanh p = context.TinhThanhs.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        public ActionResult HinhAnhs(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<HinhAnhTinhThanh> TinhThanhs = context.HinhAnhTinhThanhs.Where(x => x.MaTinhThanh == id).ToList();
            return View(TinhThanhs);
        }

        public ActionResult Create()
        {
            if (Request.Form.Count > 0)
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                TinhThanh tinhThanh = new TinhThanh();
                tinhThanh.TenTinhThanh = Request.Form["TenTinhThanh"];
                tinhThanh.MoTa = Request.Form["MoTa"];

                context.TinhThanhs.InsertOnSubmit(tinhThanh);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            TinhThanh tinhThanh = context.TinhThanhs.FirstOrDefault(x => x.ID == id);
            if (Request.Form.Count == 0)
            {
                return View(tinhThanh);
            }
            tinhThanh.TenTinhThanh = Request.Form["TenTinhThanh"];
            tinhThanh.MoTa = Request.Form["MoTa"];

            context.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            TinhThanh tinhThanh = context.TinhThanhs.FirstOrDefault(x => x.ID == id);
            if(tinhThanh != null)
            {
                context.TinhThanhs.DeleteOnSubmit(tinhThanh);
                context.SubmitChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
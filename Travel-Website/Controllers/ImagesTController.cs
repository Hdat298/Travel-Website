using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;
using System.Web.UI.WebControls;
using System.IO;

namespace Travel_Website.Controllers
{
    public class ImagesTController : Controller
    {
        // GET: ImagesT
        public ActionResult Index()
        {
            Model1 context = new Model1();
            List<HinhAnhTinhThanh> tinhThanhs = context.HinhAnhTinhThanhs.ToList();
            return View(tinhThanhs);
        }

        public ActionResult Details(int id)
        {
            Model1 context = new Model1();
            HinhAnhTinhThanh p = context.HinhAnhTinhThanhs.FirstOrDefault(x => x.ID == id);
            return View(p);
        }
        public ActionResult Create()
        {
            HinhAnhTinhThanh hinhanhtinhthanh = new HinhAnhTinhThanh();
            Model1 context = new Model1();
            hinhanhtinhthanh.ListTinhThanh = context.TinhThanhs.ToList();
            return View(hinhanhtinhthanh);
        }
        [HttpPost]
        public ActionResult Create(HinhAnhTinhThanh hinhAnhTinhThanh, HttpPostedFileBase file)
        {
            Model1 context = new Model1();
            hinhAnhTinhThanh.MoTa = Request.Form["Mota"];
            hinhAnhTinhThanh.MaTinhThanh = int.Parse(Request.Form["MaTinhThanh"]);

            if (file != null)
            {
                hinhAnhTinhThanh.HinhAnh = new byte[file.ContentLength];
                file.InputStream.Read(hinhAnhTinhThanh.HinhAnh, 0, file.ContentLength);
            }

            context.HinhAnhTinhThanhs.Add(hinhAnhTinhThanh);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {        
            Model1 context = new Model1();
            HinhAnhTinhThanh hinhanhtinhthanh = context.HinhAnhTinhThanhs.FirstOrDefault(x => x.ID == id);
            hinhanhtinhthanh.ListTinhThanh = context.TinhThanhs.ToList();
            return View(hinhanhtinhthanh);
        }

        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase file)
        {
            Model1 context = new Model1();
            HinhAnhTinhThanh hinhAnhTinhThanh = context.HinhAnhTinhThanhs.FirstOrDefault(x => x.ID == id);

            if (Request.Form.Count == 0)
            {
                return View(hinhAnhTinhThanh);
            }

            hinhAnhTinhThanh.MoTa = Request.Form["MoTa"];
            hinhAnhTinhThanh.MaTinhThanh = int.Parse(Request.Form["MaTinhThanh"]);

            if (file != null)
            {
                hinhAnhTinhThanh.HinhAnh = new byte[file.ContentLength];
                file.InputStream.Read(hinhAnhTinhThanh.HinhAnh, 0, file.ContentLength);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Model1 context = new Model1();
            HinhAnhTinhThanh KhachHang = context.HinhAnhTinhThanhs.FirstOrDefault(x => x.ID == id);
            if (KhachHang != null)
            {
                context.HinhAnhTinhThanhs.Remove(KhachHang);
                context.SaveChanges(); 
            }
            return RedirectToAction("Index");
        }
    }
}
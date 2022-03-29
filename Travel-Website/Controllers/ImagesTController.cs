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
            HinhAnhTinhThanh context = new HinhAnhTinhThanh();
            return View(context);
        }
        [HttpPost]
        public ActionResult Create(HinhAnhTinhThanh hinhAnhTinhThanh, HttpPostedFileBase file)
        {
            Model1 context = new Model1();

            hinhAnhTinhThanh.MaTinhThanh = int.Parse(Request.Form["MaTinhThanh"]);
            hinhAnhTinhThanh.MoTa = Request.Form["Mota"];

            if(file != null)
            {
                hinhAnhTinhThanh.HinhAnh = new byte[file.ContentLength];
                file.InputStream.Read(hinhAnhTinhThanh.HinhAnh, 0, file.ContentLength);
            }
            context.HinhAnhTinhThanhs.Add(hinhAnhTinhThanh);
            context.SaveChanges();
            return View(hinhAnhTinhThanh);
        }

        //public ActionResult Edit(int id)
        //{
        //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        //    Model1 context = new Model1();
        //    HinhAnhTinhThanh hinhAnh = context.HinhAnhTinhThanhs.FirstOrDefault(x => x.ID == id);
        //    if (Request.Form.Count == 0)
        //    {
        //        return View(hinhAnh);
        //    }
        //    hinhAnh.MaTinhThanh = int.Parse(Request.Form["MaTinhThanh"]);
        //    hinhAnh.MoTa = Request.Form["MoTa"];
        //    HttpPostedFileBase file = Request.Files["HinhAnh"];
        //        if (file != null && file.FileName!= "")
        //        {
        //            string serverPath = HttpContext.Server.MapPath("E:/C#/TH_Web/Đồ án web/Travel-Website/Travel-Website/Content/Images");
        //            string filepath = serverPath + "/" + file.FileName;
        //            file.SaveAs(filepath);
        //            hinhAnh.HinhAnh = new System.Data.Linq.Binary(encoding.GetBytes(file.FileName));
        //        }
        //        context.SubmitChanges();
        //        return RedirectToAction("Index");
        //}

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
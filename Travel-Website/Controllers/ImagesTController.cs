using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class ImagesTController : Controller
    {
        // GET: ImagesT
        public ActionResult Index()
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            List<HinhAnhTinhThanh> tinhThanhs = context.HinhAnhTinhThanhs.ToList();
            return View(tinhThanhs);
        }

        public ActionResult Details(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            HinhAnhTinhThanh p = context.HinhAnhTinhThanhs.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        public ActionResult Create(HttpPostedFileBase file)
        {
            if (Request.Form.Count>0)
            {
                DataClasses1DataContext context = new DataClasses1DataContext();
                HinhAnhTinhThanh hinhAnh = new HinhAnhTinhThanh();
                hinhAnh.MaTinhThanh = int.Parse(Request.Form["MaTinhThanh"]);
                hinhAnh.MoTa = Request.Form["MoTa"];             

                if(file != null)
                {
                   hinhAnh.HinhAnh = new byte[file.ContentLength];
                   file.InputStream.Read(hinhAnh.HinhAnh.ToArray(), 0, file.ContentLength);
                }

                context.HinhAnhTinhThanhs.InsertOnSubmit(hinhAnh);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            DataClasses1DataContext context = new DataClasses1DataContext();
            HinhAnhTinhThanh hinhAnh = context.HinhAnhTinhThanhs.FirstOrDefault(x => x.ID == id);
            if (Request.Form.Count == 0)
            {
                return View(hinhAnh);
            }
            hinhAnh.MaTinhThanh = int.Parse(Request.Form["MaTinhThanh"]);
            hinhAnh.MoTa = Request.Form["MoTa"];
            HttpPostedFileBase file = Request.Files["HinhAnh"];
                if (file != null && file.FileName!= "")
                {
                    string serverPath = HttpContext.Server.MapPath("E:/C#/TH_Web/Đồ án web/Travel-Website/Travel-Website/Content/Images");
                    string filepath = serverPath + "/" + file.FileName;
                    file.SaveAs(filepath);
                    hinhAnh.HinhAnh = new System.Data.Linq.Binary(encoding.GetBytes(file.FileName));
                }
                context.SubmitChanges();
                return RedirectToAction("Index");
        }
    }
}
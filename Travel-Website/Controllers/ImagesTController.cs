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
  
        public ActionResult Create(HttpPostedFileBase HinhAnh1)
        {
            if (Request.Form.Count>0)
            {

                DataClasses1DataContext context = new DataClasses1DataContext();
                HinhAnhTinhThanh hinhAnh = new HinhAnhTinhThanh();
                
                hinhAnh.MaTinhThanh = int.Parse(Request.Form["MaTinhThanh"]);
                hinhAnh.MoTa = Request.Form["MoTa"];

                byte[] bytes;
                using (BinaryReader br = new BinaryReader(HinhAnh1.InputStream))
                {
                    bytes = br.ReadBytes(HinhAnh1.ContentLength);
                }

                hinhAnh.HinhAnh = bytes;

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

        public ActionResult Delete(int id)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            HinhAnhTinhThanh tinhThanh = context.HinhAnhTinhThanhs.FirstOrDefault(x => x.ID == id);
            if (tinhThanh != null)
            {
                context.HinhAnhTinhThanhs.DeleteOnSubmit(tinhThanh);
                context.SubmitChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
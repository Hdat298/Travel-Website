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
    }
}
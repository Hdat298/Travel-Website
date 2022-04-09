using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class SearchTourController : Controller
    {

        Model1 context = new Model1();

        [HttpPost]
        public ActionResult getKeyWord(string keyword)
        {
            return RedirectToAction("searchResult", new { @Keyword = keyword });
        }

        [HttpGet]
        public ActionResult searchResult(string keyword)
        {
            var lstTour = context.Tours.Where(p => p.TenTour.Contains(keyword));
            if (lstTour.Count() == 0)
            {
                RedirectToAction("Index", "Home");
            }
            ViewBag.KeyWord = keyword;
            return View(lstTour.OrderBy(n => n.TenTour));
        }
    }
}
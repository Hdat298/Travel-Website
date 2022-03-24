using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel_Website.Areas.admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: admin/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}
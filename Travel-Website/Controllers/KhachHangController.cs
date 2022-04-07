using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        public ActionResult Index()
        {
            Model1 context = new Model1();
            List<KhachHang> KhachHangs = context.KhachHangs.ToList();
            return View(KhachHangs);
        }

        public ActionResult Details(int id)
        {
            Model1 context = new Model1();
            KhachHang p = context.KhachHangs.FirstOrDefault(x => x.ID == id);
            return View(p);
        }

        public ActionResult Create()
        {
            try
            {
                if (Request.Form.Count > 0)
                {
                    Model1 context = new Model1();
                    KhachHang KhachHang = new KhachHang();

                    if(context.KhachHangs.Any(x=> x.TenDangNhap == KhachHang.TenDangNhap))
                    {
                        ViewBag.DuplicateMessage = "Tên đăng nhập đã tồn tại.";
                        return View("");
                    }

                    KhachHang.Ten = Request.Form["Ten"];
                    KhachHang.SDT = Request.Form["SDT"];
                    KhachHang.TenDangNhap = Request.Form["TenDangNhap"];
                    KhachHang.MatKhau = Request.Form["MatKhau"];

                    context.KhachHangs.Add(KhachHang);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            Model1 context = new Model1();
            KhachHang KhachHang = context.KhachHangs.FirstOrDefault(x => x.ID == id);

            if (Request.Form.Count == 0)
            {
                return View(KhachHang);
            }

            KhachHang.Ten = Request.Form["TenKhachHang"];
            KhachHang.SDT = Request.Form["SDT"];
            KhachHang.TenDangNhap = Request.Form["TenDangNhap"];
            KhachHang.MatKhau = Request.Form["MatKhau"];

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Model1 context = new Model1();
            KhachHang KhachHang = context.KhachHangs.FirstOrDefault(x => x.ID == id);
            if (KhachHang != null)
            {
                context.KhachHangs.Remove(KhachHang);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
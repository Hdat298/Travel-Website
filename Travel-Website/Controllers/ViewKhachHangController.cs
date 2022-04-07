using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Controllers
{
    public class ViewKhachHangController : Controller
    {
        // GET: ViewKhachHang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongTinKhachhHang()
        {       
            Model1 context = new Model1();
            var userid = Session["Account"] as KhachHang;

            KhachHang khachhang = context.KhachHangs.FirstOrDefault(x => x.ID == userid.ID);

            return View(khachhang);
        }

        public ActionResult Edit(int id)
        {
            Model1 context = new Model1();
            var userid = Session["Account"] as KhachHang;

            KhachHang khachhang = context.KhachHangs.FirstOrDefault(x => x.ID == id);

            
            if (Request.Form.Count == 0)
            {
                return View(khachhang);
            }

            //khachhang.Ten = Request.Form["TenKhachHang"];
            khachhang.SDT = Request.Form["SDT"];
            khachhang.MatKhau = Request.Form["MatKhau"];

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
            return RedirectToAction("ThongTinKhachhHang", "ViewKhachHang");
        }

        public ActionResult ThongTinTour()
        {
            var userid = Session["Account"] as KhachHang;

            dynamic dy = new ExpandoObject();
            dy.chitietdattourlist = getChiTietDatTour(userid.ID);
            return View(dy);
        }

        public List<ChiTietDatTour> getChiTietDatTour(int id)
        {
            Model1 context = new Model1();
            List<ChiTietDatTour> chitietdattour = context.ChiTietDatTours.Where(x => x.MaKhachHang == id).ToList();
            return chitietdattour;
        }
    }
}
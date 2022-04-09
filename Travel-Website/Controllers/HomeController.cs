using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;
using System.Dynamic;
using System.Net.Mail;
using System.Net;
using System.Data.Entity.Migrations;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient.Memcached;
using PagedList;

namespace Travel_Website.Controllers
{
    public class HomeController : Controller
    {

        public void GuiEmail(string Title, string ToEmail, string FromEmail, string PassWord, string Content)
        {
            // goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail); // Địa chi nhận
            mail.From = new MailAddress(ToEmail); // Địa chửi gừi
            mail.Subject = Title; // tiêu đề gửi
            mail.Body = Content; // Nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của Gmail
            smtp.Port = 587;  // port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, PassWord);//Tài khoản password người gửi
            smtp.EnableSsl = true; //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail); //Gửi mail đi

        }

        public string transMD5 (string password)
        {
            MD5 mh = MD5.Create();
            //chuyen chuoi thanh byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            //ma hoa chuoi da chuyen
            byte[] hash = mh.ComputeHash(inputBytes);
            //tao doi tuong StringBuilder (lam viec voi kieu du lieu lon)
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public string randomPass()
        {
            string Numrd_str;
            Random rd = new Random();
            Numrd_str = rd.Next(100000, 1000000).ToString();
            return Numrd_str;
        }

        public ActionResult Index()
        {
            dynamic dy = new ExpandoObject();
            dy.hinhanhttlist = getHinhAnhTinhThanh();
            dy.tinhthanhlist = getTinhThanh();
            dy.tourlist = getTour();
            dy.loaitourlist = getLoaiTours();
            return View(dy);
        }

        public List<HinhAnhTinhThanh> getHinhAnhTinhThanh()
        {
            Model1 context = new Model1();
            List<HinhAnhTinhThanh> tinhThanhs = context.HinhAnhTinhThanhs.ToList();
            return tinhThanhs;
        }

        public List<TinhThanh> getTinhThanh()
        {
            Model1 context = new Model1();
            List<TinhThanh> tinhThanhs = context.TinhThanhs.ToList();
            return tinhThanhs;
        }

        public List<Tour> getTour()
        {
            Model1 context = new Model1();
            List<Tour> tinhThanhs = context.Tours.ToList();
            return tinhThanhs;
        }

        public List<LoaiTour> getLoaiTours()
        {
            Model1 context = new Model1();
            List<LoaiTour> tinhThanhs = context.LoaiTours.ToList();
            return tinhThanhs;
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(KhachHang KhachHang)
        {
            ViewBag.Message = "DDT Group - Đăng ký tài khoản";
            if(ModelState.IsValid)
            {
                if (Request.Form.Count > 0)
                {
                    Model1 context = new Model1();
                    KhachHang KhachHang1 = new KhachHang();

                    if (context.KhachHangs.Any(x => x.TenDangNhap == KhachHang.TenDangNhap))
                    {
                        ViewBag.DuplicateMessage = "Tên đăng nhập đã tồn tại.";
                        return View();
                    }

                    KhachHang1.Ten = Request.Form["Ten"];
                    KhachHang1.SDT = Request.Form["SDT"];
                    KhachHang1.TenDangNhap = Request.Form["TenDangNhap"];
                    KhachHang1.MatKhau = Request.Form["MatKhau"];

                    context.KhachHangs.Add(KhachHang1);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
           
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            Model1 context = new Model1();
            string sUser = f["txtUser"].ToString();
            string sPass = f["txtPass"].ToString();
            var GiaiPass = transMD5(sPass);

            KhachHang kh = context.KhachHangs.SingleOrDefault(p => p.TenDangNhap == sUser && p.MatKhau == GiaiPass);
            if (kh != null)
            {
                Session["Account"] = kh;
                return RedirectToAction("Index", "Home");
            }
            TempData["msg"] = "<script>alert('Sai tài khoản hoặc mật khẩu.');</script>";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["Account"] = null;
            return RedirectToAction("Index", "Home");
        }

        public List<Tour> GetTours(int id)
        {
            Model1 context = new Model1();
            List<Tour> p = context.Tours.Where(x => x.ID == id).ToList();
            return p;
        }
        public ActionResult DatTour(int id)
        {
            dynamic dy = new ExpandoObject();
            dy.Tours = GetTours(id);
            TempData["msg1"] = "<script>alert('Vui lòng đăng nhập để đặt tour.');</script>";
            return View(dy);
        }

        public ActionResult DatTour2(int id)
        {
            var userid = Session["Account"] as KhachHang;
            Model1 context = new Model1();
            var Tour = context.Tours.SingleOrDefault(p => p.ID == id);

            if (Request.Form.Count > 0)
            {
                DatTour tinhthanh = new DatTour();

                tinhthanh.NgayDat = DateTime.Now;
                tinhthanh.SoCho = int.Parse(Request.Form["SoCho"]);
                tinhthanh.ThanhTien = Tour.Gia * tinhthanh.SoCho;
                tinhthanh.MaTour = id;
                
                context.DatTours.Add(tinhthanh);
                context.SaveChanges();

                ViewBag.ThanhTien = tinhthanh.ThanhTien;

                var DatTour = new ChiTietDatTour();

                DatTour.MaDatTour = tinhthanh.ID;
                DatTour.MaKhachHang = userid.ID;
                
                context.ChiTietDatTours.Add(DatTour);
                context.SaveChanges();                            
            }
            return View();
        }

        public ActionResult forgotPassword (string email)
        {
            Model1 context = new Model1();
            string email1 = email;
            var s = context.KhachHangs.FirstOrDefault(p => p.TenDangNhap == email1);
            if (s != null)
            {
                //var mail = new SmtpClient("smtp.gmail.com", 587)
                //{
                //    //Credentials = new NetworkCredential("minhtan12374@gmail.com", "Minh@123"),
                //    Credentials = new NetworkCredential("daonhattin12@gmail.com", "nhattin12"),
                //    EnableSsl = true,
                //    UseDefaultCredentials = false
                ////};
                //MailMessage message = new MailMessage();
                //message.From = new MailAddress("daonhattin12@gmail.com");
                //message.ReplyToList.Add("daonhattin12@gmail.com");
                //message.To.Add(new MailAddress(s.TenDangNhap));
                //message.Subject = "Thông báo về việc thay đổi mật khẩu của DDT Tour";
                string pass = randomPass();
                //message.Body = "Mật khẩu của bạn đã được reset thành " + pass;
                s.MatKhau = transMD5(pass);
                context.KhachHangs.AddOrUpdate(s);
                context.SaveChanges();
                //mail.Send(message);
                GuiEmail("Thông báo về việc thay đổi mật khẩu của DDT Tour", s.TenDangNhap, "daonhattin12@gmail.com", "nhattin12", "Mật khẩu của bạn đã được reset thành " + pass);
                ViewBag.Message = "Đã gửi mail thành công!!! Vui lòng kiểm tra email";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Địa chỉ Email không chính xác";
                return RedirectToAction("Index","Home");
            }
        }
        public ActionResult TourDetails(int id)
        {
            Model1 context = new Model1();
            Tour p = context.Tours.FirstOrDefault(x => x.ID == id);
            return View(p);
        }


        public ActionResult TourList(int id, int? page)
        {
            Model1 context = new Model1();
            int pageNumber = page ?? 1;
            int pageSize = 2;
            var tourlist = context.Tours.Where(x => x.MaLoaiTour == id).OrderBy(y => y.TenTour).ToPagedList(pageNumber, pageSize);
            
            return View(tourlist);
        }
    }
}
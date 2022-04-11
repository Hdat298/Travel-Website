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
using System.Configuration;
using Travel_Website.Others;

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
            dy.tourlist = getTour();
            dy.loaitourlist = getLoaiTours();
            //dy.lienhe = getLienHes();
            return View(dy);
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

        //public List<LienHe> getLienHes()
        //{
        //    Model1 context = new Model1();
        //    List<LienHe> tinhThanhs = context.LienHes.ToList();
        //    return tinhThanhs;
        //}

        [HttpPost]
        public ActionResult contact(FormCollection ff)
        {
            string kName = ff["txtName"].ToString();
            string kMail = ff["txtMail"].ToString();
            string kPhone = ff["txtPhone"].ToString();
            string kSubject = ff["txtSubject"].ToString();
            string kContent = ff["txtContent"].ToString();
            Model1 context = new Model1();
            LienHe contact = new LienHe();
            contact.Ten = kName;
            contact.SDT = kPhone;
            contact.email = kMail;
            contact.NoiDung = kSubject;
            contact.TinNhan = kContent;
            context.LienHes.Add(contact);
            context.SaveChanges();
            TempData["msg2"] = "<script>alert('Cảm ơn quý khách đã đóng góp, chúng tôi sẽ tiếp nhận thông tin.');</script>";
            return RedirectToAction("Index", "Home");
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
                    KhachHang1.MatKhau = transMD5(Request.Form["MatKhau"]);

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
                var TongTien = tinhthanh.ThanhTien;
                context.DatTours.Add(tinhthanh);
                context.SaveChanges();

                ViewBag.ThanhTien = tinhthanh.ThanhTien;

                var DatTour = new ChiTietDatTour();
                DatTour.MaDatTour = tinhthanh.ID;
                DatTour.MaKhachHang = userid.ID;
                context.ChiTietDatTours.Add(DatTour);
                context.SaveChanges();

                string url = ConfigurationManager.AppSettings["Url"];
                string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
                string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

                PayLib pay = new PayLib();

                pay.AddRequestData("vnp_Version", "2.0.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
                pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
                pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
                pay.AddRequestData("vnp_Amount", (TongTien*100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
                pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
                pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
                pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
                pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
                pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
                pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
                pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
                pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

                string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

                return Redirect(paymentUrl);

                //DatTour tinhthanh = new DatTour();

                //tinhthanh.NgayDat = DateTime.Now;
                //tinhthanh.SoCho = int.Parse(Request.Form["SoCho"]);
                //tinhthanh.ThanhTien = Tour.Gia * tinhthanh.SoCho;
                //tinhthanh.MaTour = id;
                
                //context.DatTours.Add(tinhthanh);
                //context.SaveChanges();

                //ViewBag.ThanhTien = tinhthanh.ThanhTien;                      
            }
            return View();
        }

        //Payment
        public ActionResult Payment(int id)
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.0.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", "1000000"); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }

        public ActionResult PaymentConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                //bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                //if (checkSignature)
                //{
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công

                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                //}
                //else
                //{
                    //ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                //}
            }

            return View();
        }
        //Payment-End

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
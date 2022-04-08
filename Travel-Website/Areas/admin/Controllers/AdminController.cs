﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel_Website.Models;

namespace Travel_Website.Areas.admin.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult dashBoard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string passWord)
        {
            Model1 context = new Model1();
            string username = userName;
            string password = passWord;

            TaiKhoanAdmin ad = context.TaiKhoanAdmins.SingleOrDefault(p => p.TenDangNhap == username && p.MatKhau == password);
            if (ad != null)
            {
                Session["adAccount"] = ad;
                return RedirectToAction("Index", "Tour", new { area = "" });
            }
            else
            {
                return View();
            }
            //return RedirectToAction("Login","Admin");
        }

        public ActionResult Logout()
        {
            Session["adAccount"] = null;
            return RedirectToAction("Login", "Admin"); /*RedirectToAction("Index", "Home", new { area = "" });*/

        }
    }
}
//using DataAccessLayer.Concrete;
//using EntityLayer.Concrete;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Xml.Schema;
//using System.Web.Security;
//using BusinessLayer.Abstract;

//namespace MvcProjeKampi.Controllers
//{
//    public class LoginController : Controller
//    {
//        [HttpGet]
//        public ActionResult Index()
//        {
//            return View();
//        }
//        [HttpPost]
//        public ActionResult Index(Admin p)
//        {
//            Context c = new Context();
//            var adminuserinfo = c.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);

//            if (adminuserinfo != null)
//            {
//                FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
//                Session["AdminUserName"] = adminuserinfo.AdminUserName;
//                return RedirectToAction("Index", "AdminCategory");
//            }
//            else
//            {
//                // Eğer kullanıcı adı veya şifre yanlışsa bir hata mesajı gönder
//                TempData["LoginError"] = "Kullanıcı adı veya şifre hatalı!";
//                return RedirectToAction("Index");
//            }
//        }

//    }


//}


using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.Concrete;

namespace MvcProjeKampi.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            //HashPasswords();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin p)
        {
            using (var context = new Context())
            {
                // Hashlenmiş şifre ile veritabanındaki şifreyi karşılaştır
                var adminuserinfo = context.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName);

                // Kullanıcı adı mevcut mu kontrol et
                if (adminuserinfo != null /*/*&& PasswordHasher.VerifyPassword(p.AdminPassword, adminuserinfo.AdminPassword)*/)
                {
                    FormsAuthentication.SetAuthCookie(adminuserinfo.AdminUserName, false);
                    Session["AdminUserName"] = adminuserinfo.AdminUserName;
                    return RedirectToAction("Index", "AdminCategory");
                }
                else
                {
                    TempData["LoginError"] = "Kullanıcı adı veya şifre hatalı!";
                    return RedirectToAction("Index");
                }
            }
        }
       

        //public ActionResult HashPasswords()
        //{
        //    var adminManager = new AdminManager();
        //    adminManager.UpdateAdminPasswords(); // Mevcut şifreleri hash'le
        //    TempData["Message"] = "Tüm şifreler başarıyla hash'lenmiştir!";
        //    return RedirectToAction("Index"); // İlgili sayfaya yönlendir
        //}
    }
}


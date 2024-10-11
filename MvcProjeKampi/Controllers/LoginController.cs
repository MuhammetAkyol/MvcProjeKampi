using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.Concrete;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.WebControls;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        WriterLoginManager wm = new WriterLoginManager(new EfWriterDal());
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
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            //using (var context = new Context())
            //{
            //    // Hashlenmiş şifre ile veritabanındaki şifreyi karşılaştır
            //    var writeruserinfo = context.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail);
            var writeruserinfo = wm.GetWriter(p.WriterMail, p.WriterPassword);
                // Kullanıcı adı mevcut mu kontrol et
                if (writeruserinfo != null /*/*&& PasswordHasher.VerifyPassword(p.AdminPassword, adminuserinfo.AdminPassword)*/)
                {
                    FormsAuthentication.SetAuthCookie(writeruserinfo.WriterMail, false);
                    Session["WriterMail"] = writeruserinfo.WriterMail;
                    return RedirectToAction("MyContent", "WriterPanelContent");
                }
                else
                {
                    TempData["LoginError"] = "Kullanıcı adı veya şifre hatalı!";
                    return RedirectToAction("WriterLogin");
                }
            //}


          
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}


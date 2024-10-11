using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        // GET: WriterPanelMessage
        public ActionResult Inbox()//gelen mesajlar
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        }
        public ActionResult Sendbox()//gönderilen mesajlar
        {
            string p = (string)Session["WriterMail"];
            var messagelist = mm.GetListSendInbox(p);
            return View(messagelist);

        }
        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }
        public ActionResult GetInBoxMessageDetails(int id)//gelen mesajlar sayfası içeriği
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        public ActionResult GetSendBoxMessageDetails(int id)//gönderilen mesajlar sayfası içeriği
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message p, string action)//yeni mesaj sayfası
        {
            string sender = (string)Session["WriterMail"];
            ValidationResult results = messagevalidator.Validate(p);
            if (results.IsValid)
            {
                // Butona göre MessageStatus ayarlıyoruz
                if (action == "save")
                {
                    p.messagestatus = true;  // Normal kaydet, status true
                }
                else if (action == "draft")
                {
                    p.messagestatus = false;  // Taslak olarak kaydet, status false
                }

                // Gönderici e-posta, tarih bilgisi ve mesaj ekleme işlemi
                //p.SenderMail = "admin@gmail.com";  // Sabit gönderici
                p.SenderMail = sender;

                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());  // Şu anki tarih ve saat

                mm.MessageAdd(p);  // Mesajı veritabanına ekle

                return RedirectToAction("SendBox");  // Başarılıysa Gelen Kutusuna yönlendir
            }
            else
            {
                // Eğer validasyon hatası varsa, hataları göster
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

    }
}
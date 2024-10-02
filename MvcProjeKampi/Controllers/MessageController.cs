using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        [Authorize]
        public ActionResult Inbox()//gelen mesajlar
        {
            var messagelist = mm.GetListInbox();
            return View(messagelist);
        }
        public ActionResult Sendbox()//gönderilen mesajlar
        {
            var messagelist = mm.GetListSendInbox();
            return View(messagelist);

        }
        public ActionResult Draft()//taslaklar sayfası
        {
            var messagelist = mm.GetListDraft();
            return View(messagelist);
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
        public ActionResult GetDraftMessageDetails(int id)//taslaklar sayfası içeriği
        {
            var values = mm.GetByID(id);
            return View(values);
        }
        public ActionResult DeleteMessage()//Silinenler sayfası
        {
            var messagelist = mm.GetListDelete();
            return View(messagelist);
        }
        public ActionResult GetDeleteMessageDetails(int id)//taslaklar sayfası içeriği
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult ToggleMessageStatus(int id)//burası taslak sayfasındaki mesajı göndeme işlemmini, eylemini yapan yer
        {
            var message = mm.GetByID(id); // id ile mesajı bul
            if (message != null)
            {
                // MessageStatus'ü tersine çevir
                message.messagestatus = !message.messagestatus;

                // MessageUpdate metodunu kullanarak veritabanında güncelle
                mm.MessageUpdate(message);
            }

            // Aynı sayfaya geri yönlendir (mesaj detayları)
            return RedirectToAction("GetDeleteMessageDetails", new { id = id });
        }
        [HttpPost]
        public ActionResult ToggleMessageRemove(int id)//burası taslak sayfasındaki mesajı göndeme işlemmini, eylemini yapan yer
        {
            var message = mm.GetByID(id); // id ile mesajı bul
            if (message != null)
            {
                // MessageRemove'yi tersine çevir
                message.messageremove = !message.messageremove;

                // MessageUpdate metodunu kullanarak veritabanında güncelle
                mm.MessageUpdate(message);
            }

            // Aynı sayfaya geri yönlendir (mesaj detayları)
            return RedirectToAction("GetDraftMessageDetails", new { id = id });

        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult NewMessage(Message p, string action)//yeni mesaj sayfası
        {
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
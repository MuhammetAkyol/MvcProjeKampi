//using BusinessLayer.Concrete;
//using DataAccessLayer.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using DataAccessLayer.Concrete;
//using EntityLayer.Concrete;
//using BusinessLayer.Abstract;

//namespace MvcProjeKampi.Controllers
//{
//    public class HeadingController : Controller
//    {
//         HeadingManager hm = new HeadingManager(new EfHeadingDal());
//         CategoryManager cm = new CategoryManager(new EfCategoryDal());
//         WriterManager wm = new WriterManager(new EfWriterDal());

//        // GET: Heading
//        public ActionResult Index()
//        {
//            var headingvalues = hm.GetList();
//            return View(headingvalues);
//        }
//        [HttpGet]
//        public ActionResult AddHeading()
//        {
//            List<SelectListItem> valuecategory = (from x in cm.GetList()
//                                                  select new SelectListItem
//                                                  {
//                                                      Text = x.CategoryName,
//                                                      Value = x.CategoryID.ToString()
//                                                  }).ToList();
//            List<SelectListItem> valuewriter = (from x in wm.GetList()
//                                                  select new SelectListItem
//                                                  {
//                                                      Text = x.WriterName + " " +
//                                                      x.WriterSurName,
//                                                      Value = x.WriterID.ToString()
//                                                  }).ToList();

//            ViewBag.vlc = valuecategory;
//            ViewBag.wlc = valuewriter;
//            return View();
//        }
//        [HttpPost]
//        public ActionResult AddHeading(Heading p)
//        {
//            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString()); 
//            hm.HeadingAdd(p);
//            return RedirectToAction("Index");
//        }
//        [HttpGet]
//        public ActionResult EditHeading(int id)
//        {

//            List<SelectListItem> valuecategory = (from x in cm.GetList()
//                                                  select new SelectListItem
//                                                  {
//                                                      Text = x.CategoryName,
//                                                      Value = x.CategoryID.ToString()
//                                                  }).ToList();
//            ViewBag.vlc = valuecategory;
//            var headingvalue=hm.GetByID(id);
//            return View(headingvalue);

//        }
//        [HttpPost]
//        public ActionResult EditHeading(Heading p)
//        {
//            hm.HeadingUpdate(p);
//            return RedirectToAction("Index");
//        }
//        public ActionResult DeleteHeading(int id)
//        {   
//            var headingvalue = hm.GetByID(id);
//            hm.HeadingDelete(headingvalue);
//            return RedirectToAction("Index");
//        }
//        public ActionResult EditStatus(int id)
//        {
//            var headingvalue = hm.GetByID(id); // ID ile başlığı al           
//            if (headingvalue != null)
//            {
//                headingvalue.HeadingStatus = !headingvalue.HeadingStatus; // Durumunu tersine çevir
//                hm.HeadingUpdate(headingvalue); // Güncellenmiş durumu kaydet
//            }            
//            return RedirectToAction("Index"); // Geri dön
//        }
//    }
//}
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());

        // GET: Heading
        public ActionResult Index()
        {
            var headingvalues = hm.GetList();
            return View(headingvalues);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            List<SelectListItem> valuewriter = (from x in wm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.WriterName + " " +
                                                    x.WriterSurName,
                                                    Value = x.WriterID.ToString()
                                                }).ToList();

            ViewBag.vlc = valuecategory;
            ViewBag.wlc = valuewriter;
            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            hm.HeadingAdd(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            var headingvalue = hm.GetByID(id);
            return View(headingvalue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = hm.GetByID(id);
            hm.HeadingDelete(headingvalue);
            return RedirectToAction("Index");
        }

        public ActionResult EditStatus(int id)
        {
            var headingvalue = hm.GetByID(id); // ID ile başlığı al
            if (headingvalue != null)
            {
                headingvalue.HeadingStatus = !headingvalue.HeadingStatus; // Durumunu tersine çevir
                hm.HeadingUpdate(headingvalue); // Güncellenmiş durumu kaydet

                // Durumun güncellenip güncellenmediğini konsola yazdırarak kontrol edin
                System.Diagnostics.Debug.WriteLine("Yeni Durum: " + headingvalue.HeadingStatus);
            }
            return RedirectToAction("Index"); // Index sayfasına geri dön
        }


    }
}

//using BusinessLayer.Concrete;
//using DataAccessLayer.EntityFramework;
//using EntityLayer.Concrete;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace MvcProjeKampi.Controllers
//{
//    public class AboutController : Controller
//    {
//        AboutManager abm=new AboutManager(new EfAboutDal());
//        // GET: About
//        public ActionResult Index()
//        {
//            var aboutvalues = abm.GetList();
//            return View(aboutvalues);
//        }
//        [HttpGet]
//        public ActionResult AddAbout()
//        {
//            return View();  
//        }
//        [HttpPost]
//        public ActionResult AddAbout(About p)
//        {
//            abm.AboutAdd(p);
//            return RedirectToAction("Index");
//        }
//        public PartialViewResult AboutPartial()
//        {
//            return PartialView();
//        }
//    }
//}


using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class AboutController : Controller
    {
        AboutManager abm = new AboutManager(new EfAboutDal());

        // GET: About
        public ActionResult Index()
        {
            var aboutvalues = abm.GetList();
            return View(aboutvalues);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            abm.AboutAdd(p);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }

        // Aktif Yap
        public ActionResult Activate(int id)
        {
            var about = abm.GetByID(id);
            if (about != null)
            {
                about.AboutStatus = true; // Aktif yapılıyor
                abm.AboutUpdate(about);
            }
            return RedirectToAction("Index");
        }

        // Pasif Yap
        public ActionResult Deactivate(int id)
        {
            var about = abm.GetByID(id);
            if (about != null)
            {
                about.AboutStatus = false; // Pasif yapılıyor
                abm.AboutUpdate(about);
            }
            return RedirectToAction("Index");
        }
    }
}

using DataAccessLayer.Concrete;
using MvcProjeKampi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using DataAccessLayer.Concrete; // DbContext ve Entity sınıfları için gerekli namespace
    using MvcProjeKampi.Models; // ViewModel ve diğer sınıflar için gerekli namespace

    public class IstatistiksController : Controller
    {
        private readonly Context _context;

        public IstatistiksController()
        {
            _context = new Context();
        }

        private int GetHeadingCountByCategoryName(string categoryName)//kategori saysını ve Hangi Başlık tanımlandıysa o
                                                                      //başlıkta olan sayı değerni getiriyor
                                                                      //aşağıdada ActionResult Index() çağırıyoruz
        {
            var categoryId = _context.Categories.FirstOrDefault(c => c.CategoryName == categoryName)?.CategoryID;

            return categoryId == null ? 0 : _context.Headings.Count(h => h.CategoryID == categoryId);
        }

        private int GetAuthorsWithA()//yazıcı adından a harfi bulunanların sayısı
        {
            return _context.Writers.Count(w => w.WriterName.Contains("a"));
        }

        private string GetCategoryWithMostHeadings()//En fazla başlığa sahip kategori adını'getiren kısım
        {
            var categoryWithMostHeadings = _context.Categories
                .Select(c => new
                {
                    CategoryName = c.CategoryName,
                    HeadingCount = _context.Headings.Count(h => h.CategoryID == c.CategoryID)
                })
                .OrderByDescending(c => c.HeadingCount)
                .FirstOrDefault();

            return categoryWithMostHeadings?.CategoryName ?? "No categories found";
        }

        private int GetCategoryDifferenceByStatus()//Kategori tablosunda durumu true olan
                                                   //kategoriler ile false olan kategoriler arasındaki sayısal fark bulma
        {
            int trueCount = _context.Categories.Count(c => c.Durum == true);
            int falseCount = _context.Categories.Count(c => c.Durum == false);

            return trueCount = falseCount;
        }
        public ActionResult Index()
        {
            int categoryCount = _context.Categories.Count();//toplam kategori sayısı
            int headingCount = GetHeadingCountByCategoryName("yazılım");//yukarıda açtığımız metodların çağrımı
            int authorsWithA = GetAuthorsWithA();//yukarıda açtığımız metodların çağrımı
            string categoryWithMostHeadings = GetCategoryWithMostHeadings();
            int categoryDifference = GetCategoryDifferenceByStatus();


            var viewModel = new CategoryHeadingUserViewModel
            {
                Categories = _context.Categories.ToList(),
              Headings = _context.Headings.ToList(),
                Writers = _context.Writers.ToList(),
                Contacts = _context.Contacts.ToList(),
                Contents = _context.Contents.ToList(),
                Abouts = _context.Abouts.ToList(),
                CategoryCount = categoryCount,//yukarıda açtığımız metodların çağrımı
                HeadingCount = headingCount,//yukarıda açtığımız metodların çağrımı
                AuthorsWithA = authorsWithA,//yukarıda açtığımız metodların çağrımı
                CategoryWithMostHeadings = categoryWithMostHeadings, // Eklenen özellik
                CategoryDifference = categoryDifference // Eklenen özellik

            };

            return View(viewModel);
        }
    }

}
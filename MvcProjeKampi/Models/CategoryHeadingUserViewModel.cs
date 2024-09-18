using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjeKampi.Models
{
    public class CategoryHeadingUserViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Heading> Headings { get; set; }
        public IEnumerable<About> Abouts { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
        public IEnumerable<Content> Contents { get; set; }
        public IEnumerable<Writer> Writers { get; set; }
        public int CategoryCount { get; set; }  // Kategori sayısı
        public int HeadingCount { get; set; }   // Başlık sayısı
        public int AuthorsWithA { get; set; }  // İçerisinde harf bulan özellik
        public string CategoryWithMostHeadings { get; set; } // İçerisinde harf bulan özellik
        public int CategoryDifference { get; set; } // Eklenen özellik


    }
}
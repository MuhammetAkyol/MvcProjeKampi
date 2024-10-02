using System.Collections.Generic;
using MvcProjeKampi.Content;

namespace MvcProjeKampi.Models
{
    public class CategoryHeadingUserViewModelBase
    {
        public IEnumerable<EntityLayer.Concrete.Content> Contents { get; set; }
    }
}
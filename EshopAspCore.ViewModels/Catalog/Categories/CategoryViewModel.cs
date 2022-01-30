using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Catalog.Categories
{
    public class CategoryViewModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int? ParentId { get; set; }
        public string LanguageId { get; set; }
    }
}

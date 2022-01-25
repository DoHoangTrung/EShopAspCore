using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public decimal OriginalPrice { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public bool? IsFeatured { get; set; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public string Language { get; set; }
        public int? CategoryId { get; set; }
        public List<SelectedItem> AllCategory { get; set; } = new List<SelectedItem>();
        public string CategoriesString { get; set; }
        public string ThumbnailImage { get; set; }
    }
}

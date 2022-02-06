using EshopAspCore.ViewModels.Catalog.ProductImages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Catalog.Products
{
    public class ProductDetailViewModel
    {
        public int Id { set; get; }
        public decimal Price { set; get; }
        public int Stock { set; get; }
        public int ViewCount { set; get; }
        public DateTime DateCreated { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string CategoriesString { get; set; }
        public List<ProductImageViewModel> Images { get; set; }
        public string ThumbnailImage { get; set; }
    }
}

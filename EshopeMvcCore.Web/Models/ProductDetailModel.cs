using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Models
{
    public class ProductDetailModel
    {
        public ProductViewModel Product { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}

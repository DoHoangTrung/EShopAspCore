using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Models
{
    public class ProductSearchViewModel
    {
        public PageResult<ProductViewModel> ProductPages { get; set; }
        public string SelectionSortOrder { get; set; }
        public int ProductCount
        {
            get
            {
                return ProductPages.Items.Count;
            }
        }
    }
}

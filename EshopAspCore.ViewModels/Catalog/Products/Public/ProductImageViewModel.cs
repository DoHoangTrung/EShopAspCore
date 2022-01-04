using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Catalog.Products.Public
{
    public class ProductImageViewModel
    {
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public bool isDefault { get; set; }
        public long FileSize { get; set; }

    }
}

using EshopAspCore.Utilities.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Models
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal TotalPrice { get; set; }

        public string GetTotalPriceVND
        {
            get
            {
                return TotalPrice.ToVNDString();
            }
        }
    }
}

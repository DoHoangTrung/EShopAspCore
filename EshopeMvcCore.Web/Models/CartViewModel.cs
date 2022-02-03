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

        public decimal TotalCartPrice { get; set; }

        public string GetTotalCartPriceVND
        {
            get
            {
                return TotalCartPrice.ToVNDString();
            }
        }
    }
}

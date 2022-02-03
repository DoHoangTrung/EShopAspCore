using EshopAspCore.Utilities.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Models
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { set; get; }
        public decimal TotalCost { get; set; }
        public string GetTotalCostVND { get
            {
                return TotalCost.ToVNDString();
            } }

        public string GetPriceVND { get
            {
                return Price.ToVNDString();
            } }
    }
}

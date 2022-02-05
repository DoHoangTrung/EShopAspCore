using EshopAspCore.Utilities.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Models
{
    public class CheckOutViewModel
    {
        public List<CartItemViewModel> cartItems { get; set; } = new List<CartItemViewModel>();
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
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

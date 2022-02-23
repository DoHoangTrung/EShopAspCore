using EshopAspCore.Utilities.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Models
{
    public class CheckOutViewModel
    {
        public List<CartItemViewModel> cartItems { get; set; } = new List<CartItemViewModel>();
        public Guid? UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
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

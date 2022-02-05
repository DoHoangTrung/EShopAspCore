using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Sales
{
    public class CheckOutRequest
    {
        public List<OrderItemViewModel> cartItems { get; set; } = new List<OrderItemViewModel>();
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

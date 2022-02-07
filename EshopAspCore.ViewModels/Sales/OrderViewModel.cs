using EshopAspCore.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Sales
{
    public class OrderViewModel
    {
        public int Id { set; get; }
        public DateTime OrderDate { set; get; }
        public Guid? UserId { set; get; }
        [DisplayName("Name")]
        public string ShipName { set; get; }
        [DisplayName("Address")]
        public string ShipAddress { set; get; }
        [DisplayName("Email")]
        public string ShipEmail { set; get; }
        [DisplayName("Phone")]
        public string ShipPhoneNumber { set; get; }
        public OrderStatus Status { set; get; }
        public decimal TotalBillCash { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}

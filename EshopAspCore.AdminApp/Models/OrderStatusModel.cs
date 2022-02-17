using EshopAspCore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Models
{
    public class OrderStatusModel
    {
        public OrderStatus Status { get; set; }
        public string classBoostrap { get; set; }
    }
}

using EshopAspCore.Data.Enum;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Models
{
    public class OrderPageViewModel
    {
        public PageResult<OrderViewModel> Orders { get; set; }
        public OrderStatus Status { get; set; }

        public List<OrderStatusModel> listState { get; set; }
    }
}

using EshopAspCore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.Sales
{
    public class OrderGetRequest
    {
        public OrderStatus? status { get; set; }
    }
}

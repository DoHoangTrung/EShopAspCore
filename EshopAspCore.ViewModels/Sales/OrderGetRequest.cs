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

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}

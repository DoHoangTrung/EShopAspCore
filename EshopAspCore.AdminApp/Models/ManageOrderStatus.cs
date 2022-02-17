using EshopAspCore.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Models
{
    public class ManageOrderStatus
    {
        public OrderStatus Status { get; set; }
        public ManageOrderStatus(OrderStatus status)
        {
            Status = status;
        }

        public List<OrderStatusModel> GetManageOrderState()
        {
            var model = new List<OrderStatusModel>();

            if (Status == OrderStatus.Success)
                return model;

            switch (Status)
            {
                case OrderStatus.InProgress:
                    model.Add(new OrderStatusModel()
                    {
                        Status = OrderStatus.Confirmed,
                        classBoostrap = "btn btn-sm btn-primary"
                    });
                    break;
                case OrderStatus.Confirmed:
                    model.Add(new OrderStatusModel()
                    {
                        Status = OrderStatus.Shipping,
                        classBoostrap = "btn btn-sm btn-info"
                    });
                    break;
                case OrderStatus.Shipping:
                    model.Add(new OrderStatusModel()
                    {
                        Status = OrderStatus.Success,
                        classBoostrap = "btn btn-sm btn-success"
                    });
                    break;
                case OrderStatus.Canceled:
                    model.Add(new OrderStatusModel()
                    {
                        Status = OrderStatus.InProgress,
                        classBoostrap = "btn btn-sm btn-dark"
                    });
                    break;
                default:
                    break;
            }


            if (Status != OrderStatus.Canceled)
            {
                model.Add(new OrderStatusModel()
                {
                    Status = OrderStatus.Canceled,
                    classBoostrap = "btn btn-sm btn-danger"
                });
            }

            return model;
        }
    }
}

using EshopAspCore.Data.EF;
using EshopAspCore.Data.Entity;
using EshopAspCore.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Sales
{
    public class OrderService : IOrderService
    {
        private readonly EshopDbContext _context;

        public OrderService(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckOutOrders(CheckOutRequest request)
        {
            try
            {
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    ShipAddress = request.Address,
                    ShipEmail = request.Email,
                    ShipName = request.Name,
                    ShipPhoneNumber = request.Phone,
                    Status = Data.Enum.OrderStatus.InProgress,
                    UserId = request.UserId,
                };

                order.OrderDetails = new List<OrderDetail>();
                foreach (var item in request.cartItems)
                {
                    order.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.Id,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }

                await _context.Orders.AddAsync(order);

                int recordsAffected = _context.SaveChanges();
                return recordsAffected > 0;
            }
            catch (Exception e)
            {
                string msg = e.Message;

                throw;
            }
        }
    }
}

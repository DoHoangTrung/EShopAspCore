using EshopAspCore.Data.EF;
using EshopAspCore.Data.Entity;
using EshopAspCore.Data.Enum;
using EshopAspCore.ViewModels.Sales;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<OrderViewModel>> GetAll(OrderGetRequest request)
        {
            var query = _context.Orders.Select(x=>x);

            if(request.status != null)
            {
                query = query.Where(x => x.Status == request.status);
            }

            return await query.Select(x=> new OrderViewModel()
            {
                Id = x.Id,
                OrderDate = x.OrderDate,
                ShipAddress = x.ShipAddress,
                ShipEmail = x.ShipEmail,
                ShipName = x.ShipName,
                ShipPhoneNumber = x.ShipPhoneNumber,
                Status = x.Status
            }).ToListAsync();
        }

        public async Task<OrderViewModel> GetById(int id, string languageId)
        {
            var query = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (query == null) return null;

            var order = new OrderViewModel()
            {
                Id = query.Id,
                OrderDate = query.OrderDate,
                ShipAddress = query.ShipAddress,
                ShipEmail = query.ShipEmail,
                ShipName = query.ShipName,
                ShipPhoneNumber = query.ShipPhoneNumber
            };

            //get list order item
            var orderitems =await (from od in _context.OrderDetails
                            join pt in _context.ProductTranslations on od.ProductId equals pt.ProductId
                            where od.OrderId == order.Id && pt.LanguageId == languageId
                            select new OrderItemViewModel()
                            {
                                Name = pt.Name,
                                Price = od.Price,
                                ProductId = od.ProductId,
                                Quantity = od.Quantity
                            }).ToListAsync();

            order.OrderItems = orderitems;
            order.TotalBillCash = orderitems.Sum(x => x.TotalPrice);

            return order;
        }

        public async Task<int> UpdateStatus(int id, OrderStatus newStatus)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x=>x.Id == id);
            if (order == null) return 0;

            if (newStatus == order.Status) return 0;

            order.Status = newStatus;
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync();
        }
    }
}

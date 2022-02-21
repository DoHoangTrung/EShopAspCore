using EshopAspCore.Data.Enum;
using EshopAspCore.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Sales
{
    public interface IOrderService
    {
        Task<bool> CheckOutOrders(CheckOutRequest request);
        Task<List<OrderViewModel>> GetAll(OrderGetRequest request);
        Task<OrderViewModel> GetById(int id, string languageId);
        Task<int> UpdateStatus(int id, OrderStatus newStatus);
        Task<bool> Delete(int id);

    }
}

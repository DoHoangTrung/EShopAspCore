using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public interface IOrderApiClient
    {
        Task<bool> CheckOut(CheckOutRequest request);

        Task<List<OrderViewModel>> GetAll(OrderGetRequest request);
        Task<OrderViewModel> GetById (int id, string languageId);

        Task<bool> SendEmail(MailContent mailContent);
    }
}

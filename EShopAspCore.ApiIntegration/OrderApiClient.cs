using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public class OrderApiClient : BaseApiClient, IOrderApiClient
    {
        public OrderApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<OrderViewModel> GetById(int id, string languageId)
        {
            string url = $"api/orders/{id}/{languageId}";
            var apiResult = await GetAsync<OrderViewModel>(url);
            return apiResult;
        }

        public async Task<bool> CheckOut(CheckOutRequest request)
        {
            string url = $"api/orders";
            var apiResult = await PostAsync<bool, CheckOutRequest>(url, request);
            return apiResult;
        }

        public async Task<List<OrderViewModel>> GetAll(OrderGetRequest request)
        {
            string url = "/api/orders";
            if (request.status != null)
            {
                url += $"?status={request.status}";
            }
            var apiResult = await GetAsync<List<OrderViewModel>>(url);
            return apiResult;
        }

        public async Task<bool> SendEmail(MailContent mailContent)
        {
            string url = "api/emails";
            var apiResult = await PostAsync<bool, MailContent>(url, mailContent);
            return apiResult;
        }
    }
}

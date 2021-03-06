using EshopAspCore.Data.Enum;
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

        public async Task<int> CheckOut(CheckOutRequest request)
        {
            string url = $"api/orders";
            var apiResult = await PostAsync<int, CheckOutRequest>(url, request);
            return apiResult;
        }

        public async Task<PageResult<OrderViewModel>> GetAll(OrderGetRequest request)
        {
            string url = $"/api/orders?pageIndex={request.PageIndex}&pageSize={request.PageSize}";
            if (request.status != null)
            {
                url += $"&status={request.status}";
            }
            var apiResult = await GetAsync<PageResult<OrderViewModel>>(url);
            return apiResult;
        }

        public async Task<bool> SendEmail(MailContent mailContent)
        {
            string url = "api/emails";
            var apiResult = await PostAsync<bool, MailContent>(url, mailContent);
            return apiResult;
        }

        public async Task<int> UpdateStatus(int id, OrderStatus status)
        {
            string url = $"api/orders/{id}/status";
            var apiRestult = await PutAsync<int, OrderStatus>(url, status);
            return apiRestult;
        }

        public async Task<bool> Delete(int id) {
            string url = $"api/orders/{id}";
            var resultApi = await DeleteAsync<bool>(url);
            return resultApi;
        }

    }
}

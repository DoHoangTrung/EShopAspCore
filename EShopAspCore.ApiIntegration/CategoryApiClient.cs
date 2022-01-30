using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<ApiResult<List<CategoryViewModel>>> GetAll(string languageId)
        {
            string url = $"/api/categories?languageId={languageId}";
            var apiResult = await GetAsync<ApiResult<List<CategoryViewModel>>>(url);
            return apiResult;
        }

        public async Task<ApiResult<List<CategoryViewModel>>> GetByProductId(int productId, string languageId)
        {
            string url = $"/api/categories/product-id/{productId}/{languageId}";
            var apiResult = await GetAsync<ApiResult<List<CategoryViewModel>>>(url);
            return apiResult;
        }

        public async Task<bool> Update( int id, List<SelectedItem> items)
        {
            string url = $"api/categories/{id}";
            var result = await PutAsync<bool, List<SelectedItem>>(url, items);
            return result;
        }

        public async Task<ApiResult<CategoryViewModel>> GetById(int id, string languageId)
        {
            string url = $"api/categories/{id}/{languageId}";
            var apiResult = await GetAsync<ApiResult<CategoryViewModel>>(url);
            return apiResult;
        }

    }
}

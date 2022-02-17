using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Catalog.Products.Manage;
using EshopAspCore.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<bool> Create( ProductCreateRequest request)
        {
            HttpClient client = GetBearerHeaderClient();

            var requestContent = new MultipartFormDataContent();

            //iformfile to binary
            if(request.ThumbNailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbNailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbNailImage.OpenReadStream().Length);
                }

                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbNailImage", request.ThumbNailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(request.Stock.ToString()), "stock");
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");
            requestContent.Add(new StringContent(request.Details.ToString()), "details");
            requestContent.Add(new StringContent(request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(request.LanguageId.ToString()), "languageId");

            var apiResponse = await client.PostAsync("/api/products", requestContent);
            return apiResponse.IsSuccessStatusCode;
        }

        public async Task<ApiResult<PageResult<ProductViewModel>>> GetAll(GetManageProductPagingRequest request)
        {
            string url = $"api/products?pageIndex={request.PageIndex}&pageSize={request.PageSize}" +
                $"&languageId={request.LanguageId}";

            if (request.CategoryId.HasValue)
            {
                url += $"&categoryId={request.CategoryId}";
            }
            if (!string.IsNullOrEmpty(request.SelectionSortOrder))
            {
                url += $"&SelectionSortOrder={request.SelectionSortOrder}";
            }

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                url += $"&keyWord={request.Keyword}";
            }

            var apiResponse = await GetAsync<ApiResult<PageResult<ProductViewModel>>>(url);
            return apiResponse;
        }

        public async Task<ApiResult<ProductViewModel>> GetById(int id, string languageId)
        {
            string urlApi = $"/api/products/{id}/{languageId}";
            var apiResult = await GetAsync<ApiResult<ProductViewModel>>(urlApi);
            return apiResult;
        }
        public async Task<ApiResult<List<ProductViewModel>>> GetFeaturedProduct(string languageId, int take)
        {
            string url = $"/api/products/featured/{take}/{languageId}";
            var apiResult = await GetAsync<ApiResult<List<ProductViewModel>>>(url);
            return apiResult;
        }

        public async Task<ApiResult<List<ProductViewModel>>> GetLatestProduct(string languageId, int take)
        {
            string url = $"/api/products/latest/{take}/{languageId}";
            var apiResult = await GetAsync<ApiResult<List<ProductViewModel>>>(url);
            return apiResult;
        }

        public async Task<bool> Update(int id, ProductUpdateRequest request)
        {
            HttpClient client = GetBearerHeaderClient();

            var requestContent = new MultipartFormDataContent();

            //iformfile to binary
            if (request.ThumbNailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbNailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbNailImage.OpenReadStream().Length);
                }

                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbNailImage", request.ThumbNailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Id.ToString()), "id");
            requestContent.Add(new StringContent(request.Name), "name");
            requestContent.Add(new StringContent(request.Description), "Description");
            requestContent.Add(new StringContent(request.Details), "Details");
            requestContent.Add(new StringContent(request.SeoAlias), "SeoAlias");
            requestContent.Add(new StringContent(request.SeoDescription) , "SeoDescription");
            requestContent.Add(new StringContent(request.SeoTitle), "SeoTitle");
            requestContent.Add(new StringContent(request.LanguageId), "LanguageId");

            var response = await client.PutAsync($"/api/products/{id}", requestContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Delete(int id)
        {
            var isSuccess = await DeleteAsync<bool>($"/api/products/{id}");
            return isSuccess;
        }
    }
}

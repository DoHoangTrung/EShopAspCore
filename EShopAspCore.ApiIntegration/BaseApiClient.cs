using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public class BaseApiClient
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IConfiguration _configuration;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var client = GetBearerHeaderClient();

            var apiResponse = await client.GetAsync(url);

            var content = await apiResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        protected async Task<TResponse> PostAsync<TResponse, DataType>(string url, DataType newObject)
        {
            var json = JsonConvert.SerializeObject(newObject);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = GetBearerHeaderClient();

            var response = await client.PostAsync(url, httpContent);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        protected async Task<TResponse> PutAsync<TResponse, DataType>(string url, DataType updateOject)
        {
            //use json convert data
            var client = GetBearerHeaderClient();

            var json = JsonConvert.SerializeObject(updateOject);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(url, httpContent);
            var responseData = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(responseData);
        }

        protected async Task<TResponse> DeleteAsync<TResponse>(string url)
        {
            var client = GetBearerHeaderClient();

            var response = await client.DeleteAsync(url);

            var content = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        protected HttpClient GetBearerHeaderClient()
        {
            var client = _httpClientFactory.CreateClient("HttpClientWithSSLUntrusted");
            var bearToken = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseApiUrlString]);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);

            return client;
        }
    }
}

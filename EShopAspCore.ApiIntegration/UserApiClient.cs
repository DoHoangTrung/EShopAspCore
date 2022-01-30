using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Roles;
using EshopAspCore.ViewModels.System.Users;
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
    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
            : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseApiUrlString]);
            var response = await client.PostAsync("/api/users/authenticate", data);
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(content);
            }
            else
            {
                return JsonConvert.DeserializeObject<ApiErrorResult<string>>(content);
            }
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            string urlApi = $"/api/users/{id}";
            var apiResult = await GetAsync<ApiResult<UserViewModel>>(urlApi);
            return apiResult;
        }

        public async Task<ApiResult<PageResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseApiUrlString]);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.BearerToken);

            var response = await client.GetAsync($"/api/users/paging?PageIndex={request.PageIndex}" +
                $"&PageSize={request.PageSize}&Keywords={request.Keywords}");

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<PageResult<UserViewModel>>>(content);
            }
            else
            {
                return JsonConvert.DeserializeObject<ApiErrorResult<PageResult<UserViewModel>>>(content);
            }
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var apiResponse = await PostAsync<ApiResult<bool>, RegisterRequest>("/api/users", request);

            return apiResponse;
        }

        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {
            string urlApi = $"/api/users/{id}";
            var apiResponse = await PutAsync<ApiResult<bool>, UserUpdateRequest>(urlApi,request);

            return apiResponse;
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            string urlApi = $"/api/users/{id}";
            var apiResponse = await DeleteAsync<ApiResult<bool>>(urlApi);

            return apiResponse;
        }

        public async Task<ApiResult<bool>> RoleAssign (Guid id, RoleAssignRequest request)
        {
            var bearToken = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseApiUrlString]);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/users/{id}/roles", httpContent);
            var responseData = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(responseData);
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(responseData);
        }
    }
}

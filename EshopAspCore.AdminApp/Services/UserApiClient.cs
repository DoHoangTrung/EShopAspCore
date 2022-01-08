using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Users;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseApiUrlString]);
            var response = await client.PostAsync("/api/users/authenticate", data);
            var token = await response.Content.ReadAsStringAsync();

            return token;
        }

        public async Task<PageResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseApiUrlString]);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", request.BearerToken);

            var response = await client.GetAsync($"/api/users/paging?PageIndex={request.PageIndex}" +
                $"&PageSize={request.PageSize}&Keywords={request.Keywords}");

            var content = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<PageResult<UserViewModel>>(content);

            return users;
        }
    }
}

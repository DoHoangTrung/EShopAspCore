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
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Services
{
    public class RoleApiClient : BaseApiClient, IRoleApiClient
    {
        public RoleApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            return await GetAsync<ApiResult<List<RoleViewModel>>>("/api/roles");
        }
    }
}

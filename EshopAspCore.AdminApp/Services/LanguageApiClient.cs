using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Languages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Services
{
    public class LanguageApiClient : BaseApiClient,ILanguageApiClient
    {
        public LanguageApiClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor) 
            : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
        {
            var response = await GetAsync<ApiResult<List<LanguageViewModel>>>("/api/languages");

            return response;
        }
    }
}

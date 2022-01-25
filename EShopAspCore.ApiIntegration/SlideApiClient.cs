using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.Utilities.Slides;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public class SlideApiClient : BaseApiClient, ISlideApiClient
    {
        public SlideApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<ApiResult<List<SlideViewModel>>> GetAll()
        {
            string url = $"/api/slides";
            var apiResult = await GetAsync<ApiResult<List<SlideViewModel>>>(url);
            return apiResult;
        }

        

    }
}

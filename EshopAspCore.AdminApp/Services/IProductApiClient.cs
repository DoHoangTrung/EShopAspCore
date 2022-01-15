using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Services
{
    public interface IProductApiClient
    {
        public Task<ApiResult<PageResult<ProductViewModel>>> GetAll(string url);
    }
}

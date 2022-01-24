using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Catalog.Products.Manage;
using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public interface IProductApiClient
    {
        public Task<ApiResult<PageResult<ProductViewModel>>> GetAll(string url);
        public Task<bool> Create (ProductCreateRequest request);

        public Task<ApiResult<ProductViewModel>> GetById(int id, string languageId);
    }
}

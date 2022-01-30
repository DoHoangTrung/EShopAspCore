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
        public Task<ApiResult<PageResult<ProductViewModel>>> GetAll(GetManageProductPagingRequest request);
        public Task<bool> Create (ProductCreateRequest request);

        public Task<ApiResult<ProductViewModel>> GetById(int id, string languageId);
        public Task<bool> Update (int id, ProductUpdateRequest request);
        public Task<bool> Delete(int id);
        Task<ApiResult<List<ProductViewModel>>> GetFeaturedProduct(string languageId, int take);
        Task<ApiResult<List<ProductViewModel>>> GetLatestProduct(string languageId, int take);

    }
}

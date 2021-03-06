using EshopAspCore.Data.Entity;
using EshopAspCore.ViewModels.Catalog.ProductImages;
using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Catalog.Products.Manage;
using EshopAspCore.ViewModels.Catalog.Products.Public;
using EshopAspCore.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Catalog.Products
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task AddViewCount(int productId);

        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task<int> Delete(int productId);

        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);
        Task<ApiResult<ProductViewModel>> GetById(int productId, string languageId);
        Task<int> AddImage(int productId, ProductImageCreateRequest request);
        Task<int> RemoveImage(int imageId);
        Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request);

        Task<List<ProductImageViewModel>> GetListImages(int productId);

        Task<ProductImageViewModel> GetProductImageById(int imageId);
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request);
        Task<List<ProductViewModel>> GetFeaturedProduct(string languageId, int take);
        Task<List<ProductViewModel>> GetLatestProduct(string languageId, int take);
        Task<int> GetStockById(int id);
    }
}

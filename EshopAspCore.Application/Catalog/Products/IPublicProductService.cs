using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Catalog.Products.Public;
using EshopAspCore.ViewModels.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>> GetAllByCategoryId(string languageId,GetPublicProductPagingRequest request);

        Task<List<ProductViewModel>> GetAll();
    }
}

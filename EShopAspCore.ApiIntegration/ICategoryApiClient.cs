using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public interface ICategoryApiClient
    {
        Task<ApiResult<List<CategoryViewModel>>> GetAll (string languageId);

        Task<ApiResult<List<CategoryViewModel>>> GetByProductId(int productId, string languageId);

    }
}

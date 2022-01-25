using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.ApiIntegration
{
    public interface ISlideApiClient
    {
        Task<ApiResult<List<SlideViewModel>>> GetAll ();
    }
}

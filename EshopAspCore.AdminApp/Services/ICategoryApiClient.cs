﻿using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Services
{
    public interface ICategoryApiClient
    {
        Task<ApiResult<List<CategoryViewModel>>> GetAll (string languageId);
    }
}

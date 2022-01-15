using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Services
{
    public interface ILanguageApiClient
    {
        public Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}

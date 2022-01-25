using EshopAspCore.ApiIntegration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Controllers.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public SideBarViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var culture = CultureInfo.CurrentCulture.Name;

            var categoriesApiResult = await _categoryApiClient.GetAll(culture);
            if (categoriesApiResult.IsSuccessed)
            {
                var categories = categoriesApiResult.ResultObject;
                return View(categories);
            }

            return View();
        }
    }
}

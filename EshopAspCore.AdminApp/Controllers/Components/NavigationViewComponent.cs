using EshopAspCore.AdminApp.Models;
using EshopAspCore.AdminApp.Services;
using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.System.Languages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Controllers.Components
{
    public class NavigationViewComponent: ViewComponent
    {
        private readonly ILanguageApiClient _languageApiClient;

        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            _languageApiClient = languageApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var apiResult = await _languageApiClient.GetAll();
            if (apiResult.IsSuccessed == false)
            {
                ModelState.AddModelError("", "call language api get all failed.");
                return View();
            }

            var navigationViewModel = new NavigationModel()
            {
                CurrentLanguageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId),
                Languages = apiResult.ResultObject
            };

            return View("Default", navigationViewModel);
        }
    }
}

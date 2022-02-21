using EshopAspCore.ApiIntegration;
using EshopAspCore.Utilities.Constants;
using EshopeMvcCore.Web.Models;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Controllers
{
    [AllowAnonymous] 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger, ISharedCultureLocalizer loc, ISlideApiClient slideApiClient, IProductApiClient productApiClient)
        {
            _logger = logger;
            _loc = loc;
            _slideApiClient = slideApiClient;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index()
        {
            //get slides
            var slidesResult = await _slideApiClient.GetAll();
            if (slidesResult.IsSuccessed == false)
            {
                ModelState.AddModelError("", "Api slide failed.");
            }

            var culture = CultureInfo.CurrentCulture.Name;

            var slides = slidesResult.ResultObject;

            //get feature products
            var featuredProductApiResult = await _productApiClient.GetFeaturedProduct(culture, SystemConstants.NumberOfFeaturedProducts);
            if (featuredProductApiResult.IsSuccessed == false)
            {
                ModelState.AddModelError("", "Api product feature return failed.");
            }
            var featuredProducts = featuredProductApiResult.ResultObject;

            //get latest products
            var latestProductApiResult = await _productApiClient.GetLatestProduct(culture, SystemConstants.NumberOfLatestProducts);
            if (latestProductApiResult.IsSuccessed == false)
            {
                ModelState.AddModelError("", "Api latest products return failed.");
            }

            var latestProducts = latestProductApiResult.ResultObject;

            var homeViewModel = new HomeViewModel()
            {
                Slides = slides,
                FeaturedProducts = featuredProducts,
                LatestProducts = latestProducts
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }


    }
}

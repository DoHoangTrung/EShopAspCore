using EshopAspCore.AdminApp.Services;
using EshopAspCore.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        //GET : /product/index
        public async Task<IActionResult> Index(string languageId,int pageIndex=1, int pageSize=10)
        {
            if (string.IsNullOrEmpty(languageId))
            {
                languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            }
            string url = $"/api/products?pageindex={pageIndex}&pagesize={pageSize}&languageId={languageId}";

            var result = await _productApiClient.GetAll(url);
            if (result.IsSuccessed)
            {
                var pageResult = result.ResultObject;
                var products = pageResult.Items;
                return View(products);
            }

            return BadRequest(result.Message);
        }
    }
}

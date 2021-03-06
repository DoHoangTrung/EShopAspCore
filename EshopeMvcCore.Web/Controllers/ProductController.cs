using EshopAspCore.ApiIntegration;
using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.Catalog.Products.Manage;
using EshopeMvcCore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }
              
        [HttpGet]
        public async Task<IActionResult> Details(string culture, int id)
        {
            var Product = await _productApiClient.GetById(id, culture);
            var category = await _categoryApiClient.GetById(id, culture);

            return View(new ProductDetailModel()
            {
                Category = category.ResultObject,
                Product = Product.ResultObject,
            });
        }

        [HttpGet]
        public async Task<IActionResult> Category(string selectionSortOrder,string culture, int id, int pageIndex = 1, int pageSize = 5)
        {
            var selection = new List<string>()
            {
                SystemConstants.SelectionSortOrder.PriceLowestFirst,
                SystemConstants.SelectionSortOrder.ProductNameAZ,
                SystemConstants.SelectionSortOrder.ProductNameZA,
                SystemConstants.SelectionSortOrder.ProductStocke,
            };

            var selectList = selection.Select(x => new SelectListItem()
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = x.ToString() == selectionSortOrder,
            });

            ViewBag.selectionSortOrder = selectList;
            

            var products = await _productApiClient.GetAll(new GetManageProductPagingRequest()
            {
                LanguageId = culture,
                CategoryId = id,
                PageIndex = pageIndex,
                PageSize = pageSize,
                SelectionSortOrder = selectionSortOrder,
            });

            var category = await _categoryApiClient.GetById(id, culture);
            return View(new ProductCategoryViewModel()
            {
                Category = category.ResultObject,
                ProductPages = products.ResultObject,
            });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keys, string selectionSortOrder, int pageIndex = 1, int pageSize = 10)
        {
            ViewBag.Keys = keys;

            var selection = new List<string>()
            {
                SystemConstants.SelectionSortOrder.PriceLowestFirst,
                SystemConstants.SelectionSortOrder.ProductNameAZ,
                SystemConstants.SelectionSortOrder.ProductNameZA,
                SystemConstants.SelectionSortOrder.ProductStocke,
            };

            var selectList = selection.Select(x => new SelectListItem()
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = x.ToString() == selectionSortOrder,
            });

            ViewBag.selectionSortOrder = selectList;


            var apiResult = await _productApiClient.GetAll(new GetManageProductPagingRequest()
            {
                LanguageId = CultureInfo.CurrentCulture.Name,
                PageIndex = pageIndex,
                PageSize = pageSize,
                SelectionSortOrder = selectionSortOrder,
                Keyword = keys
            });

            if (apiResult == null)
            {
                ModelState.AddModelError("", "Get all product is null.");
                return View(ModelState);
            }

            var product = apiResult.ResultObject;
            if (product.Items.Count <= 0)
                ViewData["MsgNoResult"] = $"No result with: {keys}";
            return View(new ProductSearchViewModel()
            {
                ProductPages = product,
            });
        }
    }
}

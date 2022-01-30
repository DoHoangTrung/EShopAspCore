using EshopAspCore.ApiIntegration;
using EshopAspCore.ViewModels.Catalog.Products.Manage;
using EshopeMvcCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using EshopAspCore.Utilities.Constants;

namespace EshopeMvcCore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> Category(string selectionSortOrder,string culture, int id, int pageIndex = 1, int pageSize = 10)
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
    }
}


using EshopAspCore.ApiIntegration;
using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Catalog.Products.Manage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Controllers
{
    [Authorize(Roles = SystemConstants.AppRole.Admin)]
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }

        //GET : /product/index
        [HttpGet]
        public async Task<IActionResult> Index(string languageId, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            if (string.IsNullOrEmpty(languageId))
            {
                languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            }

            //get categories
            var categoryApiResult = await _categoryApiClient.GetAll(languageId);
            if (categoryApiResult.IsSuccessed)
            {
                var categories = categoryApiResult.ResultObject;
                ViewBag.categories = categories.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = categoryId.HasValue && x.Id == categoryId
                });
            }
            else
            {
                return BadRequest(categoryApiResult.Message);
            }

            //get all product

            var result = await _productApiClient.GetAll(new GetManageProductPagingRequest()
            {
                LanguageId = languageId,
                CategoryId = categoryId,
                PageIndex = pageIndex,
                PageSize = pageSize,
            });

            if (result.IsSuccessed)
            {
                var pageResult = result.ResultObject;
                var products = pageResult.Items;
                return View(products);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest newProduct)
        {
            if (!ModelState.IsValid)
                return View();

            var isSuccess = await _productApiClient.Create(newProduct);
            if (isSuccess)
            {
                TempData[SystemConstants.AppSettings.SuccessMessage] = "Save product success.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Save product failed.");
            return View(newProduct);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var productApiResult = await _productApiClient.GetById(id, languageId);
            if (productApiResult.IsSuccessed == false)
            {
                ModelState.AddModelError("", productApiResult.Message);
                return View();
            }

            var productViewModel = productApiResult.ResultObject;

            var updateRequest = new ProductUpdateRequest()
            {
                CategoryIds = productViewModel.AllCategory,
                Description = productViewModel.Description,
                Details = productViewModel.Details,
                Id = productViewModel.Id,
                LanguageId = productViewModel.LanguageId,
                Name = productViewModel.Name,
                SeoAlias = productViewModel.SeoAlias,
                SeoDescription = productViewModel.SeoDescription,
                SeoTitle = productViewModel.SeoTitle,
            };

            return View(updateRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductUpdateRequest request)
        {
            //update categories of this product
            var isCategoryUpdateSuccess = await _categoryApiClient.Update(request.Id, request.CategoryIds);

            //update product informations
            var isProductUpdateSuccess = await _productApiClient.Update(request.Id, request);

            if (isCategoryUpdateSuccess == false && isProductUpdateSuccess == false)
            {
                return View(request);
            }

            TempData[SystemConstants.AppSettings.SuccessMessage] = "Update product successed!";
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var productApiResult = await _productApiClient.GetById(id, languageId);
            if (productApiResult.IsSuccessed == false)
            {
                ModelState.AddModelError("", productApiResult.Message);
                return View();
            }

            var product = productApiResult.ResultObject;

            return View(new ProductDetailViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                DateCreated = product.DateCreated,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoriesString = product.CategoriesString,
                Images = product.Images,
            }) ;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var productApiResult = await _productApiClient.GetById(id, languageId);
            if (productApiResult.IsSuccessed == false)
            {
                ModelState.AddModelError("", productApiResult.Message);
                return View();
            }

            var product = productApiResult.ResultObject;

            return View(new ProductDeleteRequest()
            {
                Id = product.Id,
                Name = product.Name,
                DateCreated = product.DateCreated,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest request)
        {
            var isSuccess = await _productApiClient.Delete(request.Id);

            if (isSuccess)
            {
                TempData[SystemConstants.AppSettings.SuccessMessage] = "Delete successed!";
                return RedirectToAction("Index", "Product");
            }

            ModelState.AddModelError("", "Delete failed.");
            return View(request);
        }
    }
}

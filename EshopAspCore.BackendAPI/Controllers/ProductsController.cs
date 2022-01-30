using EshopAspCore.Application.Catalog.Products;
using EshopAspCore.ViewModels.Catalog.ProductImages;
using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Catalog.Products.Manage;
using EshopAspCore.ViewModels.Catalog.Products.Public;
using EshopAspCore.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService publicProductService)
        {
            _productService = publicProductService;
        }

        //GET: api/products?pageIndex=1&pageSize=10&categoryId=1&languageId=vi-VN
        [HttpGet]
        public async Task<IActionResult> GetProductPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetAllPaging(request);
            if (products == null)
            {
                var errorResponse = new ApiErrorResult<PageResult<ProductViewModel>>("ERROR get all paging product service");
                return BadRequest(errorResponse);
            }

            var successedResponse = new ApiSuccessResult<PageResult<ProductViewModel>>(products);
            return Ok(successedResponse);
        }

        //GET: api/products/1
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _productService.GetById(productId, languageId);
            if (product == null)
                return BadRequest("Cannot find product");

            return Ok(product);
        }

        //POST: api/products
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productId = await _productService.Create(request);
            if (productId == 0)
                return BadRequest("Created product failed.");

            var product = await _productService.GetById(productId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new { id = product }, product);
        }

        //PUT: api/products/1
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var affortedProduct = await _productService.Update(request);
            if (affortedProduct == 0)
                return BadRequest();

            return Ok();
        }


        //DELETE: api/products/1
        [HttpDelete("{productId}")]
        [Authorize]
        public async Task<IActionResult> Delete (int productId)
        {
            var affortedProduct = await _productService.Delete(productId);
            if (affortedProduct == 0)
                return BadRequest(false);

            return Ok(true);
        }

        [HttpPatch("{productId}/{newPrice}")]
        [Authorize]

        public async Task<IActionResult> UpdatePrice (int productId, decimal newPrice)
        {
            var isSuccessful = await _productService.UpdatePrice(productId,newPrice);
            if (!isSuccessful)
                return BadRequest();

            return Ok();
        }


        //POST: api/products/1/images
        [HttpPost("{productId}/images")]
        [Authorize]

        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imageId = await _productService.AddImage(productId,request);
            if (imageId == 0) 
                return BadRequest("Created product failed.");

            var image = await _productService.GetProductImageById(imageId);
            if (image == null)
                return BadRequest();
            
            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        //GET: api/products/1/images/1
        [HttpGet("{productId}/images/{imageId}")]

        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _productService.GetProductImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find product");
            
            return Ok(image);
        }


        [HttpPut("{productId}/images/{imageId}")]
        [Authorize]

        public async Task<IActionResult> UpdateImage(int imageId,[FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var affortedProduct = await _productService.UpdateImage(imageId,request);
            if (affortedProduct == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        [Authorize]

        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var affortedProduct = await _productService.RemoveImage(imageId);
            if (affortedProduct == 0)
                return BadRequest();

            return Ok();
        }


        //GET: api/products/featured/4/vi-VN
        [HttpGet("featured/{take}/{languageId}")]
        public async Task<IActionResult> GetFeaturedProducts(int take, string languageId)
        {
            var products = await _productService.GetFeaturedProduct(languageId, take);
            if (products == null)
            {
                var errorResponse = new ApiErrorResult<List<ProductViewModel>>("Slide service return NULL.");
                return BadRequest(errorResponse);
            }

            var sussessResponse = new ApiSuccessResult<List<ProductViewModel>>(products);
            return Ok(sussessResponse);
        }

        //GET: api/products/latest/4/vi-VN
        [HttpGet("latest/{take}/{languageId}")]
        public async Task<IActionResult> GetLatestProducts(int take, string languageId)
        {
            var products = await _productService.GetLatestProduct(languageId, take);
            if (products == null)
            {
                var errorResponse = new ApiErrorResult<List<ProductViewModel>>("Slide service return NULL.");
                return BadRequest(errorResponse);
            }

            var sussessResponse = new ApiSuccessResult<List<ProductViewModel>>(products);
            return Ok(sussessResponse);
        }
    }
}

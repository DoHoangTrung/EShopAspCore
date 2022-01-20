using EshopAspCore.Application.Catalog.Categories;
using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Common;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //GET: api/categories?languageId=
        [HttpGet]
        public async Task<IActionResult> GetAll(string languageId)
        {
            var categories = await _categoryService.GetAll(languageId);
            if (categories != null)
            {
                var successResponse = new ApiSuccessResult<List<CategoryViewModel>>(categories);
                return Ok(successResponse);
            }

            var errorResponse = new ApiErrorResult<List<CategoryViewModel>>("get all categories return null");
            return BadRequest(errorResponse);
        }

        //GET: api/categories?productId=1&languageId=vi-VN
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetByProductId(int productId, string languageId)
        {
            var categories = await _categoryService.GetById(productId, languageId);
            if (categories != null)
            {
                var successResponse = new ApiSuccessResult<List<CategoryViewModel>>(categories);
                return Ok(successResponse);
            }

            var errorResponse = new ApiErrorResult<List<CategoryViewModel>>("get categories by id product return null");
            return BadRequest(errorResponse);
        }
    }
}

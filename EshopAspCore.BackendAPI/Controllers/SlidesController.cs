using EshopAspCore.Application.Utilities.Slides;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.Utilities.Slides;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SlidesController : ControllerBase
    {
        private readonly ISlideService _slideService;

        public SlidesController(ISlideService slideService)
        {
            _slideService = slideService;
        }

        //GET: api/slides
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var slides = await _slideService.GetAll();

            if (slides == null)
            {
                var errorResponse = new ApiErrorResult<List<SlideViewModel>>("Slide service return NULL.");
                return BadRequest(errorResponse);
            }

            var sussessResponse = new ApiSuccessResult<List<SlideViewModel>>(slides);
            return Ok(sussessResponse);
        }
    }
}

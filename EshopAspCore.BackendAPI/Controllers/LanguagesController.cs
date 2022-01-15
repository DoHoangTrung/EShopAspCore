using EshopAspCore.Application.System.Languages;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Languages;
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
    public class LanguagesController:ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        //GET: api/languages
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _languageService.GetAll();
            if(roles == null)
            {
                var errorResult = new ApiErrorResult<List<LanguageViewModel>>("get all api return null");
                return BadRequest(errorResult);
            }

            var successResult = new ApiSuccessResult<List<LanguageViewModel>>(roles);
            return Ok(successResult);
        }
    }
}

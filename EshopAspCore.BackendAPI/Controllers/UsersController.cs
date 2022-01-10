using EshopAspCore.Application.System.Users;
using EshopAspCore.ViewModels.System.Users;
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
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        //POST: api/users/authenticate
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.AuthenticateAsync(request);
            if (string.IsNullOrEmpty(response.ResultObject))
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }

        //POST:api/users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.Register(request);
            if (!response.IsSuccessed)
                return BadRequest(response);

            return Ok(response);
        }

        //PUT: api/users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,[FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.Update(id, request);
            if (response.IsSuccessed)
                return Ok(response);

            return BadRequest(response);
        }

        //GET: api/users/paging?pageIndex=1&pageSize=10&keywords=abc
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respose = await _userService.GetUserPaging(request);

            return Ok(respose);
        }

        //GET: api/users/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var apiResult =await _userService.GetById(id);
            if (apiResult.IsSuccessed)
            {
                return Ok(apiResult);
            }

            return BadRequest(apiResult);
        }
    }
}

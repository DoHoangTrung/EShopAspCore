﻿using EshopAspCore.Application.System.Users;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultToken = await _userService.AuthenticateAsync(request);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Login failed! Please check your username or password");
            }
            
            return Ok(resultToken);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isSuccessful = await _userService.Register(request);
            if (!isSuccessful)
                return BadRequest("Login failed! Please check your username or password");

            return Ok();
        }

        //GET: users/paging?pageIndex=1&pageSize=10&keywords=abc
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = await _userService.GetUserPaging(request);

            return Ok(users);
        }
    }
}

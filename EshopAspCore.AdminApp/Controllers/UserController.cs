using EshopAspCore.AdminApp.Services;
using EshopAspCore.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;

        private readonly IConfiguration _configuration;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        
        [HttpGet]
        public async Task<IActionResult> Index(string keywords, int pageIndex = 1, int pageSize = 10)
        {
            ViewBag.keywords = keywords;
            //get list users
            //1.get key word, page idnex, page size
            //2.get token
            string token = HttpContext.Session.GetString("Token");
            //3.create getUserRequest
            var request = new GetUserPagingRequest()
            {
                BearerToken = token,
                Keywords = keywords,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
            //4.call service
            var result = await _userApiClient.GetUsersPaging(request);
            var pageResult = result.ResultObject;
            return View(pageResult);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //sign out when go to login page
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var apiResult = await _userApiClient.Authenticate(request);
            if (!apiResult.IsSuccessed)
            {
                ModelState.AddModelError("", apiResult.Message);
                return View();
            }

            var token = apiResult.ResultObject;
            
            var userPrincipals = ValidateToken(apiResult.ResultObject);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5), //if authenticate is not using for about 5m, it removed
                IsPersistent = true,
            };

            HttpContext.Session.SetString("Token", token);

            //login 
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipals,
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            //sign out when go to login page
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("Token");

            return RedirectToAction("Login", "User");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Register(request);
            if (result.IsSuccessed)
            {
                TempData["SuccessMsg"] = "Register successed.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken tokenSecure;
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters();
            validations.ValidateLifetime = true;
            validations.ValidAudience = _configuration["Tokens:Issuer"];
            validations.ValidIssuer = _configuration["Tokens:Issuer"];
            validations.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal claimsPrincipal = handler.ValidateToken(jwtToken, validations, out tokenSecure);

            return claimsPrincipal;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if(!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }

            var user = result.ResultObject;

            var updateRequest = new UserUpdateRequest()
            {
                Id = id,
                Dob = user.Dob,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            };

            return View(updateRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Update(request.Id,request);
            if (result.IsSuccessed)
            {
                TempData["SuccessMsg"] = "Edit successed.";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details (Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }

            var user = result.ResultObject;

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (!result.IsSuccessed)
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }

            var user = result.ResultObject;

            var userDeleteRequest = new UserDeleteRequest()
            {
                Id = user.Id,
                Dob = user.Dob,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
            };

            return View(userDeleteRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest request)
        {
            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {
                TempData["SuccessMsg"] = "Delete successed.";
                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError("", result.Message);
            return View();
        }
    }
}

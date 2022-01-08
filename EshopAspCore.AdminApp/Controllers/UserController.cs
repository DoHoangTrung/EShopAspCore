using EshopAspCore.AdminApp.Services;
using EshopAspCore.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public async Task<IActionResult> Index(string keywords, int pageIndex =1, int pageSize=10)
        {
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
                PageSize = pageIndex,
            };
            //4.call service
            var pageResult = await _userApiClient.GetUsersPaging(request);

            return View(pageResult);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            //sign out when go to login page
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userApiClient.Authenticate(request);

            var userPrincipals = ValidateToken(token);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false,
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

        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return View();
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
    }
}

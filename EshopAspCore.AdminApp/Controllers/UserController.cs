using EshopAspCore.AdminApp.Services;
using EshopAspCore.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public IActionResult Index()
        {
            var name = User.Identity.Name;
            return View();
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

            return RedirectToAction("Login","User");
        }

        [HttpPost]
        public IActionResult Register()
        {
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

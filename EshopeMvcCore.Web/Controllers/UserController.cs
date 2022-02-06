using EshopAspCore.ApiIntegration;
using EshopAspCore.Utilities.Constants;
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

namespace EshopeMvcCore.Web.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IUserApiClient _userApiClient;

        private readonly IConfiguration _configuration;

        private readonly IRoleApiClient _roleApiClient;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration, IRoleApiClient roleApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _roleApiClient = roleApiClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult > Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

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
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(45), //if authenticate is not using for about 5m, it removed
                IsPersistent = false,
            };

            HttpContext.Session.SetString("Token", token);

            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, _configuration[SystemConstants.AppSettings.DefaultLanguageId]);

            //login 
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipals,
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            //sign out when go to login page
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("Token");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string culture, RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.Register(request);
            if (result.IsSuccessed)
            {
                TempData["SuccessMsg"] = "Register successed.";
                return Redirect($"/{culture}/user/login");
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
    }
}

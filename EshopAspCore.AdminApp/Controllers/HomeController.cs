using EshopAspCore.AdminApp.Models;
using EshopAspCore.Utilities.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Controllers
{
    [Authorize(Roles = SystemConstants.AppRole.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //POST: /home/language
        [HttpPost]
        public IActionResult Language(NavigationModel model)
        {
            if(model == null)
            {
                ModelState.AddModelError("", "parameter is null");
                return BadRequest(ModelState);
            }

            HttpContext.Session.SetString(SystemConstants.AppSettings.DefaultLanguageId, model.CurrentLanguageId);

            return Redirect(model.CurrentUrl);
        }
    }
}

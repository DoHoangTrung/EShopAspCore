using EshopAspCore.Application.Common;
using EshopAspCore.ViewModels.Common;
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
    [AllowAnonymous]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        //POST: api/emails
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MailContent mailContent)
        {
            var result = await _emailService.SendAsyn(mailContent);

            if (result == true) return Ok(result);

            return BadRequest(result);
        }
    }
}

using EshopAspCore.Application.Sales;
using EshopAspCore.ViewModels.Sales;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _checkoutService;

        public OrdersController(IOrderService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        //POST: api/orders
        [HttpPost]
        public async Task<IActionResult> SaveOrders(CheckOutRequest request)
        {
            var result = await _checkoutService.CheckOutOrders(request);
            if (result == true)
                return Ok(result);

            return BadRequest(result);
        }
    }
}

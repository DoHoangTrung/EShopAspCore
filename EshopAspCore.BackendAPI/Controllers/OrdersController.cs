using EshopAspCore.Application.Sales;
using EshopAspCore.Data.Enum;
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
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //POST: api/orders
        [HttpPost]
        public async Task<IActionResult> SaveOrders(CheckOutRequest request)
        {
            var result = await _orderService.CheckOutOrders(request);
            if (result == true)
                return Ok(result);

            return BadRequest(result);
        }

        //GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OrderGetRequest request)
        {
            var orders = await _orderService.GetAll(request);
            return Ok(orders);
        }

        //GET: api/orders/1/vi
        [HttpGet("{id}/{languageId}")]
        public async Task<IActionResult> GetById(int id, string languageId)
        {
            var orders = await _orderService.GetById(id, languageId);
            return Ok(orders);
        }

        //PUT: api/oders/1/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] OrderStatus newStatus)
        {
            int rowAffected = await _orderService.UpdateStatus(id, newStatus);
            return Ok(rowAffected);
        }

        //DELETE: api/orders/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isSuccess = await _orderService.Delete(id);
            if (isSuccess) 
                return Ok(isSuccess);
            else 
                return BadRequest(isSuccess);
        }
    }
}

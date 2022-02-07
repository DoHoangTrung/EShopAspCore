using EshopAspCore.ApiIntegration;
using EshopAspCore.Data.Enum;
using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.Sales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Controllers
{
    [Authorize(Roles = SystemConstants.AppRole.Admin)]
    public class OrderController : Controller
    {
        private readonly IOrderApiClient _orderApiClient;

        public OrderController(IOrderApiClient orderApiClient)
        {
            _orderApiClient = orderApiClient;
        }

        public async Task<IActionResult> Index(string status)
        {
            //find enum by string
            OrderStatus orderstatus = (OrderStatus)System.Enum.Parse(typeof(OrderStatus), status);

            var orders = await _orderApiClient.GetAll(new OrderGetRequest()
            {
                status = orderstatus,
            });

            return View(orders);
        }

        //GET: order/print?orderId=1
        [HttpGet]
        public async Task<IActionResult> Print(int orderId)
        {
            string languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var order = await _orderApiClient.GetById(orderId, languageId);
            return View(order);
        }
    }
}

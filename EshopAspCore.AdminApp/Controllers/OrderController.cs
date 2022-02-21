using EshopAspCore.AdminApp.Models;
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

        public async Task<IActionResult> Index(OrderStatus status)
        {
            //find enum by string
            var orders = await _orderApiClient.GetAll(new OrderGetRequest()
            {
                status = status,
            });

            return View(new OrderPageViewModel()
            {
                Orders = orders,
                Status = status,
                listState = new ManageOrderStatus(status).GetManageOrderState()
            });
        }

        //GET: order/print?orderId=1
        [HttpGet]
        public async Task<IActionResult> Print(int orderId)
        {
            string languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var order = await _orderApiClient.GetById(orderId, languageId);
            return View(order);
        }

        //POST  
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatus status)
        {
            var affected = await _orderApiClient.UpdateStatus(id, status);
            return Ok(affected);
        }

        //GET: order/details/1
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var order = await _orderApiClient.GetById(id, languageId);

            return View(order);
        }

        //GET: order/delete/1
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var order =await _orderApiClient.GetById(id, languageId);
            return View(order);
        }

        //POST: order/PostDelete
        [HttpPost]
        public async Task<IActionResult> PostDelete(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var isSuccess = await _orderApiClient.Delete(id);
            if (isSuccess)
            {
                TempData[SystemConstants.AppSettings.SuccessMessage] = "Delete order success.";
                return RedirectToAction(nameof(Index));
            }

            TempData[SystemConstants.AppSettings.ErrorMessage] = "Delete order failed!";
            return RedirectToAction(nameof(Index));

        }
    }
}

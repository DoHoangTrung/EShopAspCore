using EshopAspCore.ApiIntegration;
using EshopAspCore.Utilities.Constants;
using EshopeMvcCore.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopeMvcCore.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;

        public CartController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var cart = new CartViewModel();

            var currentCart = HttpContext.Session.GetString(SystemConstants.CartSession);

            if (currentCart != null)
            {
                cart = JsonConvert.DeserializeObject<CartViewModel>(currentCart);
            }

            return Ok(cart);
        }

        //POST: vi/cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, string culture)
        {
            var apiResult = await _productApiClient.GetById(id, culture);
            var product = apiResult.ResultObject;

            var cart = new CartViewModel();

            var currentCart = HttpContext.Session.GetString(SystemConstants.CartSession);
            if (currentCart != null)
            {
                cart = JsonConvert.DeserializeObject<CartViewModel>(currentCart);
            }

            int quantity = 1;
            if (cart.Items.Any(x => x.Id == id))
            {
                var item = cart.Items.First(x => x.Id == id);
                item.Quantity += 1;
                item.TotalCost = item.Quantity * item.Price;
            }
            else
            {
                var item = new CartItemViewModel()
                {
                    Id = id,
                    Description = product.Description,
                    Image = product.ThumbnailImage,
                    Name = product.Name,
                    Quantity = quantity,
                    Price = product.Price,
                    TotalCost = quantity * product.Price,
                };
                cart.Items.Add(item);
            }

            cart.TotalPrice = cart.Items.Sum(x => x.TotalCost);

            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(cart));

            return Ok();
        }
    }
}

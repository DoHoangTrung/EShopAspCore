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
                item.TotalPrice = item.Quantity * item.Price;
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
                    TotalPrice = quantity * product.Price,
                };
                cart.Items.Add(item);
            }

            cart.TotalCartPrice = cart.Items.Sum(x => x.TotalPrice);

            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(cart));

            return Ok();
        }

        //POST: vi/cart/updateCart
        [HttpPost]
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cart = new CartViewModel();

            var currentCart = HttpContext.Session.GetString(SystemConstants.CartSession);
            if (currentCart != null)
            {
                cart = JsonConvert.DeserializeObject<CartViewModel>(currentCart);
            }

            foreach (var item in cart.Items)
            {
                if (item.Id == id)
                {
                    if (quantity == 0)
                    {
                        cart.Items.Remove(item);
                        break;
                    }

                    item.Quantity = quantity;
                    item.TotalPrice = quantity * item.Price;
                    break;
                }
            }

            cart.TotalCartPrice = cart.Items.Sum(x => x.TotalPrice);

            HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(cart));
            return Ok(cart);
        }
    }
}

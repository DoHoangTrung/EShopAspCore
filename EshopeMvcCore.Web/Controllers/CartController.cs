using EshopAspCore.ApiIntegration;
using EshopAspCore.Utilities.Constants;
using EshopAspCore.ViewModels.Sales;
using EshopeMvcCore.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EshopAspCore.ViewModels.Common;

namespace EshopeMvcCore.Web.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IOrderApiClient _orderApiClient;

        public CartController(IProductApiClient productApiClient, IOrderApiClient orderApiClient)
        {
            _productApiClient = productApiClient;
            _orderApiClient = orderApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var cart = GetCartSession();

            return Ok(cart);
        }

        private CartViewModel GetCartSession()
        {
            var cart = new CartViewModel();

            var currentCart = HttpContext.Session.GetString(SystemConstants.CartSession);

            if (currentCart != null)
            {
                cart = JsonConvert.DeserializeObject<CartViewModel>(currentCart);
            }
            return cart;
        }

        //POST: vi/cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, string culture, int quantity= 1)
        {
            var apiResult = await _productApiClient.GetById(id, culture);
            var product = apiResult.ResultObject;

            var cart = GetCartSession();

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
                    Stock = product.Stock
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
            var cart = GetCartSession();

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

        //GET: vi/cart/checkout
        [HttpGet]
        public IActionResult CheckOut()
        {
            var cart = GetCheckOutCart();

            return View(cart);
        }

        //POST: vi/cart/checkout
        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutViewModel model)
        {
            var cart = GetCheckOutCart();

            var request = new CheckOutRequest()
            {
                Name = model.Name,
                Phone = model.Phone,
                UserId = model.UserId,
                Address = model.Address,
                Email = model.Email,
            };

            foreach (var item in cart.cartItems)
            {
                //check stock
                var stock = await _productApiClient.GetStock(item.Id);
                if (stock < item.Quantity)
                {
                    TempData[SystemConstants.AppSettings.ErrorMessage] = $"Mặt hàng {item.Name} không đủ số lượng trong kho";
                    return RedirectToAction(nameof(CheckOut));
                }

                request.cartItems.Add(new OrderItemViewModel()
                {
                    ProductId = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            };

            var isSuccess = await _orderApiClient.CheckOut(request);
            if (isSuccess)
            {
                //send email
                ViewData[SystemConstants.AppSettings.SuccessMessage] = "Checkout success";

                var textCart = "<p>Thank you for chosing our <strong>Eshop</strong><br/>You have checking out success:<br/>";
                for (int i = 0; i < cart.cartItems.Count; i++)
                {
                    var item = cart.cartItems[i];
                    textCart += $"{i + 1}_{item.Name}, quantity: {item.Quantity}, price: {item.Price}<br/>";
                }
                textCart += "Your orders will be ship soon</p>";

                var isSuccessed = await _orderApiClient.SendEmail(new MailContent()
                {
                    To = request.Email,
                    Body = textCart,
                    Subject = "checkout succesed",
                });
            }

            return View();
        }

        //GET: vi/cart/countCartItem
        [HttpGet]
        public IActionResult CountCartItem()
        {
            var cart = GetCartSession();
            return Ok(cart.Items.Count);
        }

        private CheckOutViewModel GetCheckOutCart()
        {
            var cart = new CartViewModel();

            var currentCart = HttpContext.Session.GetString(SystemConstants.CartSession);
            if (currentCart != null)
            {
                cart = JsonConvert.DeserializeObject<CartViewModel>(currentCart);
            }

            return new CheckOutViewModel()
            {
                cartItems = cart.Items,
                TotalCartPrice = cart.TotalCartPrice
            };
        }


        [HttpPost]
        //GET: vi/cart/updateCartSession
        public async Task<IActionResult> UpdateCartSession(CartUpdateRequest request)
        {
            if (request != null)
            {
                var cart = GetCartSession();

                foreach (var item in request.Items)
                {
                    //check stock
                    var stock = await _productApiClient.GetStock(item.Id);
                    if (stock < item.Quantity)
                        return BadRequest($"Sản phẩm {item.Id} chỉ còn {stock} sản phẩm");

                    var prod = cart.Items.Find(x => x.Id == item.Id);
                    if (prod != null)
                    {
                        var oldQuantity = prod.Quantity;
                        prod.Quantity = item.Quantity;
                        prod.TotalPrice += (item.Quantity - oldQuantity) * prod.Price;
                    }
                }

                HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(cart));
            }
            return Ok();
        }
    }
}

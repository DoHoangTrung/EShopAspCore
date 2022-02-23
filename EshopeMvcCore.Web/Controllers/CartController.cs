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
using Microsoft.Extensions.Configuration;
using EshopAspCore.Application.Utilities.Confirm;
using EshopAspCore.Data.Enum;

namespace EshopeMvcCore.Web.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IOrderApiClient _orderApiClient;
        private readonly IConfiguration _config;

        public CartController(IProductApiClient productApiClient, IOrderApiClient orderApiClient, IConfiguration config)
        {
            _productApiClient = productApiClient;
            _orderApiClient = orderApiClient;
            _config = config;
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
                item.Quantity += quantity;
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
            

            if (!ModelState.IsValid)
            {
                var cartSs = GetCheckOutCart();

                return View(cartSs);
            }

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

            var orderId = await _orderApiClient.CheckOut(request);

            if (orderId <= 0)
            {
                return View();
            }

            ClearSessionCart();

            //send email
            try
            {
                //create confirm link 
                string key = _config["Tokens:Key"];
                string state = Enum.GetName(typeof(OrderStatus), OrderStatus.Confirmed);
                var token = AesOperation.EncryptString(key, state);

                string confirmLink = Url.Action("ConfirmOrder", "Cart" , new
                {
                    id = orderId,
                    token = token
                }, protocol: HttpContext.Request.Scheme);

                var textCart = "<p>Thank you for chosing our <strong>Eshop</strong><br/>You have checking out success:<br/>";
                for (int i = 0; i < cart.cartItems.Count; i++)
                {
                    var item = cart.cartItems[i];
                    textCart += $"{i + 1}_{item.Name}, quantity: {item.Quantity}, price: {item.Price}<br/>";
                }

                textCart += $"<p>Please confirm your order:</p> </br>" + confirmLink;

                var sendMailSuccess = await _orderApiClient.SendEmail(new MailContent()
                {
                    To = request.Email,
                    Body = textCart,
                    Subject = "Checkout succesed",
                });

                if (!sendMailSuccess)
                {
                    ViewData[SystemConstants.AppSettings.SuccessMessage] = "Checkout success, we will contact to you by your phone soon.";
                    return View();
                }
                else
                {
                    ViewData[SystemConstants.AppSettings.SuccessMessage] = "Checkout success, please check your email";
                    return View();
                }
            }
            catch(Exception ex)
            {
                ViewData[SystemConstants.AppSettings.SuccessMessage] = "Checkout success, we will contact to you by your phone soon.";
                return View();
            }
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

        private void ClearSessionCart()
        {
            HttpContext.Session.Remove(SystemConstants.CartSession); 
        }

        //GET: vi/cart/updateCartSession
        [HttpPost]
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

                cart.TotalCartPrice = cart.Items.Sum(x => x.TotalPrice);

                HttpContext.Session.SetString(SystemConstants.CartSession, JsonConvert.SerializeObject(cart));
            }
            return Ok();
        }

        //GET: cart/ConfirmOrder
        [HttpGet] 
        public async Task<IActionResult> ConfirmOrder (int id,string token)
        {
            string errorMsg = "Your order can't confirm. Please contact us to solve this problem.";

            string key = _config["Tokens:Key"];

            string value = AesOperation.DecryptString(key,token);

            //if this is comfirmed state, confirmed this order
            OrderStatus state;
            if (!Enum.TryParse(value, out state))
            {
                ViewData[SystemConstants.AppSettings.ErrorMessage] = errorMsg;
                return View();
            }

            if (state != OrderStatus.Confirmed)
            {
                ViewData[SystemConstants.AppSettings.ErrorMessage] = errorMsg;
                return View();
            }

            //state = confirmed
            var rowAffected =await _orderApiClient.UpdateStatus(id, state);
            if (rowAffected == 0)
            {
                ViewData[SystemConstants.AppSettings.ErrorMessage] = errorMsg;
                return View();
            }

            ViewData[SystemConstants.AppSettings.SuccessMessage]= "This order confirm succeed.";
            return View();
        }
    }
}

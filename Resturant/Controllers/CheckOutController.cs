using Microsoft.AspNetCore.Mvc;
using Resturant.Data;
using Resturant.Models;
using Stripe.Checkout;
using Stripe;
using Microsoft.AspNetCore.Identity;
using Resturant.Interfaces;

namespace Resturant.Controllers
{
    public class CheckOutController : UserBaseController
    {

        private readonly YummyDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IShopingRepository _shopingRepository;

        public CheckOutController(YummyDbContext Db, UserManager<User> userManager, IShopingRepository shopingRepository)
        {
            _db = Db;
            _userManager=userManager;
            _shopingRepository=shopingRepository;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }
            var productList = _shopingRepository.GetUserCart(user.Id);

            return View(productList);
        }

        public IActionResult OrderConfirmation()
        {
            if (TempData["Session"] != null)
            {
                var service = new SessionService();
                var session = service.Get(TempData["Session"].ToString());

                if (session.PaymentStatus == "paid")
                {
                    return View("Succses");
                }
                return View("Login");
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult Succses()
        {
            return View();
        }








        public async Task<IActionResult> CheckOut()
        {
            var user = await _userManager.GetUserAsync(User);

            var customerEmail = user.Email;

            var productList = _db.shoppingCartItems
                .Where(item => item.UserId == user.Id)
                .ToList();

            var domain = "https://localhost:7138/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "CheckOut/OrderConfirmation",
                CancelUrl = domain + "UserAuth/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = user.Email
            };

            foreach (var item in productList)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.MenuName.ToString(),
                        }
                    },
                    Quantity = item.Quantity
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            if (session.Id != null)
            {
                Response.Headers.Add("Location", session.Url);
                TempData["Session"] = session.Id;
                return new StatusCodeResult(303);
            }
            else
            {
                return NotFound();
            }
        }
    }
}

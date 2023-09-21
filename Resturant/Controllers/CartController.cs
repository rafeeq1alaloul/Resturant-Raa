using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant.Interfaces;
using Resturant.Models;

namespace StripWithCart.Controllers
{
    public class CartController : Controller
    {
        private readonly IShopingRepository _shoppingCart;
        private readonly UserManager<User> _userManager;

        public CartController(IShopingRepository shoppingCart, UserManager<User> userManager)
        {
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddToCartAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            await _shoppingCart.AddToCartAsync(id, user);
            TempData["Sucsees"]= "تمت إضافة عنصر إلى السلة";
            return RedirectToAction("Menu", "Home");
        }

        public IActionResult Delete(int id) {
            _shoppingCart.Delete(id);
            return Ok(new { message = "تم الحذف بنجاح" });
        }
    }
}
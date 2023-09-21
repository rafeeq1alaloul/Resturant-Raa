
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resturant.Data;
using Resturant.Interfaces;
using Resturant.Models;
using System.Security.Claims;

namespace Resturant.Repositories
{
    public class ShopingRepository : IShopingRepository
    {   


        private readonly YummyDbContext items;
        private readonly UserManager<User> _userManager;

        public ShopingRepository(YummyDbContext context, UserManager<User> userManager)
        {
            items = context;
            _userManager=userManager;
        }



        public async Task AddToCartAsync(int id, User userId)
        {

            var menuid = items.menus.FirstOrDefault(item => item.MenuId == id);

            var cart = items.shoppingCartItems.FirstOrDefault(item => item.MenuId == id && item.UserId == userId.Id);

            ShoppingCartItem shoppingCartItem = new ShoppingCartItem();

            if (cart != null)
            {
                cart.Quantity++;
                items.Update(cart);
                items.SaveChanges();
            }
            else
            {
                shoppingCartItem.MenuId = menuid.MenuId;
                shoppingCartItem.MenuName = menuid.MenuName;
                shoppingCartItem.Price = menuid.Price;
                shoppingCartItem.Quantity = 1;
                shoppingCartItem.ImagePath = menuid.ImagePath;
                shoppingCartItem.UserId = userId.Id;

                items.Add(shoppingCartItem);
                items.SaveChanges();
            }
        }

        public List<ShoppingCartItem> GetUserCart(string userId)
        {
            return items.shoppingCartItems.Where(c => c.UserId == userId).ToList();
        }

        public void Delete(int id)
        {
            var cart = items.shoppingCartItems.Find(id);

            items.Remove(cart);
            items.SaveChanges();
        }

    }
}
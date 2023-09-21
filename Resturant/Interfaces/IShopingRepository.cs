using Resturant.Models;

namespace Resturant.Interfaces
{
    public interface IShopingRepository
    {
        Task AddToCartAsync(int id, User userId);
        List<ShoppingCartItem> GetUserCart(string userId);
        void Delete(int id);

    }
}

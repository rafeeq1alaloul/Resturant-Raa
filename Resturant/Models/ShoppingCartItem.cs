namespace Resturant.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
        public string? UserId { get; set; }

    }

}
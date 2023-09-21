using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Ingredients { get; set; }

        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }

        public int MeatId { get; set; }
        public Meat Meat { get; set; }


    }
}

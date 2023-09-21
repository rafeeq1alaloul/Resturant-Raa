using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant.Models
{
    public class Events
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string? ImagePath { get; set;}

        [NotMapped]
        public IFormFile Image { get; set; }

    }
}

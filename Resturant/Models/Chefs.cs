using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Resturant.Models
{
    public class Chefs
    {
        public int Id { get; set; }
        // [Required]
        public string Name { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
        // [Required]
        public string Officium { get; set; }
        // [Required]
        public string Description { get; set; }

        // [Required]
        public string Facebook { get; set; }
        // [Required]
        public string Twitter { get; set; }
        // [Required]
        public string Instagram { get; set; }
        // [Required]
        public string LinkedIn { get; set; }

    }
}

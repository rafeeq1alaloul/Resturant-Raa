using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant.Models
{
    public class Meat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Menu> Menus { get; set; }

    }
}

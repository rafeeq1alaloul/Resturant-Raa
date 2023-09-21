using Microsoft.AspNetCore.Mvc.Rendering;
using Resturant.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resturant.ViewModel
{
    public class MenuVM
    {
        public int MenuId { get; set; }
        [Display(Name ="Meat Name")]
//        [Required(ErrorMessage = "اسم الوجبة مطلوب")]
        [Required]
        public string MenuName { get; set; }
//        [Required(ErrorMessage = "المكونات مطلوبة")]
        [Required]
        public string Ingredients { get; set; }
//        [Required(ErrorMessage = "الصورة مطلوبة")]
        [Required]
        public IFormFile Image { get; set; }
//        [Required(ErrorMessage = "السعر مطلوب")]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int MeatId { get; set; }
        public List<SelectListItem>? MeatList { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Resturant.ViewModel
{
    public class ContactVM
    {
        [Required(ErrorMessage = "الأسم مطلوب")]
        public string Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "البريد الألكتروني مطلوب")]
        public string Email { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        public string Address { get; set; }

        [Required(ErrorMessage = "المجينة مطلوبة")]
        public string City { get; set; }

        [Required(ErrorMessage = "يجب أن تملأ الحقل ب10 أحرف على الأقل")]
        [MinLength(10), MaxLength(150)]
        public string Massage { get; set; }
    }
}

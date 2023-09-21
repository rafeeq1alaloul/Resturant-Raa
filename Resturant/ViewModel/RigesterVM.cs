using System.ComponentModel.DataAnnotations;

namespace Resturant.ViewModel
{
    public class RigesterVM
    {
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الأسم الأول")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الأسم الأخير")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [EmailAddress]
        [Display(Name = "البريد الألكتروني")]
        public string Email { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "كلمتين السر غير متطابقتين")]
        [Display(Name = "تأكيد كلمة السر")]
        public string ConfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Resturant.ViewModel
{
    public class LoginVM
    {

        [Required]
        [EmailAddress]
        [Display(Name = "البريد الألكتروني")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة السر")]
        public string Password { get; set; }

//        public bool RememberMe { get; set; }


    }
}

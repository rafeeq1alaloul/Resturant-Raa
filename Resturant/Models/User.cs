using Microsoft.AspNetCore.Identity;

namespace Resturant.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}

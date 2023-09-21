using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Resturant.Controllers
{
    [Authorize]
    public class UserBaseController : Controller
    {
    }
}

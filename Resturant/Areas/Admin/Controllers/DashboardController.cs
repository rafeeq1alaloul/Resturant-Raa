using Microsoft.AspNetCore.Mvc;

namespace Resturant.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

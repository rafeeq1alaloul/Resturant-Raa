using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant.Data;
using Resturant.Models;
using Resturant.ViewModel;
using System.Diagnostics;

namespace Resturant.Controllers
{
    public class HomeController : UserBaseController
    {
        private readonly YummyDbContext _yummyDbContext;
        private readonly UserManager<User> _userManager;

        public HomeController(YummyDbContext yummyDbContext, UserManager<User> userManager)
        {
            _yummyDbContext = yummyDbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View(_yummyDbContext.menus.ToList());
        }

        public IActionResult Events()
        {
            return View(_yummyDbContext.events.ToList());
        }

        public IActionResult Chefs()
        {
            var chefs = _yummyDbContext.chefs.ToList();
            return View(chefs);
        }

        public async Task<IActionResult> ContactsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            ContactVM contact = new ContactVM();
            contact.Email = user.Email;
            return View(contact);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacts(ContactVM contactVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                Contact contact = new Contact();
                contact.Name = contactVM.Name;
                contact.Email = user.Email;
                contact.Phone = contactVM.Phone;
                contact.City = contactVM.City;
                contact.Address = contactVM.Address;
                contact.Massage = contactVM.Massage;
                TempData["Succses Massage"] = "لقد تم الأرسال بنجاح";
                _yummyDbContext.Add(contact);
                _yummyDbContext.SaveChanges();
                return RedirectToAction(nameof(Contacts));
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                ContactVM contact = new ContactVM();
                contact.Email = user.Email;
                ModelState.AddModelError("", "البيانات غير صحيحة");
                return View(contact);
            }
        }

    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resturant.Models;
using Resturant.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Resturant.Controllers
{
    [AllowAnonymous]
    public class UserAuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserAuthController(UserManager<User> userManager, SignInManager<User> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Rigester()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Rigester(RigesterVM rigesterVM)
        {
            if (ModelState.IsValid)
            {

                User user = new User
                {
                    Email = rigesterVM.Email,
                    UserName = rigesterVM.Email,
                    FullName = $"{rigesterVM.FirstName} {rigesterVM.lastName}",
                };

                var result = await _userManager.CreateAsync(user, rigesterVM.Password);

                if (result.Succeeded)
                {
                        await _userManager.AddToRoleAsync(user, "User");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "خطأ في التسجيل");
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    bool isSuperAdmin = await _userManager.IsInRoleAsync(user, "Super Admin");
                    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                    bool isUser = await _userManager.IsInRoleAsync(user, "User");

                    if (isSuperAdmin)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Chefs", "Admin");
                    }
                    else if (isAdmin)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Menu", "Admin");
                    }
                    else
                    {
                        var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, false, false);

                        if (result.Succeeded)
                        {
                            if (isUser)
                            {
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return RedirectToAction("Index", "Home");
                            }

                            ModelState.AddModelError("", "خطأ في تسجيل الدخول");
                            return View();
                        }

                        ModelState.AddModelError("", "خطأ في تسجيل الدخول");
                        return View();
                    }
                }

                ModelState.AddModelError("", "خطأ في تسجيل الدخول");
                return View();
            }

            ModelState.AddModelError("", "خطأ في تسجيل الدخول");
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "UserAuth");
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Resturant.Interfaces;
using Resturant.Models;
using Resturant.Repositories;
using Resturant.Services;
using Resturant.ViewModel;
using System;

namespace Resturant.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IImageServices _iImageServices;

        public MenuController(IMenuRepository menuRepository, IImageServices imageServices) { 
            _menuRepository = menuRepository;
            _iImageServices = imageServices;
        }

        public IActionResult Index()
        {
            return View(_menuRepository.GetAll());
        }


        public IActionResult Details(int id)
        {
            if (id == null || _menuRepository.GetById(id) == null)
            {
                return NotFound();
            }
            else
            {
                return View(_menuRepository.GetById(id));
            }
        }


        public IActionResult Create()
        {
            MenuVM menuVM = new MenuVM();
            menuVM.MeatList = _menuRepository.GetMeats();
            return View(menuVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuVM menuVM)
        {
            if (ModelState.IsValid)
            {
                var Img = await _iImageServices.UploadImage(menuVM.Image);

                Menu menu = new Menu()
                {
                    MenuName = menuVM.MenuName,
                    Price = menuVM.Price,
                    ImagePath = Img,
                    Ingredients = menuVM.Ingredients,
                    MeatId = menuVM.MeatId,
                };
                await _menuRepository.Add(menu);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                menuVM.MeatList = _menuRepository.GetMeats();
                return View(menuVM);
            }
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null || _menuRepository.GetById(id) == null)
            {
                return NotFound();
            }
            else
            {
                var menu = _menuRepository.GetById(id);

                MenuVM menuVM = new MenuVM();
                menuVM.MeatList = _menuRepository.GetMeats();
                menuVM.MenuName = menu.MenuName;
                menuVM.Price = menu.Price;
                menuVM.MeatId = menu.MeatId;
                menuVM.Ingredients = menu.Ingredients;

                return View(menuVM);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, MenuVM menuVM)
        {
            if (ModelState.IsValid)
            {
                var menus = _menuRepository.GetById(id);
                var Img = await _iImageServices.UploadImage(menuVM.Image);
                
                menus.MenuName = menuVM.MenuName;
                menus.Ingredients = menuVM.Ingredients;
                menus.Price = menuVM.Price;
                menus.ImagePath = Img;
                menus.MeatId = menuVM.MeatId;


                await _menuRepository.Update(menus);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                menuVM.MeatList = _menuRepository.GetMeats();
                return View(menuVM);
            }
        }



        /*        [HttpGet]
                public IActionResult Delete()
                {
                    return View();
                }

                [HttpPost]
                public IActionResult Delete(int id, Menu menu)
                {
                    _menuRepository.Delete(id, menu);
                    return View();
                }*/

        public IActionResult Delete(int id)
        {
            if (id == null || _menuRepository.GetById(id) == null)
            {
                return NotFound();
            }
            else
            {
                _menuRepository.Delete(id);
                return Ok(new { message = "Deleted successfully" });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Resturant.Interfaces;
using Resturant.Models;
using Resturant.ViewModel;

namespace Resturant.Areas.Admin.Controllers
{
    public class ChefsController : BaseController
    {
        private readonly IImageServices _imageServices;
        private readonly IChefsRepository _chefsRepository;

        public ChefsController(IChefsRepository chefsRepository, IImageServices imageServices) {
            _imageServices = imageServices;
            _chefsRepository = chefsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_chefsRepository.GetAllChefs());
        }



        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == null || _chefsRepository.GetChefById(id) == null)
            {
                return NotFound();
            }
            else
            {
                var chef = _chefsRepository.GetChefById(id);
                return View(chef);
            }
        }





        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChefsVM chefsVM)
        {
            if (ModelState.IsValid) {
                var Img = await _imageServices.UploadImage(chefsVM.Image);

                Chefs chefs = new Chefs()
                {
                    Name = chefsVM.Name,
                    Description = chefsVM.Description,
                    Officium = chefsVM.Officium,
                    ImagePath = Img,
                    Facebook = chefsVM.Facebook,
                    Instagram = chefsVM.Instagram,
                    Twitter = chefsVM.Twitter,
                    LinkedIn = chefsVM.LinkedIn,
                };
                await _chefsRepository.Add(chefs);
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return View(); 
            }
        }




        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null || _chefsRepository.GetChefById(id) == null)
            {
                return NotFound();
            }
            else
            {
                var chef = _chefsRepository.GetChefById(id);
                ChefsVM chefsVM = new ChefsVM
                {
                    Description = chef.Description,
                    Facebook = chef.Facebook,
                    Instagram = chef.Instagram,
                    Twitter = chef.Twitter,
                    LinkedIn = chef.LinkedIn,
                    Image = chef.Image,
                    Name = chef.Name,
                    Officium = chef.Officium
                };
                return View(chefsVM);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id,ChefsVM chefsVM)
        {
            if (ModelState.IsValid)
            {
                var chef = _chefsRepository.GetChefById(id);
                var Img = await _imageServices.UploadImage(chefsVM.Image);

                chef.Name = chefsVM.Name;
                chef.Description = chefsVM.Description;
                chef.Officium = chefsVM.Officium;
                chef.ImagePath = Img;
                chef.Facebook = chefsVM.Facebook;
                chef.Instagram = chefsVM.Instagram;
                chef.Twitter = chefsVM.Twitter;
                chef.LinkedIn = chefsVM.LinkedIn;

                await _chefsRepository.Update(chef);
                return RedirectToAction(nameof(Index));
            }
            else 
            { 
                return View(); 
            }
        }

        public IActionResult Delete(int id)
        {
            if (id == null || _chefsRepository.GetChefById(id) == null)
            {
                return NotFound();
            }
            else
            {
                _chefsRepository.Delete(id);
                return Ok(new { message = "Deleted successfully" });
                // return RedirectToAction(nameof(Index));
            }
        }
    }
}
